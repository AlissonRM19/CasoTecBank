import React, { useState } from 'react';
import './LoginForm.css';
import { FaUser, FaLock } from "react-icons/fa";
import api from "../../api/config"; // Asegúrate de que esta ruta sea correcta según tu estructura

const LoginForm = () => {
    const [usuario, setUsuario] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post("/auth/login", { usuario, password });
            localStorage.setItem("token", response.data.token);
            window.location.href = "/"; // Redirige al home tras una autenticación exitosa
        } catch (error) {
            // Verifica si error.response existe para evitar errores de acceso a propiedades no definidas
            const errorMsg = error.response && error.response.data && error.response.data.error
                ? error.response.data.error
                : "Ocurrió un error en la autenticación";
            alert("Error de autenticación: " + errorMsg);
        }
    };

    return (
        <div className='wrapper'>
            <form onSubmit={handleSubmit}>
                <h1>Login</h1>
                <div className="input-box">
                    <input
                        type="text"
                        placeholder='Username'
                        value={usuario}
                        onChange={(e) => setUsuario(e.target.value)}
                        required
                    />
                    <FaUser className='icon' />
                </div>
                <div className="input-box">
                    <input
                        type="password"
                        placeholder='Password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                    <FaLock className='icon' />
                </div>

                <div className="remember-forgot">
                    <label>
                        <input type='checkbox' /> Remember me
                    </label>
                    <a href='#'>Forgot Password?</a>
                </div>
                <button type="submit">Login</button>
                <div className="register-link">
                    <p>Don’t have an account? <a href='#'>Register</a> </p>
                </div>
            </form>
        </div>
    );
};

export default LoginForm;