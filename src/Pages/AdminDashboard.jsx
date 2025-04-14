import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Tabs, Tab, Container } from 'react-bootstrap';
import { RolTable } from '../Components/RolTable/RolTable';
import { ClientTable } from '../Components/ClientTable/ClientTable';
import { AccountTable } from  '../Components/AccountTable/AccountTable';
import { CardTable } from  '../Components/CardTable/CardTable';
import { AdvisorTable } from  '../Components/AdvisorTable/AdvisorTable';
import { PaymentTable } from  '../Components/PaymentTable/PaymentTable';
import LoanTable from  '../Components/LoanTable/LoanTable';
import { FaUserShield, FaUsers, FaMoneyCheckAlt, FaMoneyBillWave } from 'react-icons/fa'; // íconos
import { FaClipboardUser, FaUserTie, FaMoneyBillTransfer   } from "react-icons/fa6";



const AdminDashboard = () => {
  return(
    <Container className="mt-4">
      <h1 className="text-white">Panel de Administración</h1>      
      <Tabs defaultActiveKey="roles" id="admin-tabs" className="mb-3" fill>
        <Tab
          eventKey="roles"
          title={
            <>
              <FaUserShield className="me-2" />
              Roles
            </>
          }
        >
          <RolTable />
        </Tab>
        <Tab
          eventKey="clientes"
          title={
            <>
              <FaUsers className="me-2" />
              Clientes
            </>
          }
        >
          <ClientTable />
        </Tab>
        <Tab eventKey="accounts" title={<><FaClipboardUser className="me-2" />Cuentas</>}>
          <AccountTable />
        </Tab>
        <Tab eventKey="cards" title={<><FaMoneyCheckAlt className="me-2" />Tarjetas</>}>
          <CardTable />
        </Tab>
        <Tab eventKey="advisor" title={<><FaUserTie  className="me-2" />Asesores</>}>
          <AdvisorTable />
        </Tab>
        <Tab eventKey="payment" title={<><FaMoneyBillWave  className="me-2" />Morosidades</>}>
          <PaymentTable />
        </Tab>
        <Tab eventKey="loan" title={<><FaMoneyBillTransfer className="me-2" />Préstamos</>}>
          <LoanTable />
        </Tab>
      </Tabs>
    </Container>
  );
};

export default AdminDashboard;