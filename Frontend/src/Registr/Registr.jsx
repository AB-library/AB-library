import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Registr.css';

function Registr() {
    const [formData, setFormData] = useState({
        name: '',
        email: '',
        password: ''
    });
    const [errors, setErrors] = useState({}); // State for errors
    const navigate = useNavigate(); 

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newErrors = {};

        if (!/^[a-zA-Z]+$/.test(formData.name)) {
            newErrors.name = 'Username must contain only English characters.';
        }

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        try {
            const response = await fetch('/api/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData),
            });
            if (response.ok) {
                console.log('Registration successful');
                navigate('/login'); // Перенаправление на страницу входа
            } else {
                console.error('Registration failed');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <form className="registration-form" onSubmit={handleSubmit}>
            <label>
                Name:
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                />
                {errors.name && <span className="error">{errors.name}</span>}
            </label>
            <label>
                Email:
                <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
            </label>
            <label>
                Password:
                <input
                    type="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
            </label>
            <button type="submit">Register</button>
        </form>
    );
}

export default Registr;
