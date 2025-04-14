import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Tabs, Tab, Container } from 'react-bootstrap';
import { AccountsUser } from  '../Components/AccountsUser/AccountsUser';
import {CardsUser} from '../Components/CardsUser/CardsUser'
import { LoansUser } from '../Components/LoansUser/LoansUser';
import { FaUser, FaCreditCard, FaFileInvoice } from 'react-icons/fa'; // íconos

const UserDashboard = () => {
  return(
    <Container className="mt-4">
      <h1 className="text-white">Panel de Usuario</h1>      
      <Tabs defaultActiveKey="cuentas" id="admin-tabs" className="mb-3" fill>
        <Tab
          eventKey="cuentas"
          title={
            <>
              <FaUser className="me-2" />
              Cuentas
            </>
          }
        >
          <AccountsUser />
        </Tab>
        <Tab
          eventKey="tarjetas"
          title={
            <>
              <FaCreditCard className="me-2" />
              Tarjetas
            </>
          }
        >
          <CardsUser />
        </Tab>
        <Tab eventKey="prestamos" title={<><FaFileInvoice className="me-2" />Prestamos</>}>
          <LoansUser />
        </Tab>
      </Tabs>
    </Container>
  );
};

export default UserDashboard;