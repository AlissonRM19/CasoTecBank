import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Tabs, Tab, Container } from 'react-bootstrap';
import { RolTable } from '../Components/RolTable/RolTable';
import { ClientTable } from '../Components/ClientTable/ClientTable';
import { AccountTable } from  '../Components/AccountTable/AccountTable';
import { FaUserShield, FaUsers, FaMoneyCheckAlt } from 'react-icons/fa'; // íconos

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
        <Tab eventKey="accounts" title={<><FaMoneyCheckAlt className="me-2" />Cuentas</>}>
          <AccountTable />
        </Tab>
      </Tabs>
    </Container>
  );
};

export default AdminDashboard;