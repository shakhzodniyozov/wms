import { createBrowserRouter } from "react-router-dom";
import { Manufacturers } from "./pages/Manufacturers";
import { App } from "./App";
import { Main } from "./pages/Main";
import { Categories } from "./pages/Categories";
import { Products } from "./pages/Products";
import { NewProduct } from "./pages/NewProduct";
import { ProductDetails } from "./pages/ProductDetails";
import { AddressStorage } from "./pages/AddressStorage";
import { GenerateAddresses } from "./pages/GenerateAddresses";
import { CellDetails } from "./pages/CellDetails";
import { NewSupply } from "./pages/NewSupply";
import { SuppliesOfGoods } from "./pages/SuppliesOfGoods";
import { SupplyDetails } from "./pages/SupplyDetails";

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
            },
            {
                path: "address-storage/generate-addresses",
                element: <GenerateAddresses />
            },
            {
                path: "address-storage/:addressId",
                element: <CellDetails />
            },
            {
                path: "supply-of-goods",
                element: <SuppliesOfGoods />
            },
            {
                path: "supply-of-goods/new",
                element: <NewSupply />
            },
            {
                path: "supply-of-goods/:id",
                element: <SupplyDetails />
            }
        ]
    }
])