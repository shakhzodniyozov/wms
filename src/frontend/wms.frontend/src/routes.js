import { createBrowserRouter } from "react-router-dom";
import { Manufacturers } from "./pages/Manufacturers";
import { App } from "./App";
import { Main } from "./pages/Main";
import { Categories } from "./pages/Categories";
import { Products } from "./pages/Products";
import { NewProduct } from "./pages/NewProduct";
import { ProductDetails } from "./pages/ProductDetails";
import { AddressStorage } from "./pages/AddressStorage";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "/",
                element: <Main />
            },
            {
                path: "categories",
                element: <Categories />
            },
            {
                path: "manufacturers",
                element: <Manufacturers />
            },
            {
                path: "products",
                element: <Products />
            },
            {
                path: "products/new",
                element: <NewProduct />
            },
            {
                path: "products/:id",
                element: <ProductDetails />
            },
            {
                path: "address-storage",
                element: <AddressStorage />
            }
        ]
    }
])