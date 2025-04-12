import React from 'react';
import { Link } from 'react-router-dom'; // Импортируем Link
import './Header.css';

function Header() {
  const isLoggedIn = false; // Замените на реальную проверку авторизации

  return (
    <header className="header">
      <div className="logo">
        <span>AB-libary</span>
      </div>
      <div className="header-icons">
        <Link to="/notes" className="icon-button">Notes</Link> {/* Добавляем ссылку */}
        <button className="icon-button" disabled={!isLoggedIn}>
          <i className="fas fa-plus"></i>
        </button>
        <button className="icon-button" disabled={!isLoggedIn}>
          <i className="fas fa-bell"></i>
        </button>
        <button className="icon-button" disabled={!isLoggedIn}>
          <i className="fas fa-user-circle"></i>
        </button>
      </div>
    </header>
  );
}

export default Header;
