import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaDollarSign, FaFileInvoiceDollar } from 'react-icons/fa';

export const LoansUser = () => {
  const columns = [
    {
      name: "Nombre",
      selector: row => row.nombre,
      sortable: true,
      wrap: true, 
      minWidth: "190px"
    },
    {
      name: "Descripción",
      selector: row => row.descripcion,
      wrap: true
    },
    {
      name: "Cantidad",
      selector: row => row.cantidad,
      sortable: true,
      wrap: true, 
      minWidth: "215px"
    },
    {
      name: "Fecha",
      selector: row => row.fecha,
      sortable: true,
      wrap: true
      },
    {
      name: "Intereses",
      selector: row => row.intereses,
      wrap: true,
      minWidth: "190px"

    },
    {
        name: "Total",
        selector: row => row.total,
        wrap: true,
        minWidth: "190px"
  
      }

  ];

  const data = [
    {
      nombre: "Prestamo 1",
      descripcion: "Prestamo de 100.000",
      cantidad: "₡100.000",
      fecha: "15/03/2025",
      intereses: "5%",
      total: "₡105.000",
    },
    {
        nombre: "Prestamo 2",
        descripcion: "Prestamo de 1.000.000",
        cantidad: "₡1.000.000",
        fecha: "15/03/2025",
        intereses: "20%",
        total: "₡1.200.000",
      },

  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Prestamos</h2>
        <button className="add-button">
          <FaDollarSign style={{ marginRight: "8px" }} />
          Pago Normal
        </button>
        <button className="add-button">
          <FaFileInvoiceDollar style={{ marginRight: "8px" }} />
          Pago Extraordinario
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
