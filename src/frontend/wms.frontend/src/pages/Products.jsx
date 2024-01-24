import { useEffect, useState } from "react"
import { Button, Table } from "react-bootstrap"
import { Link, useNavigate } from "react-router-dom";
import productService from "../services/product.service";

export function Products() {
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        productService.getAll().then(result => {
            if (result.status === 200)
                setProducts(result.data);
        })
    }, [])

    return (
        <>
            <Link to="/products/new">
                <Button>
                    New product
                </Button>
            </Link>
            <Table striped hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Code</th>
                        <th>Quantity</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((p, i) => {
                        return (
                            <tr key={p.id} onClick={() => navigate(`${p.id}`)}>
                                <td>{i + 1}</td>
                                <td>{p.name}</td>
                                <td>{p.code}</td>
                                <td>{p.quantity}</td>
                                <td>{p.description}</td>
                            </tr>
                        )
                    })}
                </tbody>
            </Table>
        </>
    )
}