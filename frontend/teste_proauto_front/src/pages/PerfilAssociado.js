import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Button from "../components/Button";
import MaskedInput from "react-text-mask";
import "../css/PerfilAssociado.css";
import Swal from "sweetalert2";
import { atualizarEndereco, deletarAssociado } from "../services/authService";

const PerfilAssociado = () => {
  const { state } = useLocation();
  const { id, nome, cpf, placa, endereco, telefone } = state || {};

  const [idState] = useState(id);
  const [nomeState, setNome] = useState(nome || "");
  const [cpfState, setCpf] = useState("");
  const [placaState, setPlaca] = useState(placa || "");
  const [enderecoState, setEndereco] = useState(endereco || "");
  const [telefoneState, setTelefone] = useState(telefone || "");
  const [errorMessage, setErrorMessage] = useState("");

  const navigate = useNavigate();

  useEffect(() => {
    if (cpf) {
      const cpfMenorQueOnze = cpf.toString().length < 11 ? true : false;

      const formattedCpf = cpf.toString().length < 11 ? `0${cpf}` : cpf;

      let cpfMasked = cpf;
      if (cpfMenorQueOnze) {
        cpfMasked = formattedCpf.replace(
          /(\d{3})(\d{3})(\d{3})(\d{2})/,
          "$1.$2.$3-$4"
        );
      }

      setCpf(cpfMasked);
    }

    const savedEndereco = localStorage.getItem(`endereco-${idState}`);
    if (savedEndereco) {
      setEndereco(savedEndereco);
    }
  }, [idState, cpf]);

  const handleEnderecoChange = (e) => {
    const newEndereco = e.target.value;
    setEndereco(newEndereco);
    console.log("Endereço atualizado: ", newEndereco);

    localStorage.setItem(`endereco-${idState}`, newEndereco);
  };

  const handleEndereco = async () => {
    if (!enderecoState) {
      setErrorMessage("É obrigatório inserir o endereço!");
      return;
    }
    try {
      console.log(idState, enderecoState);

      await atualizarEndereco(idState, enderecoState);

      Swal.fire({
        title: "Endereço salvo com sucesso!",
        icon: "success",
        confirmButtonText: "OK",
      });

      localStorage.setItem(`endereco-${idState}`, enderecoState);
    } catch (error) {
      setErrorMessage("Erro ao salvar o endereço!");
      console.error(error);
    }
  };

  const handleVoltar = () => {
    navigate(-1);
  };

  const handleDeletar = async () => {
    const confirmDelete = await Swal.fire({
      title: "Você tem certeza que deseja deletar este associado?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Sim, deletar",
      cancelButtonText: "Cancelar",
    });

    if (confirmDelete.isConfirmed) {
      try {
        await deletarAssociado(idState);
        Swal.fire({
          title: "Associado deletado com sucesso!",
          icon: "success",
          confirmButtonText: "OK",
        });
        navigate("/login");
      } catch (error) {
        Swal.fire({
          title: "Erro ao deletar o associado!",
          icon: "error",
          confirmButtonText: "OK",
        });
        console.error(error);
      }
    }
  };

  return (
    <div className="profileAssociado">
      <h2>Perfil</h2>

      <div className="inputContainer">
        <label htmlFor="nome">Nome</label>
        <input type="text" id="nome" value={nomeState} readOnly />
      </div>

      <div className="inputContainer">
        <label htmlFor="cpf">CPF</label>
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
          value={cpfState}
          readOnly
        />
      </div>

      <div className="inputContainer">
        <label htmlFor="placa">Placa</label>
        <input type="text" id="placa" value={placaState} readOnly />
      </div>

      <div className="inputContainer">
        <label htmlFor="endereco">
          Endereço <span className="asterisco">*</span>
        </label>
        <input
          type="text"
          id="endereco"
          placeholder="Endereço"
          value={enderecoState}
          onChange={handleEnderecoChange}
        />
      </div>

      <div className="inputContainer">
        <label htmlFor="telefone">Telefone</label>
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
          value={telefoneState}
          readOnly
        />
      </div>

      <Button label="Salvar" onClick={handleEndereco} className="btn-azul" />
      <div className="button-container">
        <Button label="Voltar" onClick={handleVoltar} className="btn-verde" />
        <Button
          label="Deletar"
          onClick={handleDeletar}
          className="btn-vermelho"
        />
      </div>

      {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
    </div>
  );
};

export default PerfilAssociado;
