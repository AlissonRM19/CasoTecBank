import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
    const [userData, setUserData] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            try {
                // Se asume que el token es un JWT, se extrae el payload (la parte intermedia) y se decodifica
                const base64Url = token.split('.')[1];
                const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                const payload = JSON.parse(decodeURIComponent(escape(window.atob(base64))));
                setUserData(payload);
            } catch (error) {
                console.error("Error decodificando el token:", error);
            }
        }
    }, []);

    const cerrarSesion = () => {
        localStorage.removeItem("token");
        window.location.href = "/login";
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">
                <Link className="navbar-brand" to="/">BancoApp</Link>
                <div>
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <Link className="nav-link" to="/admin">Admin</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/user">Usuario</Link>
                        </li>
                    </ul>
                </div>
                <div className="d-flex align-items-center">
                    {userData ? (
                        <>
                            <span className="text-white me-3">
                                Bienvenido, {userData.usuario || userData.name || "Usuario"}
                            </span>
                            <button className="btn btn-outline-light" onClick={cerrarSesion}>
                                Cerrar Sesión
                            </button>
                        </>
                    ) : (
                        <button className="btn btn-outline-light" onClick={cerrarSesion}>
                            Cerrar Sesión
                        </button>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
