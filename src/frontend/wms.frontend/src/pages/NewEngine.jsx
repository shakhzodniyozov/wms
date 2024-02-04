import { useState } from "react"
import { useNavigate } from "react-router-dom";
import engineService from "../services/engine.service";
import { Button } from "react-bootstrap";
import Select from "react-select";

export function NewEngine() {

    const [engine, setEngine] = useState({
        capacity: 0,
        fuelType: ""
    });

    const navigate = useNavigate();

    function uploadEngine() {
        engineService.create(engine).then(result => {
            if (result.status === 200) {
                navigate("/entities/engine");
            }
        }).catch(error => console.log(error));
    }

    return (
        <div className="mt-3">
            <div>
                <input
                    className="form-control"
                    placeholder="Capacity"
                    onChange={(e) => setEngine({ ...engine, capacity: e.target.value })}
                />
            </div>
            <div className="mt-2">
                <Select
                    options={["Petrol", "Diesel"].map(x => ({ value: x, label: x }))}
                    placeholder="Fuel type"
                    onChange={(newValue) => setEngine({ ...engine, fuelType: newValue.label })}
                />
            </div>
            <div className="mt-2">
                <Button
                    onClick={() => uploadEngine()}
                >
                    Save
                </Button>
            </div>
        </div>
    )
}