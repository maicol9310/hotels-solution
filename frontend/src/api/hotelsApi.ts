import axios from 'axios';

const API_BASE = 'https://localhost:7283';

const api = axios.create({
  baseURL: API_BASE,
  headers: { 'Content-Type': 'application/json' }
});

// Login: obtiene JWT
export const login = async (username: string, role: string) => {
  const response = await api.post(`/login?username=${username}&role=${role}`, { username, role });
  return response.data; // { token: string }
};

// Obtener todos los hoteles (público)
export const getAllProperties = async () => {
  const response = await api.get('/api/properties/public');
  return response.data;
};

// Obtener hotel por ID (requiere JWT)
export const getPropertyById = async (propertyId: string, token: string) => {
  const response = await api.get(`/api/properties/${propertyId}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return response.data;
};

// Crear propiedad (requiere JWT)
export const createProperty = async (propertyData: any, token: string) => {
  const response = await api.post('/api/properties', propertyData, {
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
  return response.data;
};

export default api;
