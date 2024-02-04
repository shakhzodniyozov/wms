import { useEffect, useState } from "react"
import engineService from "../services/engine.service";
import { Button, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export function Engines() {

    const [engines, setEngines] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        engineService.getAll().then(result => {
            if (result.status === 200) {
                setEngines(result.data);
            }
        })
            .catch(error => console.log(error));

    }, []);

    function deleteEngine(id) {
        engineService.delete(id).then(result => {
            if (result.status === 204)
                setEngines(engines.filter(x => x.id !== id));
        })
    }

    return (
        <div>
            <div>
                <Button
                    onClick={() => navigate("new")}
                >
                    New
                </Button>
            </div>
            <div className="mt-3">
                <Table striped hover>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Capacity</th>
                            <th>Fuel Type</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {engines.map((engine, index) => {
                            return (
                                <tr key={engine.id}>
                                    <td>{index + 1}</td>
                                    <td onClick={() => navigate(`${engine.id}`)}>{engine.capacity.toFixed(1)}</td>
                                    <td>{engine.fuelType}</td>
                                    <td>
                                        <Button
                                            onClick={() => deleteEngine(engine.id)}
                                            variant="danger"
                                        >
                                            Delete
                                        </Button>
                                    </td>
                                </tr>
                            )
                        })}
                    </tbody>
                </Table>
            </div>
        </div>
    )
}