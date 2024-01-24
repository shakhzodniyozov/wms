import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom";
import { Table } from "react-bootstrap";
import storageService from "../services/storage.service";

export function CellDetails() {
    const params = useParams();

    useEffect(() => {
        storageService.getProductsInCell(params.addressId).then(result => {
            if (result.status === 200) {
                setProducts(result.data)
            }
        });
    }, [params.addressId]);

    const [products, setProducts] = useState([]);

    const navigate = useNavigate();

    return (
        <>
            <Table striped hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Code</th>
                        <th>Quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((product, index) => {
                        return (
                            <tr key={product.id} onClick={() => navigate(`/products/${product.id}`)}>
                                <td>{index + 1}</td>
                                <td>{product.name}</td>
                                <td>{product.code}</td>
                                <td>{product.quantity}</td>
                            </tr>
                        )
                    })}
                </tbody>
            </Table>
        </>
    )
}