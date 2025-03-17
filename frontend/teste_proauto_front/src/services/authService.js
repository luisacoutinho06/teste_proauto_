import { postCadastro, putEnderecoAtualizacao, postLogin, deleteAssociado } from "./api";

export const login = async (cpf, placa) => {
  const response = await postLogin(cpf, placa);
  return response.data;
};

export const cadastrar = async (nome, cpf, placa, endereco, telefone) => {
  const response = await postCadastro(nome, cpf, placa, endereco, telefone);
  return response.data;
};

export const atualizarEndereco = async (id, endereco) => {
  const response = await putEnderecoAtualizacao(id, endereco);
  return response.data;
};

export const deletarAssociado = async (id) => {
  const response = await deleteAssociado(id);
  return response.data;
};