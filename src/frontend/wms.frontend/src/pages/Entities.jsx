import { ListGroup, ListGroupItem } from "react-bootstrap";
import { Link } from "react-router-dom";

export function Entities() {

    return (
        <ListGroup>
            <ListGroupItem>
                <Link to="categories" >
                    Categories
                </Link>
            </ListGroupItem>
            <ListGroupItem>
                <Link to="manufacturers" >
                    Manufacturers
                </Link>
            </ListGroupItem>
            <ListGroupItem>
                <Link to="engine" >
                    Engines
                </Link>
            </ListGroupItem>
            <ListGroupItem>
                <Link to="supply-of-goods" >
                    Supply Of Goods
                </Link>
            </ListGroupItem>
        </ListGroup>
    )
}