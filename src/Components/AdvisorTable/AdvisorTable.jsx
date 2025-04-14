import React from 'react';
import '../RolTable/RolTable.css';
import jsPDF from 'jspdf';
import 'jspdf-autotable';
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
    },
    {
      nombreCompleto: "María Fernández",
      cedula: "2-1234-5678",
      fechaNacimiento: "1990-05-15",
      metaColones: 3500000,
      metaDolares: 5000
    },
    {
      nombreCompleto: "José Martínez",
      cedula: "1-3456-7890",
      fechaNacimiento: "1982-11-03",
      metaColones: 5200000,
      metaDolares: 8000
    },
    {
      nombreCompleto: "Ana Sofía Rojas",
      cedula: "3-4567-8901",
      fechaNacimiento: "1995-03-27",
      metaColones: 3000000,
      metaDolares: 4200
    },
    {
      nombreCompleto: "Luis Diego Castro",
      cedula: "1-5678-9012",
      fechaNacimiento: "1988-08-10",
      metaColones: 6000000,
      metaDolares: 9500
    },
    {
      nombreCompleto: "Patricia Mora",
      cedula: "2-6789-0123",
      fechaNacimiento: "1979-01-19",
      metaColones: 4000000,
      metaDolares: 6300
    },
    {
      nombreCompleto: "Andrés Salazar",
      cedula: "1-7890-1234",
      fechaNacimiento: "1992-06-05",
      metaColones: 4800000,
      metaDolares: 7000
    },
    {
      nombreCompleto: "Daniela Vargas",
      cedula: "3-8901-2345",
      fechaNacimiento: "1998-12-11",
      metaColones: 2500000,
      metaDolares: 3900
    },
    {
      nombreCompleto: "Elena Vargas",
      cedula: "1-1500-9632",
      fechaNacimiento: "1990-05-12",
      metaColones: 3000000,
      metaDolares: 5000
    },
    {
      nombreCompleto: "Antonio Márquez",
      cedula: "2-3782-4693",
      fechaNacimiento: "1982-03-18",
      metaColones: 8000000,
      metaDolares: 12000
    },
    {
      nombreCompleto: "Jessica López",
      cedula: "1-4903-1247",
      fechaNacimiento: "1988-11-22",
      metaColones: 6000000,
      metaDolares: 9500
    },
    {
      nombreCompleto: "Fernando Herrera",
      cedula: "3-2045-3869",
      fechaNacimiento: "1995-09-10",
      metaColones: 4500000,
      metaDolares: 7000
    },
    {
      nombreCompleto: "Sandra Ríos",
      cedula: "2-8365-7483",
      fechaNacimiento: "1992-01-09",
      metaColones: 5500000,
      metaDolares: 8500
    },
    {
      nombreCompleto: "Miguel Gómez",
      cedula: "1-6724-1596",
      fechaNacimiento: "1980-06-30",
      metaColones: 7000000,
      metaDolares: 10000
    }

  ];
  const generarReportePDF = () => {
    const doc = new jsPDF();
    // Fecha y hora actual
    const now = new Date();
    const fecha = now.toLocaleDateString('es-CR');
    const hora = now.toLocaleTimeString('es-CR');
    doc.setFontSize(16);
    doc.text("Reporte de Asesores", 14, 15);
    doc.setFontSize(11);
    doc.text(`Fecha: ${fecha}  Hora: ${hora}`, 14, 22);
  
    const tableData = data.map(row => [
      row.nombreCompleto,
      row.cedula,
      row.fechaNacimiento,
      row.metaColones.toLocaleString(),
      row.metaDolares.toLocaleString()
    ]);
  
    doc.autoTable({
      head: [["Nombre Completo", "Cédula", "Fecha de Nacimiento", "Meta Ventas (Colones)", "Meta Ventas (Dólares)"]],
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
    doc.save(`reporte_asesores_${fechaNombre}_${horaNombre}.pdf`);
  };

  return (
    <div className="table-wrapper">
      <div className="table-header">
        <h2 className="table-title">Asesores</h2>
        <div className="table-actions">
          <button className="add-button">
            <FaPlus style={{ marginRight: "8px" }} />
            Agregar Asesor
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
