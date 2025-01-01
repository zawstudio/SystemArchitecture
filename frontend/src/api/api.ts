import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:5001/media",
    headers: {
        "Content-Type": "application/json",
    },
});

export default api;
