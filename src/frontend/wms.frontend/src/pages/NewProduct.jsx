import { Button, Image, Spinner } from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Select from "react-select";
import productService from "../services/product.service";
import modelService from "../services/model.service";
import engineService from "../services/engine.service";
import noPhoto from "../assets/no-photo.png";

export function NewProduct() {

    const navigate = useNavigate();

    const [form, setForm] = useState({
        name: '',
        price: 0,
        isEnabled: false,
        forAllManufactors: false,
        forAllModels: false,
        manufactorId: 0,
        models: [],
        engines: [],
        categoryId: 0,
        description: '',
        image: noPhoto
    });

    const [data, setData] = useState({
        manufacturers: [],
        categories: []
    });

    const [loading, setLoading] = useState(false);

    const [models, setModels] = useState([]);
    const [yearsOfIssue, setYearsOfIssue] = useState([]);
    const [modelsSelectValues, setModelsSelectValues] = useState([]);
    const [yearsSelectValues, setYearsSelectValues] = useState([]);
    const [engines, setEngines] = useState([]);
    const [engineCapacityStatus, setEngineCapacityStatus] = useState(true);

    useEffect(() => {
        productService.getPreliminaryDataForNewProduct().then(result => setData(result.data))
    }, []);

    const onManufactorChange = (e) => {
        setModelsSelectValues(null);
        setYearsSelectValues([]);
        setYearsOfIssue([]);

        setForm({ ...form, manufactorId: e.value, models: [] });

        modelService.getModelsWithYearsOfIssue(e.value).then(res => {
            setModels(res.data);
        });
    }

    const onModelsListChange = (e) => {
        //adding something
        if (modelsSelectValues === null || modelsSelectValues?.length < e.length) {
            setModelsSelectValues(e);
            let years = models.find(x => x.model === e[e.length - 1].label).yearsOfIssue;
            let options = {
                label: e[e.length - 1].label,
                options: years.map(y => ({ value: Math.random(), label: y.toString(), model: e[e.length - 1].label }))
            };

            setYearsOfIssue([...yearsOfIssue, options]);
        }
        //removing something
        else {
            let deletedModel = modelsSelectValues.filter(x => !e.includes(x))[0];

            setModelsSelectValues([...modelsSelectValues.filter(x => x !== deletedModel)]);
            setYearsOfIssue([...yearsOfIssue.filter(x => x.label !== deletedModel.label)]);
            setForm({ ...form, models: form.models.filter(m => m.model !== deletedModel.label) })
            setYearsSelectValues([...yearsSelectValues.filter(x => x.model !== deletedModel.label)])
        }
    }

    const onYearsListChange = (e) => {
        let models = [...form.models];

        // adding something
        if (yearsSelectValues === null || yearsSelectValues?.length < e.length) {
            const newModel = e.filter(x => !yearsSelectValues.includes(x))[0];

            let model = models.find(x => x.model === newModel.model);

            // model exists
            if (model) {
                model.yearsOfIssue = [...model.yearsOfIssue, parseInt(newModel.label)];
            }
            // model doesn't exist
            else {
                model = {
                    model: newModel.model,
                    yearsOfIssue: [parseInt(newModel.label)]
                };
                models = [...models, model];

            }
            setForm({ ...form, models: models });
        }
        // removing something
        else {
            let deletingModel = yearsSelectValues.filter(x => !e.includes(x))[0];

            let model = models.find(x => x.model === deletingModel.model);
            model.yearsOfIssue = [...model.yearsOfIssue.filter(x => x !== parseInt(deletingModel.label))];
            setForm({ ...form, models: models });
        }
        setYearsSelectValues([...e]);
    }

    function onEngineChange(e) {
        setForm({ ...form, engines: e.map(x => x.value) })
    }

    const uploadProduct = () => {
        setLoading(true);

        productService.create(form).then(result => {

            setTimeout(() => {
                setLoading(false);

                navigate("/products");
            }, 1200);
        })
            .catch(error => {
                console.log(error);
            });
    }

    const onFileUpload = (e) => {
        const reader = new FileReader();

        reader.onload = (e) => {
            setForm({ ...form, image: e.target.result });
        }

        reader.readAsDataURL(e.target.files[0]);
    }

    function onCategoryChange(newValue) {
        setForm({ ...form, categoryId: newValue.value });
        if (newValue.label === "Ходовая часть" || newValue.label === "Двигатель") {
            engineService.getAll().then(result => {
                if (result.status === 200) {
                    setEngines(result.data);
                    setEngineCapacityStatus(false);
                }
            })
        }
        else
            setEngineCapacityStatus(true);
    }

    return (
        <div className="container-fluid p-xxl-5">
            <div className="row mb-4">
                <div className="col-12 col-md-4 col-lg-3">
                    <div className="d-flex flex-column align-items-center">
                        <div className="p-1">
                            <Image
                                src={form.image}
                                width={200}
                                height={200}
                                alt="productImage"
                                thumbnail
                            />
                        </div>
                        <input type="file" hidden id="productImage" onChange={(e) => onFileUpload(e)} />
                        <div className="d-flex">
                            <Button
                                variant="outline-dark"
                                onClick={() => document.getElementById("productImage").click()}
                                className="mt-2"
                            >
                                Select Photo
                            </Button>
                        </div>
                    </div>
                </div>
                <div className="col-sm-12 col-md-8 col-lg-9">
                    <div className="row align-items-end">
                        <div className="col-md-12 col-lg-4">
                            <input
                                className="form-control"
                                id="productName"
                                onChange={(e) => setForm({ ...form, name: e.target.value })}
                                placeholder="Name"
                            />
                        </div>
                        <div className="col-md-12 col-lg-4 mt-2">
                            <input
                                className="form-control"
                                id="productPrice"
                                onChange={(e) => setForm({ ...form, price: parseInt(e.target.value) })}
                                placeholder="Price"
                            />
                        </div>
                        <div className="col-md-12 col-lg-4 mt-2">
                            <Select
                                options={data.categories.map(x => ({ value: x.id, label: x.name }))}
                                onChange={(newValue) => onCategoryChange(newValue)}
                                placeholder="Category"
                            />
                        </div>
                        <div className="col-md-12 col-lg-4 mt-2">
                            <input
                                className="form-control"
                                placeholder="Code"
                                onChange={(e) => setForm({ ...form, code: e.target.value })}
                            />
                        </div>
                        <div className="col-md-12 col-lg-4 mt-2">
                            <Select
                                placeholder="Engine"
                                options={engines.map(x => ({ value: x.id, label: `${x.capacity.toFixed(1)} ${x.fuelType}` }))}
                                onChange={(e) => onEngineChange(e)}
                                isDisabled={engineCapacityStatus}
                                isMulti
                                closeMenuOnSelect={false}
                            />
                        </div>
                    </div>
                    <div className="row mt-2">
                        <div className="col">
                            <div className="form-check form-switch">
                                <input
                                    className="form-check-input"
                                    type="checkbox"
                                    role="switch"
                                    id="isEnabled"
                                    onChange={(e) => setForm({ ...form, isEnabled: e.target.checked })}
                                />
                                <label className="form-check-label" htmlFor="isEnabled">Видно пользователям</label>
                            </div>
                        </div>
                    </div>
                    <div className="row mt-2">
                        <div className="col">
                            <textarea
                                onChange={(e) => setForm({ ...form, description: e.target.value })}
                                placeholder="Description"
                                className="form-control"
                            >
                            </textarea>
                        </div>
                    </div>
                </div>
            </div>
            <h2>Compatibility</h2>
            <div className="row">
                <div className="col-md-12 col-lg-4">
                    <div className="form-check form-switch">
                        <input
                            className="form-check-input"
                            type="checkbox"
                            role="switch"
                            id="forAllManufactors"
                            onChange={(e) => setForm({ ...form, forAllManufactors: e.target.checked })}
                        />
                        <label className="form-check-label" htmlFor="forAllManufactors">Совместим со всеми производителями</label>
                    </div>
                </div>
                <div className="col-md-12 col-lg-4 mt-2 mt-lg-0">
                    <div className="form-check form-switch">
                        <input
                            className="form-check-input"
                            type="checkbox"
                            role="switch"
                            id="forAllModels"
                            onChange={(e) => setForm({ ...form, forAllModels: e.target.checked })}
                        />
                        <label className="form-check-label" htmlFor="forAllModels">Совместим со всеми моделями</label>
                    </div>
                </div>
            </div>
            <div className="row align-items-end">
                <div className="col-12 col-md-4 mt-2">
                    <Select
                        placeholder="Производители"
                        options={data.manufacturers.map(x => ({ value: x.id, label: x.name }))}
                        onChange={(e) => onManufactorChange(e)}
                        isDisabled={form.forAllManufactors}
                    />
                </div>
                <div className="col-12 col-md-4 mt-2">
                    <Select
                        placeholder="Модели"
                        options={models.map(m => ({ value: m.model, label: m.model }))}
                        isMulti
                        onChange={(e) => onModelsListChange(e)}
                        value={modelsSelectValues}
                        closeMenuOnSelect={false}
                        isDisabled={form.forAllModels || form.forAllManufactors}
                    />
                </div>
                <div className="col-12 col-md-4 mt-2">
                    <Select
                        placeholder="Год выпуска"
                        options={yearsOfIssue}
                        isMulti
                        onChange={(e) => onYearsListChange(e)}
                        value={yearsSelectValues}
                        closeMenuOnSelect={false}
                        isDisabled={form.forAllModels || form.forAllManufactors}
                    />
                </div>
            </div>
            <Button
                onClick={(e) => uploadProduct()}
                className="my-2"
            >
                {loading && <Spinner

                    animation="border"
                    as="span"
                    size="sm"
                    role="alert"
                />}
                {loading ? "  Loading..." : "Save"}
            </Button>
        </div >

    )
}