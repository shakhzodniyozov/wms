import './App.css';
import { Outlet } from "react-router-dom";

export function App() {
    return (
        <div className='container'>
            <Outlet />
        </div>
    );
}