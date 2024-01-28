import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom"
import supplyService from "../services/supply.service";
import AsyncSelect from "react-select/async";
import { BsXLg } from "react-icons/bs";
import { Button } from "react-bootstrap";
import productService from "../services/product.service";

export function SupplyDetails() {

    const params = useParams();
    const navigate = useNavigate();
    const [supply, setSupply] = useState();
    const [prodList, setProdList] = useState({ supplyId: params.id, date: new Date(), products: {} });
    const [selectedProduct, setSelectedProduct] = useState([]);
    const [productsInTable, setProductsInTable] = useState([]);
    const [quantity, setQuantity] = useState("");

    useEffect(() => {
        supplyService.getById(params.id).then(result => {
            if (result.status === 200) {
                setSupply(result.data);
                setProductsInTable(result.data.supplyDetails.map(x => ({ id: x.productId, name: x.product, quantity: x.quantity })));
                copyToProdList(result.data);
            }
        }).catch(error => console.log(error));

    }, [params.id]);

    function copyToProdList(data) {
        let products = {};

        data.supplyDetails.forEach(x => {
            products[x.productId] = x.quantity;
        });
        setProdList({ ...prodList, date: data.date, products: products });
    }

    function addProduct() {
        if (!selectedProduct.value || quantity === 0) return;

        let productsListCopy = { ...prodList.products };
        productsListCopy[selectedProduct.value] = quantity;
        setProdList({ ...prodList, products: productsListCopy });

        let newRecord = {
            id: selectedProduct.value,
            name: selectedProduct.label,
            quantity: quantity
        };

        setProductsInTable([...productsInTable, newRecord]);

        setSelectedProduct(null);
        setQuantity("");
    }

    function deleteProduct(e) {
        const rowNumber = e.target.parentElement.parentElement.firstChild.innerText;
        const id = productsInTable[rowNumber - 1].id;
        delete prodList.products[id];
        setProdList(prodList);
        setProductsInTable(productsInTable.filter((v, i) => i !== rowNumber - 1));
    }

    function sendConsignment(e) {
        supplyService.update(prodList).then(result => {
            if (result.status === 200)
                navigate("/supply-of-goods");
        }).catch(error => console.error(error));
    }

    function loadProducts(value, callback) {
        productService.getSuggestions(value.trim()).then(result => callback(result.data.map(item => ({ value: item.id, label: item.name }))));
    }

    return (
        <div className="container-fluid">
            <div className="row mt-lg-2 align-items-center">
                <div className="col-12 col-lg-2 col-xl-2">
                    <input
                        type="date"
                        value={prodList.date}
                        onChange={(e) => setProdList({ ...prodList, date: e.target.value })}
                    />
                </div>
                <div className="col-12 col-lg-6 col-xl-8 mt-2 mt-lg-0">
                    <AsyncSelect
                        cacheOptions
                        placeholder="Products"
                        isSearchable
                        value={selectedProduct}
                        onChange={(newValue) => {
                            setSelectedProduct(newValue);
                            document.getElementById("quantity").focus();
                        }}
                        loadOptions={loadProducts}
                    />
                </div>
                <div className="col-12 col-lg-2 col-xl-1 mt-2 mt-lg-0">
                    <input
                        placeholder="Количество"
                        id="quantity"
                        className="form-control"
                        value={quantity}
                        onChange={e => setQuantity(e.target.value)}
                        onKeyPress={e => {
                            if (e.code === "Enter")
                                addProduct();
                        }}
                    />
                </div>
                <div className="col-12 col-lg-2 col-xl-1 mt-2 mt-lg-0">
                    <div className="d-grid d-lg-block">
                        <Button onClick={e => addProduct(e.target)} variant="secondary">Add</Button>
                    </div>
                </div>
            </div>
            <div className="col-12 col-lg-1 mt-2">
                <div className="d-grid d-lg-block">
                    <Button variant="primary" onClick={sendConsignment}>Save</Button>
                </div>
            </div>
            <div className="table-responsive">
                <table className="table products my-1">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Product</th>
                            <th>Quantity</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {productsInTable.map((v, i) => {
                            return (
                                <tr key={i}>
                                    <th scope="row">{i + 1}</th>
                                    <td className="col-product-name">{v.name}</td>
                                    <td>{v.quantity}</td>
                                    <td>
                                        <Button variant="danger" onClick={deleteProduct} data-id={v.id}>
                                            <BsXLg pointerEvents={'none'} />
                                        </Button>
                                    </td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            </div>
        </div >
    )
}
