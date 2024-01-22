import { Button } from "react-bootstrap";

export function AddressStorage() {

    const generateAddresses = () => {

    }

    return (
        <div>
            <Button
                onClick={() => generateAddresses()}
            >
                Generate Addresses
            </Button>
        </div>
    )
}

