import { useState } from "react"
import { Button } from "react-bootstrap";
import storageService from "../services/storage.service"
import { useNavigate } from "react-router-dom";

export function GenerateAddresses() {

    const [form, setForm] = useState({
        lines: 0,
        sections: 0,
        levels: 0,
        cells: []
    });

    const navigate = useNavigate();

    const returnInputs = () => {
        let html = [];

        for (let i = 0; i < form.levels; i++) {
            html.push(<div className="row mt-2" key={i}>
                <div className="col-3">
                    <label htmlFor={`cellOnLevel${i + 1}`} className="form-label">Cells on {i + 1} level</label> <br />
                </div>
                <div className="col">
                    <input type="number" className="form-control" id={`cellOnLevel${i + 1}`} onChange={(e) => {
                        const newCells = [...form.cells];
                        newCells[i] = e.target.value;
                        setForm({ ...form, cells: newCells });
                    }}
                    />
                </div>
            </div>)
        }

        return html;
    }

    const upload = () => {
        storageService.generateAddresses(form).then(result => {
            if (result.status === 200) {
                navigate("/address-storage");
            }
        });
    }

    return (
        <>
            <div className="row mt-2">
                <div className="col-3">
                    <label htmlFor="lineFrom" className="form-label">Lines</label> <br />
                </div>
                <div className="col">
                    <input type="number" className="form-control" id="lineFrom" onChange={(e) => setForm({ ...form, lines: e.target.value })} />
                </div>
            </div>
            <div className="row mt-2">
                <div className="col-3">
                    <label htmlFor="sectionsQuantity">Sections on each lines</label>
                </div>
                <div className="col">
                    <input type="number" className="form-control" id="sectionsQuantity" onChange={(e) => setForm({ ...form, sections: e.target.value })} />
                </div>
            </div>
            <div className="row mt-2">
                <div className="col-3">
                    <label htmlFor="levelsQuantity">Levels on each sections</label>
                </div>
                <div className="col">
                    <input type="number" className="form-control" id="levelsQuantity" onChange={(e) => {
                        setForm({ ...form, levels: e.target.value });
                    }}
                    />
                </div>
            </div>

            {form.levels > 0 && returnInputs()}

            <Button
                onClick={() => upload()}
            >
                Generate
            </Button>
        </>
    )
}