import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom'; // Добавлено useNavigate
import './Login.css';

function Login({ onRegisterClick }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isEmailValid, setIsEmailValid] = useState(true);
  const navigate = useNavigate(); // Инициализация useNavigate

  const handleEmailChange = (e) => {
    const value = e.target.value;
    setEmail(value);
    setIsEmailValid(value.includes('@'));
  };

  const handleLogin = (e) => {
    e.preventDefault();
    console.log('Logging in with:', email, password);
    // здесь API
  };

  const handleRegisterClick = (e) => {
    e.preventDefault();
    navigate('/notes'); // Перенаправление на Notes
  };

  return (
    <form onSubmit={handleLogin} className="login-form">
      <h2>Login</h2>
      <input 
        type="email" 
        placeholder="Email" 
        value={email} 
        onChange={handleEmailChange} 
        className={isEmailValid ? '' : 'invalid'}
      />
      <input 
        type="password" 
        placeholder="Password" 
        value={password} 
        onChange={(e) => setPassword(e.target.value)} 
      />
      <button type="submit">Login</button>
      <p><Link to="/forgot-password">Forgot password?</Link></p>
      <p>Don’t have an account? <a href="#" onClick={handleRegisterClick}><strong>Register</strong></a></p>
    </form>
  );
}

export default Login;
