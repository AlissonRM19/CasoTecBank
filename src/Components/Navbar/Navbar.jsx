import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container">
        <Link className="navbar-brand" to="/">BancoApp</Link>
        <div>
          <ul className="navbar-nav">
            <li className="nav-item"><Link className="nav-link" to="/admin">Admin</Link></li>
            <li className="nav-item"><Link className="nav-link" to="/user">Usuario</Link></li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;