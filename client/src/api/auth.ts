import api from './axios';

interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  user: {
    id: string;
    email: string;
    displayName: string;
  };
}

export const authApi = {
  register: (data: { email: string; password: string; displayName: string }) =>
    api.post<AuthResponse>('/auth/register', data),

  login: (data: { email: string; password: string }) =>
    api.post<AuthResponse>('/auth/login', data),

  refresh: (data: { accessToken: string; refreshToken: string }) =>
    api.post<AuthResponse>('/auth/refresh', data),

  logout: (refreshToken: string) =>
    api.post('/auth/logout', { refreshToken }),
};