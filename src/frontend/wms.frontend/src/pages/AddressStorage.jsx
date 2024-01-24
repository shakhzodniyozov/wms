import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import storageService from "../services/storage.service";

export function AddressStorage() {

    useEffect(() => {
        storageService.getAddresses().then(result => {
            if (result.status === 200) {
                setAddresses(result.data);
            }
        })
    }, []);

    const [addresses, setAddresses] = useState([]);

    const deleteAddress = () => {

    }

    return (
        <div>
            <Link to="/address-storage/generate-addresses">
                <Button
                >
                    Generate Addresses
                </Button>
            </Link>
            <div className="mt-3">
                <ul className="list-group">
                    {addresses.map(address => {
                        return (
                            <li className="list-group-item d-flex">
                                <div className="me-auto">
                                    <Link to={`/address-storage/${address.id}`}>
                                        {address.line}-{address.section}-{address.level}-{address.cell}
                                    </Link>
                                </div>
                                <div>
                                    <Button
                                        onClick={() => deleteAddress()}
                                        variant="danger"
                                    >
                                        Delete
                                    </Button>
                                </div>
                            </li>
                        )
                    })}
                </ul>
            </div>
        </div>
    )
}

