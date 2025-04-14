import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5000/api", // URL de la WebAPI
});

// Interceptor para añadir token JWT automáticamente
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;