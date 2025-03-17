import React, { useState } from "react";
import Button from "../components/Button";
import { Link, useNavigate } from "react-router-dom";
import { cadastrar } from "../services/authService";
import MaskedInput from "react-text-mask";
import "../css/CadastroAssociado.css";
import Swal from "sweetalert2";

const validarCPF = (cpf) => {
  cpf = cpf.replace(/[^\d]+/g, "");

  if (cpf.length !== 11) return false;

  if (/^(\d)\1{10}$/.test(cpf)) return false;

  let soma = 0;
  let resto;
  for (let i = 0; i < 9; i++) {
    soma += parseInt(cpf.charAt(i)) * (10 - i);
  }

  resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.charAt(9))) return false;

  soma = 0;
  for (let i = 0; i < 10; i++) {
    soma += parseInt(cpf.charAt(i)) * (11 - i);
  }

  resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.charAt(10))) return false;

  return true;
};

const Cadastro = () => {
  const [nome, setNome] = useState("");
  const [cpf, setCpf] = useState("");
  const [placa, setPlaca] = useState("");
  const [endereco, setEndereco] = useState("");
  const [telefone, setTelefone] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [formError, setFormError] = useState("");

  const navigate = useNavigate();

  const handleCadastro = async () => {
    if (!nome || !cpf || !placa || !endereco || !telefone) {
      setFormError("Todos os campos são obrigatórios!");
      return;
    }

    if (!validarCPF(cpf)) {
      setErrorMessage(
        "CPF inválido! O CPF deve ser composto por 11 números e não pode ser uma sequência repetida."
      );
      return;
    }

    console.log(
      "Tentando cadastrar um associado com os seguintes dados:",
      nome,
      cpf,
      placa,
      endereco,
      telefone
    );
    try {
      await cadastrar(nome, cpf, placa, endereco, telefone);

      setErrorMessage("");
      setFormError("");

      setTimeout(() => {
        Swal.fire({
          title: "Cadastro feito com sucesso!",
          icon: "success",
          confirmButtonText: "OK",
        }).then(() => {
          navigate("/login");
        });
      }, 2000);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        setErrorMessage(error.response.data);
      } else if (error.response && error.response.status === 409) {
        setErrorMessage(error.response.data.message);
      } else {
        setErrorMessage("Erro ao cadastrar, tente novamente mais tarde.");
      }
      console.error(error);
    }
  };

  const handleCpfChange = (e) => {
    const value = e.target.value;
    setCpf(value);
  };

  return (
    <div className="cadastroAssociado">
      <h2>Cadastro</h2>
      <div className="inputContainer">
        <label htmlFor="nome">
          Nome <span className="asterisco">*</span>
        </label>
        <input
          type="text"
          id="nome"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
      </div>

      <div className="inputContainer">
        <label htmlFor="cpf">
          CPF <span className="asterisco">*</span>
        </label>
        <MaskedInput
          id="cpf"
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
          value={cpf}
          onChange={handleCpfChange}
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

      <div className="inputContainer">
        <label htmlFor="endereco">
          Endereço <span className="asterisco">*</span>
        </label>
        <input
          type="text"
          id="endereco"
          placeholder="Endereço"
          value={endereco}
          onChange={(e) => setEndereco(e.target.value)}
        />
      </div>

      <div className="inputContainer">
        <label htmlFor="telefone">
          Telefone <span className="asterisco">*</span>
        </label>
        <MaskedInput
          mask={[
            "(",
            /\d/,
            /\d/,
            ")",
            " ",
            /\d/,
            /\d/,
            /\d/,
            /\d/,
            /\d/,
            "-",
            /\d/,
            /\d/,
            /\d/,
            /\d/,
          ]}
          placeholder="(XX) XXXXX-XXXX"
          id="telefone"
          value={telefone}
          onChange={(e) => setTelefone(e.target.value)}
        />
      </div>

      <div style={{ marginBottom: "15px" }}>
        <Link
          to="/login"
          style={{
            color: "#007bff",
            textDecoration: "none",
            fontSize: "15px",
          }}
        >
          Já possui login? Faça o Login aqui!
        </Link>
      </div>

      <Button label="Cadastrar" onClick={handleCadastro} />
      {formError && <p style={{ color: "red" }}>{formError}</p>}
      {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
    </div>
  );
};

export default Cadastro;
