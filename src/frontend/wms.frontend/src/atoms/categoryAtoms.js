import { atom } from "recoil";

export const categoriesState = atom({
  key: "categoriesState",
  default: []
});

export const currentCategoryState = atom({
  key: "currentCategoryState",
  default: {}
});