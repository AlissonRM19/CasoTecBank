import React from 'react';
import '../RolTable/RolTable.css';
import DataTable from 'react-data-table-component';
import { FaDollarSign } from 'react-icons/fa';
import { useState } from 'react';

export const CardsUser = () => {
  const columns = [
    {
        name: "Número de Tarjeta",
        selector: row => row.numeroTarjeta,
        sortable: true,
        wrap: true, 
        minWidth: "190px"
    },  
    {
        name: "Descripcion",
        selector: row => row.descripcion,
        sortable: true,
        wrap: true, 
        minWidth: "175px"
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
      }
  ];

  const data = [
    {
        numeroTarjeta: "1234-5678-9012-3456",
        descripcion: "Pago Recibos",
        fecha: "15/03/2025",
        total: "-₡50.000",
      },
      {
        numeroTarjeta: "0000-0000-0000-0000",
        descripcion: "Intereses Ganados",
        fecha: "15/05/2025",
        total: "₡85,67",
        },
        {
        numeroTarjeta: "1234-5678-9012-3456",
        descripcion: "Transferencia",
        fecha: "15/03/1945",
        total: "₡8.500",
        },
        {
        numeroTarjeta: "0000-0000-0000-0000",
        descripcion: "Pago Trabajos",
        fecha: "29/02/2025",
        total: "-₡86.000",
        },
  ];
  const [records, setRecords] = useState(data)

  const cardChanger = (e) => {
    const filteredRecord = data.filter(record => {
        return record.numeroTarjeta.includes(e.target.value)
    })
    setRecords(filteredRecord)
  }

  return (
    <div className="table-wrapper">
        <input type="text" placeholder='Ingrese el numero de tarjeta' size="23"
        onChange={cardChanger}
        />
        <input type="text" placeholder='Ingrese la 1era fecha' size="16"/>
        <input type="text" placeholder='Ingrese la 2da fecha' size="15"/>
      <div className="table-header">
        <h2 className="table-title">Tarjetas</h2>
        <button className="add-button">
          <FaDollarSign style={{ marginRight: "8px" }} />
          Pago Tarjeta Credito
        </button>
      </div>
      <DataTable
        columns={columns}
        data={records}
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
