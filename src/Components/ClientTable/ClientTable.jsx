import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';

export const ClientTable = () => {
  const columns = [
    {
      name: "Nombre Completo",
      selector: row => row.nombreCompleto,
      sortable: true,
      wrap: true
    },
    {
      name: "Cédula",
      selector: row => row.cedula,
      sortable: true,
      wrap: true
    },
    {
      name: "Dirección",
      selector: row => row.direccion,
      wrap: true
    },
    {
      name: "Teléfono",
      selector: row => row.telefono,
      sortable: true,
      wrap: true
    },
    {
      name: "Ingreso Mensual",
      selector: row => `$${row.ingresoMensual.toLocaleString()}`,
      sortable: true,
      wrap: true
    },
    {
      name: "Tipo de Cliente",
      selector: row => row.tipoCliente,
      sortable: true,
      wrap: true
    },
    {
      name: "Usuario",
      selector: row => row.usuario,
      wrap: true
    },
    {
      name: "Contraseña",
      selector: row => row.password,
      wrap: true
    },
    {
      name: "Acciones",
      cell: row => (
        <div className="icon-buttons">
          <FaEdit className="action-icon edit" title="Editar" />
          <FaTrash className="action-icon delete" title="Eliminar" />
        </div>
      )
    }
  ];

  const data = [
    {
      nombreCompleto: "Juan Carlos Pérez Jiménez",
      cedula: "1-2345-6789",
      direccion: "Av. Central, San José",
      telefono: "8888-1234",
      ingresoMensual: 1500,
      tipoCliente: "Físico",
      usuario: "juanperez",
      password: "123456"
    },
    {
      nombreCompleto: "Empresa XYZ S.A.",
      cedula: "3-101-456789",
      direccion: "Parque Empresarial, Heredia",
      telefono: "2222-3344",
      ingresoMensual: 25000,
      tipoCliente: "Jurídico",
      usuario: "empresa_xyz",
      password: "emp2024"
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Clientes</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Agregar Cliente
        </button>
      </div>
      <DataTable
        columns={columns}
        data={data}
        responsive
        highlightOnHover
        striped
        pagination
        customStyles={{
          headRow: {
            style: {
              backgroundColor: 'rgba(255, 255, 255, 0.15)',
              color: '#fff',
              fontWeight: 'bold',
              fontSize: '18px',
            }
          },
          headCells: {
            style: {
              color: '#fff',
              fontSize: '16px',
              justifyContent: 'center'
            }
          },
          cells: {
            style: {
              fontSize: '15px',
              paddingTop: '10px',
              paddingBottom: '10px',
              paddingLeft: '15px',
              paddingRight: '15px',
              color: '#fff',
              backgroundColor: 'rgba(255, 255, 255, 0.05)'
            }
          },
          rows: {
            style: {
              minHeight: '60px'
            }
          }
        }}
      />
    </div>
  );
};
