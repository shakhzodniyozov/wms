import axios from "axios";

const ax = axios.create({
    baseURL: "/api",
    headers: {
        "Content-Type": "application/json"
    }
});

export { ax as axios };