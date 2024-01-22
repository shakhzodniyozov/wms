import { useState } from "react";
import categoryService from "../../../services/category.service";
import { useSetRecoilState } from "recoil";
import { Button, Modal } from "react-bootstrap";
import { categoriesState } from "../../../atoms/categoryAtoms";

export function CreateCategoryModal(props) {

  const setCategories = useSetRecoilState(categoriesState);
  const [category, setCategory] = useState({ name: "" });

  function createCategory() {
    categoryService.create(category).then(result => {
      if (result.status === 200) {
        setCategories((prev) => [...prev, result.data]);
        setCategory({});
        props.setShowCreateModal();
      }
    });
  }

  return (
    <Modal show={props.showCreateModal} onHide={props.setShowCreateModal}>
      <Modal.Header closeButton={() => props.setShowCreateModal}>
        <Modal.Title>Add Category</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div>
          <input
            className="form-control"
            placeholder="Name"
            type="text"
            value={category.name}
            onChange={(e) => setCategory({ ...category, name: e.target.value })}
          />
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={createCategory}>Save changes</Button>
      </Modal.Footer>
    </Modal>
  )
}