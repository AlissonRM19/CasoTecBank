import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';

export const AccountTable = () => {
  const columns = [
    {
      name: "Número de Cuenta",
      selector: row => row.numeroCuenta,
      sortable: true,
      wrap: true,
      minWidth: "200px"
    },
    {
      name: "Descripción",
      selector: row => row.descripcion,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Moneda",
      selector: row => row.moneda,
      sortable: true,
      wrap: true,
      minWidth: "120px"
    },
    {
      name: "Tipo de Cuenta",
      selector: row => row.tipoCuenta,
      sortable: true,
      wrap: true,
      minWidth: "170px"
    },
    {
      name: "Cliente",
      selector: row => row.cliente,
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
      numeroCuenta: "00123456789",
      descripcion: "Cuenta principal de ahorros",
      moneda: "Colones",
      tipoCuenta: "Ahorros",
      cliente: "Carlos Pérez"
    },
    {
      numeroCuenta: "00456789012",
      descripcion: "Cuenta para pagos internacionales",
      moneda: "Dólares",
      tipoCuenta: "Corriente",
      cliente: "María González"
    },
    {
      numeroCuenta: "00987654321",
      descripcion: "Cuenta personal",
      moneda: "Euros",
      tipoCuenta: "Ahorros",
      cliente: "Luis Rodríguez"
    },
    {
      numeroCuenta: "00345678901",
      descripcion: "Cuenta corporativa",
      moneda: "Colones",
      tipoCuenta: "Corriente",
      cliente: "Empresa XYZ S.A."
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Cuentas</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Agregar Cuenta
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
