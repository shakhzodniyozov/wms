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
import { Entities } from "./pages/Entities";
import { Engines } from "./pages/Engines";
import { NewEngine } from "./pages/NewEngine";
import { Engine } from "./pages/Engine";

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
                path: "entities",
                element: <Entities />
            },
            {
                path: "entities/categories",
                element: <Categories />
            },
            {
                path: "entities/manufacturers",
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
                path: "entities/supply-of-goods",
                element: <SuppliesOfGoods />
            },
            {
                path: "entities/supply-of-goods/new",
                element: <NewSupply />
            },
            {
                path: "entities/supply-of-goods/:id",
                element: <SupplyDetails />
            },
            {
                path: "entities/engine",
                element: <Engines />
            },
            {
                path: "entities/engine/new",
                element: <NewEngine />
            },
            {
                path: "entities/engine/:id",
                element: <Engine />
            }
        ]
    }
])