import './css/App.css';
import { AdminNavbar } from './components/AdminNavbar';
import { Outlet } from 'react-router-dom';
import { useRecoilValue } from 'recoil';
import { loadingState } from './atoms/others';

function App() {
    const loading = useRecoilValue(loadingState);

    return (
        <div className='container'>
            <AdminNavbar />
            {loading ? (
                <h1>loading</h1>
            ) : (<Outlet />)}
        </div>
    );
}

export { App };
