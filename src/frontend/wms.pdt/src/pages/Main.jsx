import ListGroup from "react-bootstrap/ListGroup";
import { Badge } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export function Main() {

    const navigate = useNavigate()
    return (
        <ListGroup as="ul" className="mt-5">
            <ListGroup.Item
                as="li"
                className="d-flex justify-content-between align-items-start"
                onClick={() => navigate("picking")}
            >
                <div className="ms-2 me-auto">
                    <div>Отбор</div>
                </div>
                <Badge bg="primary" pill>

                </Badge>
            </ListGroup.Item>
            <ListGroup.Item
                as="li"
                className="d-flex justify-content-between align-items-start"
                onClick={() => navigate("allocate")}
            >
                <div className="ms-2 me-auto">
                    <div>Размещение товара</div>
                </div>
                <Badge bg="primary" pill>

                </Badge>
            </ListGroup.Item>
            <ListGroup.Item
                as="li"
                className="d-flex justify-content-between align-items-start"
                onClick={() => navigate("relocate")}
            >
                <div className="ms-2 me-auto">
                    <div>Перемещение товара из ячейки</div>
                </div>
            </ListGroup.Item>
            <ListGroup.Item
                as="li"
                className="d-flex justify-content-between align-items-start"
                onClick={() => navigate("product-info")}
            >
                <div className="ms-2 me-auto">
                    <div>Информация о товаре</div>
                </div>
            </ListGroup.Item>
        </ListGroup>
    )
}