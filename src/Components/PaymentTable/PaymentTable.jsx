import React from 'react';
import '../RolTable/RolTable.css';
import jsPDF from 'jspdf';
import 'jspdf-autotable';
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
    },
    {
      nombreCompleto: "María José Pérez",
      cedula: "1-1357-2468",
      numeroPrestamo: "PRE-00234",
      cuotasVencidas: 1,
      montoAdeudado: 50000
    },
    {
      nombreCompleto: "Carlos Alberto Sánchez",
      cedula: "3-2468-1357",
      numeroPrestamo: "PRE-00356",
      cuotasVencidas: 2,
      montoAdeudado: 105000
    },
    {
      nombreCompleto: "Patricia González",
      cedula: "2-3691-7530",
      numeroPrestamo: "PRE-00478",
      cuotasVencidas: 4,
      montoAdeudado: 175000
    },
    {
      nombreCompleto: "Juan Carlos Ramírez",
      cedula: "1-4826-9573",
      numeroPrestamo: "PRE-00589",
      cuotasVencidas: 5,
      montoAdeudado: 200000
    },
    {
      nombreCompleto: "Lorena Rodríguez",
      cedula: "3-5790-1245",
      numeroPrestamo: "PRE-00601",
      cuotasVencidas: 3,
      montoAdeudado: 120000
    },
    {
      nombreCompleto: "José Luis Jiménez",
      cedula: "2-6812-3456",
      numeroPrestamo: "PRE-00723",
      cuotasVencidas: 6,
      montoAdeudado: 250000
    },
    {
      nombreCompleto: "Ana María Hernández",
      cedula: "1-7943-5682",
      numeroPrestamo: "PRE-00834",
      cuotasVencidas: 2,
      montoAdeudado: 90000
    },
    {
      nombreCompleto: "Laura Torres",
      cedula: "2-1247-5369",
      numeroPrestamo: "PRE-00945",
      cuotasVencidas: 3,
      montoAdeudado: 150000
    },
    {
      nombreCompleto: "David Pérez",
      cedula: "3-2357-6491",
      numeroPrestamo: "PRE-01056",
      cuotasVencidas: 2,
      montoAdeudado: 95000
    },
    {
      nombreCompleto: "Raquel Díaz",
      cedula: "1-1245-7360",
      numeroPrestamo: "PRE-01167",
      cuotasVencidas: 4,
      montoAdeudado: 210000
    },
    {
      nombreCompleto: "José Martínez",
      cedula: "2-3869-4572",
      numeroPrestamo: "PRE-01278",
      cuotasVencidas: 1,
      montoAdeudado: 60000
    },
    {
      nombreCompleto: "Isabel Torres",
      cedula: "3-5628-3481",
      numeroPrestamo: "PRE-01389",
      cuotasVencidas: 5,
      montoAdeudado: 275000
    },
    {
      nombreCompleto: "Manuel González",
      cedula: "1-7246-5083",
      numeroPrestamo: "PRE-01490",
      cuotasVencidas: 2,
      montoAdeudado: 115000
    }
    
  ];
  const generarReportePDF = () => {
    const doc = new jsPDF();
    // Fecha y hora actual
    const now = new Date();
    const fecha = now.toLocaleDateString('es-CR');
    const hora = now.toLocaleTimeString('es-CR');
    doc.setFontSize(16);
    doc.text("Reporte de Gestión de Mora", 14, 15);
    doc.setFontSize(11);
    doc.text(`Fecha: ${fecha}  Hora: ${hora}`, 14, 22);
  
    const tableData = data.map(row => [
      row.nombreCompleto,
      row.cedula,
      row.numeroPrestamo,
      row.cuotasVencidas,
      row.montoAdeudado.toLocaleString()
    ]);
  
    doc.autoTable({
      head: [["Nombre Completo", "Cédula", "Número de Préstamo", "Cuotas Vencidas", "Deuda (Colones)"]],
      body: tableData,
    styles: {
      fontSize: 10,
      cellPadding: 2,
    },
    headStyles: {
      fillColor: [22, 160, 133],
      textColor: 255,
      fontStyle: 'bold',
    },
    columnStyles: {
      4: { halign: 'right' } // Alinea los montos a la derecha
    },
      startY: 30
    });
    // Guarda el PDF con fecha en el nombre del archivo
    const fechaNombre = now.toISOString().slice(0, 10); // YYYY-MM-DD
    const horaNombre = now.toTimeString().slice(0, 5).replace(':', '-'); // HH-MM
    doc.save(`reporte_mora_${fechaNombre}_${horaNombre}.pdf`);
  };

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Gestión de Mora</h2>
        <div className="table-actions">
            <button className="add-button">
                <FaPlus style={{ marginRight: "8px" }} />
                Agregar Mora
            </button>
            <button className="report-button" onClick={generarReportePDF}>
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
