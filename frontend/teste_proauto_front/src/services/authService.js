import { postLogin } from "./api";

export const login = async (cpf, placa) => {
  const response = await postLogin(cpf, placa);
  return response.data;
};
