import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginAssociado from './pages/LoginAssociado.js';
import CadastroAssociado from './pages/CadastroAssociado.js';
import PerfilAssociado from './pages/PerfilAssociado.js';

function App() {
  return (
    <div className="App">
      <Router>
        <div className="content">
          <Routes>
            <Route path="/" element={<Navigate to="/login" />} />
            
            <Route path="/login" element={<LoginAssociado />} />
            <Route path="/cadastro" element={<CadastroAssociado />} />
            <Route path="/perfil" element={<PerfilAssociado />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;
