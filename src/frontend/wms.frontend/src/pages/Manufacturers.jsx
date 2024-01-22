import { useEffect, useState } from "react"
import { useSetRecoilState } from "recoil";
import { loadingState } from "../atoms/others";
import manufacturerService from "../services/manufactor.service";
import { Table } from "react-bootstrap";

export function Manufacturers() {

    const setLoading = useSetRecoilState(loadingState);
    const [manufacturers, setManufacturers] = useState([]);

    useEffect(() => {
        manufacturerService.getManufacturersWithModels().then(result => {
            if (result.status === 200) {
                setManufacturers(result.data);
            }
        });
    }, [setLoading]);

    return (
        <>
            {manufacturers.map(x => {
                return (
                    <>
                        <h1>{x.manufacturer}</h1>
                        <Table striped hover>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Model</th>
                                    <th>Years of issue</th>
                                </tr>
                            </thead>
                            <tbody>
                                {x.models.map((m, i) => {
                                    return (
                                        <tr>
                                            <td>{i + 1}</td>
                                            <td>{m.modelName}</td>
                                            <td>{m.yearsOfIssue}</td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </Table>
                    </>
                )
            })}
        </>
    )
}