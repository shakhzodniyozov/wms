import { Button, Modal } from "react-bootstrap";
import categoryService from "../../../services/category.service";
import { useRecoilState } from "recoil";
import { categoriesState, currentCategoryState } from "../../../atoms/categoryAtoms";

export function UpdateCategoryModal(props) {

  const [currentCategory, setCurrentCategory] = useRecoilState(currentCategoryState);
  const [categories, setCategories] = useRecoilState(categoriesState);

  function updateCategory() {
    categoryService.update(currentCategory).then(result => {
      if (result.status === 200) {
        const index = categories.findIndex((x) => x.id === result.data.id);
        let categoriesCopy = [...categories];
        categoriesCopy.splice(index, 1)
        categoriesCopy.splice(index, 0, result.data);
        setCategories(categoriesCopy);
        props.setShowModal(false);
      }
    });
  }

  return (
    <Modal show={props.showUpdateModal} onHide={props.setShowUpdateModal}>
      <Modal.Header closeButton>
        <Modal.Title>Редактировать</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div>
          <input
            className="form-control"
            type="text"
            value={currentCategory.name}
            onChange={(e) => setCurrentCategory({ ...currentCategory, name: e.target.value })}
          />
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="danger">Удалить</Button>
        <Button variant="primary" onClick={updateCategory}>Сохранить изменения</Button>
      </Modal.Footer>
    </Modal>
  )
}