import { Route, Routes } from "react-router-dom";
import { AdminNavbar } from "../components/AdminNavbar";
import { Main } from "./Main";

export function Admin() {

	return (
		<>
			<AdminNavbar />

			<Routes>
				<Route exact path="/" element={<Main />} />
			</Routes>
		</>
	)
}