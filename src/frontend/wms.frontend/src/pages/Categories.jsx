import { useEffect, useState } from "react";
import { categoriesState, currentCategoryState } from "../atoms/categoryAtoms";
import categoryService from "../services/category.service";
import { Button, ListGroup } from "react-bootstrap";
import { UpdateCategoryModal } from "../components/Modals/Category/UpdateCategoryModal";
import { useRecoilState, useSetRecoilState } from "recoil";
import { CreateCategoryModal } from "../components/Modals/Category/CreateCategoryModal";

export function Categories() {

  const setCurrentCategory = useSetRecoilState(currentCategoryState);
  const [categories, setCategories] = useRecoilState(categoriesState);
  const [loading, setLoading] = useState(true);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [showCreateModal, setShowCreateModal] = useState(false);

  useEffect(() => {
    categoryService.getAll().then(result => {
      setCategories((prev) => [
        ...result.data
      ]);
      setLoading(false);
    })
  }, [setCategories])

  return !loading ? (
    <div>
      <ListGroup as="ol" numbered>
        {categories.map(category => {
          return (
            <ListGroup.Item
              as="button"
              className="d-flex jusjustify-content-center"
              onClick={() => { setCurrentCategory(category); setShowUpdateModal(true) }}
              key={category.id}
            >
              <div className="ms-2 d-flex flex-column align-items-start">
                <div className="fw-bold">{category.name}</div>
                Description
              </div>
            </ListGroup.Item>
          )
        })}
      </ListGroup>
      <Button onClick={() => setShowCreateModal(true)}>Add new</Button>

      <UpdateCategoryModal
        showUpdateModal={showUpdateModal}
        setShowUpdateModal={() => setShowUpdateModal(false)}
      />

      <CreateCategoryModal
        showCreateModal={showCreateModal}
        setShowCreateModal={() => setShowCreateModal(false)}
      />
    </div>
  ) : (
    <div>
      loading...
    </div>
  )
}