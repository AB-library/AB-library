import React, { useState } from 'react';
import './Pasword.css';

function Pasword() {
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('/api/forgot-password', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email }),
            });
            if (response.ok) {
                setMessage('Password reset link sent to your email.');
            } else {
                setMessage('Failed to send reset link. Please try again.');
            }
        } catch (error) {
            setMessage('An error occurred. Please try again.');
        }
    };

    return (
        <form className="password-form" onSubmit={handleSubmit}>
            <h2>Forgot Password?</h2>
            <label>
                Enter your email:
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
            </label>
            <button type="submit">Send Reset Link</button>
            {message && <p className="message">{message}</p>}
        </form>
    );
}

export default Pasword;
