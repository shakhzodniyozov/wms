import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom"
import engineService from "../services/engine.service";
import Select from "react-select";
import { Button } from "react-bootstrap";
import { useRecoilState } from "recoil";
import { loadingState } from "../atoms/others";

export function Engine() {

    const params = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [engine, setEngine] = useState({});

    useEffect(() => {
        setLoading(true);
        engineService.getById(params.id).then(result => {
            if (result.status === 200) {
                setLoading(false);
                setEngine(result.data);
            }
        })
            .catch(error => console.log(error));
    }, [params.id]);

    function updateEngine() {
        engineService.update(engine).then(result => {
            if (result.status === 204)
                navigate("/entities/engine");
        })
            .catch(error => console.log(error));
    }

    return !loading ? (
        <div className="mt-3">
            <div>
                <input
                    className="form-control"
                    placeholder="Capacity"
                    onChange={(e) => setEngine({ ...engine, capacity: e.target.value })}
                    defaultValue={engine.capacity}
                />
            </div>
            <div className="mt-2">
                <Select
                    defaultValue={{ value: engine.fuelType, label: engine.fuelType }}
                    options={["Diesel", "Petrol"].map(x => ({ value: x, label: x }))}
                    placeholder="Fuel type"
                    onChange={(newValue) => setEngine({ ...engine, fuelType: newValue.label })}
                />
            </div>
            <div className="mt-2">
                <Button
                    onClick={() => updateEngine()}
                >
                    Save
                </Button>
            </div>
        </div>
    ) : <h1>Loading</h1>
}