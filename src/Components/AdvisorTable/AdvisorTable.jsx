import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus, FaFileAlt } from 'react-icons/fa';

export const AdvisorTable = () => {
  const columns = [
    {
      name: "Nombre Completo",
      selector: row => row.nombreCompleto,
      sortable: true,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Cédula",
      selector: row => row.cedula,
      sortable: true,
      wrap: true,
      minWidth: "120px"
    },
    {
      name: "Fecha de Nacimiento",
      selector: row => row.fechaNacimiento,
      wrap: true,
      minWidth: "190px"
    },
    {
      name: "Meta Ventas (₡)",
      selector: row => `₡${row.metaColones.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "180px"
    },
    {
      name: "Meta Ventas ($)",
      selector: row => `$${row.metaDolares.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "180px"
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
      nombreCompleto: "Laura Martínez",
      cedula: "2-1345-6789",
      fechaNacimiento: "1990-03-15",
      metaColones: 3000000,
      metaDolares: 5000
    },
    {
      nombreCompleto: "Carlos Gómez",
      cedula: "1-2456-7890",
      fechaNacimiento: "1985-07-22",
      metaColones: 4500000,
      metaDolares: 7000
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Asesores</h2>
        <div className="table-actions">
          <button className="add-button">
            <FaPlus style={{ marginRight: "8px" }} />
            Agregar Asesor
          </button>
          <button className="report-button">
            <FaFileAlt style={{ marginRight: "8px" }} />
            Generar Reporte
          </button>
        </div>
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
