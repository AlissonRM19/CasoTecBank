import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';

export const CardTable = () => {
  const columns = [
    {
      name: "Número de Tarjeta",
      selector: row => row.numeroTarjeta,
      sortable: true,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Tipo de Tarjeta",
      selector: row => row.tipoTarjeta,
      sortable: true,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Fecha de Expiración",
      selector: row => row.fechaExpiracion,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Código de Seguridad",
      selector: row => row.codigoSeguridad,
      sortable: true,
      wrap: true,
      minWidth: "210px"
    },
    {
      name: "Saldo / Crédito Disponible",
      selector: row => `$${row.disponible.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "250px"
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
      numeroTarjeta: "1234 5678 9012 3456",
      tipoTarjeta: "Débito",
      fechaExpiracion: "08/27",
      codigoSeguridad: "123",
      disponible: 1500
    },
    {
      numeroTarjeta: "9876 5432 1098 7654",
      tipoTarjeta: "Crédito",
      fechaExpiracion: "12/25",
      codigoSeguridad: "789",
      disponible: 5000
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Tarjetas</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Agregar Tarjeta
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
