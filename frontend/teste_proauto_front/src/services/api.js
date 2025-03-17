import axios from "axios";

const apiUrl = process.env.REACT_APP_BACKEND_API_URL;

if (!apiUrl) {
  console.error("BACKEND_API_URL não foi configurado corretamente.");
}

const api = axios.create({
  baseURL: apiUrl,
});

export const postLogin = async (cpf, placa) => {
  try {
    return await api.post("/api/associado/autenticar", { CPF: cpf, Placa: placa });
  } catch (error) {
    console.error("Erro na requisição de login:", error.response ? error.response.data : error.message);
    throw error;
  }
};

export const postCadastro = async (nome, cpf, placa, endereco, telefone) => {
  try {
    return await api.post("/api/associado/criar", { Nome: nome, CPF: cpf, Placa: placa, Endereco: endereco, Telefone: telefone });
  } catch (error) {
    console.error("Erro na requisição de cadastro:", error.response ? error.response.data : error.message);
    throw error;
  }
};

export const putEnderecoAtualizacao = async (id, endereco) => {
  const novoEndereco = endereco;

  return api.put(`/api/associado/atualizar-endereco/${id}`, novoEndereco, {
    headers: {
      "Content-Type": "application/json",
    },
  }).catch((error) => {
    console.error("Erro na requisição de atualização de endereço:", error.response ? error.response.data : error.message);
    throw error;
  });
};

export const deleteAssociado = async (id) => {
  return api.delete(`/api/associado/deletar/${id}`, {
  }).catch((error) => {
    console.error("Erro ao tentar deletar o associado", error.response ? error.response.data : error.message);
    throw error;
  });
};





export default api;
