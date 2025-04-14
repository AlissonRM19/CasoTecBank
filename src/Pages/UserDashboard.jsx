import React, { useEffect, useState } from 'react';
import api from '../api/config';

const UserDashboard = () => {
    const [cuentas, setCuentas] = useState([]);

    useEffect(() => {
        const cargarCuentas = async () => {
            try {
                const response = await api.get("/cuentas/mis-cuentas");
                setCuentas(response.data);
            } catch (error) {
                console.error("Error cargando cuentas:", error);
            }
        };
        cargarCuentas();
    }, []);

    return (
        <div className="container mt-5">
            <h2>Bienvenido Usuario</h2>
            <h3>Mis Cuentas</h3>
            <ul>
                {cuentas.map((cuenta) => (
                    <li key={cuenta.cuentaId}>
                        {cuenta.numeroCuenta} - Saldo: ₡{cuenta.saldo}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserDashboard;
