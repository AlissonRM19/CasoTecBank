import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaFileAlt, FaPlus } from 'react-icons/fa';

export const PaymentTable = () => {
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
      name: "Número de Préstamo",
      selector: row => row.numeroPrestamo,
      sortable: true,
      wrap: true,
      minWidth: "210px"
    },
    {
      name: "Cuotas Vencidas",
      selector: row => row.cuotasVencidas,
      sortable: true,
      wrap: true,
      minWidth: "180px"
    },
    {
      name: "Monto Adeudado",
      selector: row => `₡${row.montoAdeudado.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "190px"
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
      nombreCompleto: "Andrea Solano Mora",
      cedula: "1-1234-5678",
      numeroPrestamo: "PRE-00123",
      cuotasVencidas: 2,
      montoAdeudado: 85000
    },
    {
      nombreCompleto: "Luis Rodríguez Vargas",
      cedula: "2-2345-6789",
      numeroPrestamo: "PRE-00145",
      cuotasVencidas: 3,
      montoAdeudado: 132500
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Gestión de Mora</h2>
        <div className="table-actions">
            <button className="add-button">
                <FaPlus style={{ marginRight: "8px" }} />
                Agregar Mora
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
