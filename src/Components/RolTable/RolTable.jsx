import React from 'react';
import './RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';

export const RolTable = () => {
  const columns = [
    {
      name: "Nombre",
      selector: row => row.nombre,
      sortable: true,
      wrap: true
    },
    {
      name: "Descripción",
      selector: row => row.descripción,
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
      nombre: "Admin",
      descripción: "Control total de la base de datos"
    },
    {
      nombre: "QA",
      descripción: "Verifica que la base esté funcionando correctamente con pruebas automatizadas o manuales en producción"
    },
    {
      nombre: "Developer",
      descripción: "Programa, mantiene y da soporte a todas las funcionalidades del sistema incluyendo el backend y frontend"
    },
    {
      nombre: "User",
      descripción: "Utiliza la base de datos para su operación diaria sin acceso administrativo"
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Roles</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Agregar Rol
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
