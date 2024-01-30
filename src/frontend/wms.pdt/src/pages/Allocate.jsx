import { useEffect, useState } from "react"
import productService from "../services/product.service";

export function Allocate() {

    useEffect(() => {
        document.getElementById("productName").focus();
    }, []);

    const [data, setData] = useState({
        productId: "",
        cellId: "",
        quantity: 0
    });

    const [productName, setProductName] = useState("");

    function getProductByEAN(ean) {
        if (ean.length !== 13) return;

        productService.getProductByEAN(ean).then(result => {
            if (result.status === 200) {
                setData({ ...data, productId: result.data.id })
                setProductName(result.data.name)
                document.getElementById("productName").value = result.data.name;
                document.getElementById("cell").focus();
            }
        });
    }

    return (
        <div className="p-3">
            <div>
                <label htmlFor="productName">Товар</label>
                <input
                    id="productName"
                    className="form-control"
                    onChange={(e) => getProductByEAN(e.target.value)}
                />
            </div>
            <div className="mt-2">
                <label htmlFor="cell">Целевая ячейка</label>
                <input
                    id="cell"
                    className="form-control"
                />
            </div>
            <div className="mt-2">
                <label htmlFor="productName">Количество</label>
                <input
                    id="quantity"
                    className="form-control"
                />
            </div>
        </div>
    )
}