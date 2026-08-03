import api from './axios';

export interface Category {
  id: string;
  name: string;
  icon: string;
  defaultType: string;
}

export interface CreateCategoryData {
  name: string;
  icon: string;
  defaultType: number;
}

export interface UpdateCategoryData {
  id: string;
  name: string;
  icon: string;
  defaultType: number;
}

export const categoriesApi = {
  getAll: () => api.get<Category[]>('/categories'),

  getById: (id: string) => api.get<Category>(`/categories/${id}`),

  create: (data: CreateCategoryData) => api.post<{ id: string }>('/categories', data),

  update: (data: UpdateCategoryData) => api.put(`/categories/${data.id}`, data),

  delete: (id: string) => api.delete(`/categories/${id}`),
};