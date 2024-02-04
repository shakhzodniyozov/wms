import { Link, useNavigate } from "react-router-dom";
import { Button, Table } from "react-bootstrap";
import { useEffect, useState } from "react";
import supplyService from "../services/supply.service";

export function SuppliesOfGoods() {

    const [supplies, setSupplies] = useState([]);
    const [numbers, setnumbers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        supplyService.getSupplies().then(result => {
            if (result.status === 200) {
                setSupplies(result.data);
            }
        });

        setnumbers([1, 2, 3]);
    }, []);

    function deleteSupply(id) {
        let array = supplies.filter(x => x.id !== id);
        supplyService.delete(id).then(result => {
            if (result === 204) {
                setSupplies([...array]);
            }
        });
    }

    return (
        <>
            <Link
                to="new"
            >
                <Button>
                    New Supply
                </Button>
            </Link>
            <Table striped bordered hover className="mt-3">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Date</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {supplies.map((s, i) => {
                        return (
                            <tr key={s.id}>
                                <td>{i + 1}</td>
                                <td onClick={() => navigate(`${s.id}`)}>{s.date}</td>
                                <td>
                                    <Button
                                        variant="danger"
                                        onClick={() => deleteSupply(s.id)}
                                    >
                                        Delete
                                    </Button>
                                </td>
                            </tr>
                        )
                    })}
                </tbody>
            </Table>
        </>
    )
}