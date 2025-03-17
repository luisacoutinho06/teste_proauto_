import React, { useState } from 'react';
import Button from '../components/Button';
import { login } from '../services/authService';

const Login = () => {
  const [cpf, setCpf] = useState('');
  const [placa, setPlaca] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleLogin = async () => {
    try {
      await login(cpf, placa);
    } catch (error) {
      setErrorMessage('Erro ao autenticar');
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <input
        type="text"
        placeholder="CPF"
        value={cpf}
        onChange={(e) => setCpf(e.target.value)}
      />
      <input
        type="text"
        placeholder="Placa"
        value={placa}
        onChange={(e) => setPlaca(e.target.value)}
      />
      <Button label="Autenticar" onClick={handleLogin} />
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default Login;
