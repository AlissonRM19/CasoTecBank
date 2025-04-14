import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';

const LoanTable = () => {
  const columns = [
    {
      name: "Cliente",
      selector: row => row.cliente,
      sortable: true,
      wrap: true,
      minWidth: "120px"
    },
    {
      name: "Monto Original",
      selector: row => `$${row.montoOriginal.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "180px"
    },
    {
      name: "Saldo Actual",
      selector: row => `$${row.saldo.toLocaleString()}`,
      sortable: true,
      wrap: true,
      minWidth: "180px"
    },
    {
      name: "Tasa de Interés",
      selector: row => `${row.interes}%`,
      sortable: true,
      wrap: true,
      minWidth: "180px"
    },
    {
      name: "Pagos Realizados",
      selector: row => row.pagos.length,
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
      cliente: "Ana López",
      montoOriginal: 10000,
      saldo: 6800,
      interes: 12,
      pagos: [
        { fecha: "2024-01-15", monto: 1000, tipo: "normal" },
        { fecha: "2024-02-15", monto: 1200, tipo: "extraordinario" }
      ]
    },
    {
      cliente: "Carlos Ramírez",
      montoOriginal: 5000,
      saldo: 4800,
      interes: 10,
      pagos: [
        { fecha: "2024-03-01", monto: 200, tipo: "normal" }
      ]
    }
  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Préstamos</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Agregar Préstamo
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

export default LoanTable;
