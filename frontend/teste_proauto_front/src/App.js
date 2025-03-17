import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import LoginAssociado from './pages/LoginAssociado.js';

function App() {
  return (
    <div className="App">
      <Router>
        <div className="content">
          <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
            <Route path="/" element={<LoginAssociado />} />
          </Routes>
        </div>
      </Router>
    </div>
  );
}

export default App;
