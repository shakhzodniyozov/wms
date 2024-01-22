import { Container, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";

export function AdminNavbar() {

    return (
        <Navbar bg="light" data-bs-theme="light">
            <Container>
                <Navbar.Brand as={Link} to={"/"}>Home</Navbar.Brand>
                <Nav className="me-auto">
                    <Nav.Link as={Link} to={"categories"}>Categories</Nav.Link>
                    <Nav.Link as={Link} to={"manufacturers"}>Manufacturers</Nav.Link>
                    <Nav.Link as={Link} to={"products"}>Products</Nav.Link>
                    <Nav.Link as={Link} to={"address-storage"}>Address Storage</Nav.Link>
                </Nav>
            </Container>
        </Navbar>
    )
}