import React from 'react';
import './Footer.css';

function Footer() {
  return (
    <footer className="footer">
      <div className="footer-content">
        
        <div className="footer-section">
          <h2>AB</h2>
        </div>

        <div className="footer-section">
          <p><a href="https://github.com/AB-library" target="_blank" rel="noopener noreferrer">About us</a></p>
          <p>Contacts</p>
        </div>

        <div className="footer-section">
          <p>Contacts</p>
          <p>Contacts</p>
        </div>

        <div className="footer-section">
          <div className="footer-icons">
            <a href="https://www.instagram.com/invites/contact/?utm_source=ig_contact_invite&utm_medium=copy_link&utm_content=8y7o5gc" target="_blank" rel="noopener noreferrer">
              <i className="fab fa-instagram"></i>
            </a>
            <a href="https://www.linkedin.com/in/abrams63" target="_blank" rel="noopener noreferrer">
              <i className="fab fa-linkedin-in"></i>
            </a>
            <a href="#"><i className="fab fa-x-twitter"></i></a>
          </div>
          <p>+380633403005</p>
        </div>
        
      </div>
    </footer>
  );
}

export default Footer;

