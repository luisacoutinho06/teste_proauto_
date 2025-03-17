import React, { useState } from "react";
import Button from "../components/Button";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../services/authService";
import MaskedInput from "react-text-mask";
import Swal from "sweetalert2";
import "../css/LoginAssociado.css";

const Login = () => {
  const [cpf, setCpf] = useState("");
  const [placa, setPlaca] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const navigate = useNavigate();

  const handleLogin = async () => {
    if (!cpf || !placa) {
      setErrorMessage("Todos os campos são obrigatórios!");
      return;
    }
    console.log("Tentando fazer login com:", cpf, placa);
    try {
      var response = await login(cpf, placa);

      Swal.fire({
        title: "Login feito com sucesso!",
        icon: "success",
        confirmButtonText: "OK",
      }).then(() => {
        navigate("/perfil", {
          state: {
            id: response.id,
            nome: response.nome,
            cpf: response.cpf,
            placa: response.placa,
            endereco: response.endereco,
            telefone: response.telefone,
          },
        });
      });
    } catch (error) {
      setErrorMessage("Erro ao autenticar!");
      console.error(error);
    }
  };

  return (
    <div className="loginAssociado">
      <h2>Login</h2>

      <div className="inputContainer">
        <label htmlFor="cpf">
          CPF <span className="asterisco">*</span>
        </label>
        <MaskedInput
          mask={[
            /\d/,
            /\d/,
            /\d/,
            ".",
            /\d/,
            /\d/,
            /\d/,
            ".",
            /\d/,
            /\d/,
            /\d/,
            "-",
            /\d/,
            /\d/,
          ]}
          placeholder="CPF"
          id="cpf"
          value={cpf}
          onChange={(e) => setCpf(e.target.value)}
        />
      </div>

      <div className="inputContainer">
        <label htmlFor="placa">
          Placa <span className="asterisco">*</span>
        </label>
        <input
          type="text"
          id="placa"
          placeholder="XXXX-123, XXX-1234"
          value={placa}
          onChange={(e) => setPlaca(e.target.value)}
          maxLength="7"
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <Link
          to="/cadastro"
          style={{
            color: "#007bff",
            textDecoration: "none",
            fontSize: "15px",
          }}
        >
          Ainda não possui cadastro? Cadastre-se aqui!
        </Link>
      </div>

      <Button label="Autenticar" onClick={handleLogin} />
      {errorMessage && <p>{errorMessage}</p>}
    </div>
  );
};

export default Login;
