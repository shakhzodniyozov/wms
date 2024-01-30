import { createBrowserRouter } from "react-router-dom";
import { Main } from "./pages/Main";
import { ProductInfo } from "./pages/ProductInfo";
import { Allocate } from "./pages/Allocate";
import { Picking } from "./pages/Picking";
import { Relocate } from "./pages/Relocate";
import { App } from "./App";

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
                path: "picking",
                element: <Picking />
            },
            {
                path: "allocate",
                element: <Allocate />
            },
            {
                path: "product-info",
                element: <ProductInfo />
            },
            {
                path: "relocate",
                element: <Relocate />
            }
        ]
    }
]);
