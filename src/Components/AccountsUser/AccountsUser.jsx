import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaPlus } from 'react-icons/fa';

export const AccountsUser = () => {
  const columns = [
    {
      name: "Número de Tarjeta",
      selector: row => row.numeroTarjeta,
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
      name: "Tipo de Transferencia",
      selector: row => row.transferencia,
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
      name: "Total transferido",
      selector: row => row.total,
      wrap: true,
      minWidth: "190px"

    },

  ];

  const data = [
    {
      numeroTarjeta: "1234-5678-9012-3456",
      descripcion: "Pago Recibos",
      transferencia: "Retiro",
      fecha: "15/03/2025",
      total: "-₡50.000",
    },
    {
      numeroTarjeta: "0000-0000-0000-0000",
      descripcion: "Intereses Ganados",
      transferencia: "Deposito",
      fecha: "15/05/2025",
      total: "₡85,67",
      },
      {
      numeroTarjeta: "1234-5678-9012-3456",
      descripcion: "Transferencia",
      transferencia: "Deposito",
      fecha: "15/03/1945",
      total: "₡8.500",
      },
      {
      numeroTarjeta: "0000-0000-0000-0000",
      descripcion: "Pago Trabajos",
      transferencia: "Retiro",
      fecha: "29/02/2025",
      total: "-₡86.000",
      },

  ];

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Cuentas</h2>
        <button className="add-button">
          <FaPlus style={{ marginRight: "8px" }} />
          Realizar Transferencia
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
