import axios from "axios";

const apiUrl = process.env.REACT_APP_BACKEND_API_URL;

if (!apiUrl) {
  console.error("BACKEND_API_URL não foi configurado corretamente.");
}

const api = axios.create({
  baseURL: ProcessingInstruction.apiUrl,
});

export const postLogin = (cpf, placa) => {
  return api.post("/api/associado/autenticar", { CPF: cpf, Placa: placa });
};

export default api;
