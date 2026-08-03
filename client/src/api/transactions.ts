import api from './axios';

export interface Transaction {
  id: string;
  amount: number;
  description: string;
  date: string;
  type: string;
  categoryId: string;
  categoryName: string;
  categoryIcon: string;
}

export interface CreateTransactionData {
  amount: number;
  description: string;
  date: string;
  type: number;
  categoryId: string;
}

export interface UpdateTransactionData {
  id: string;
  amount: number;
  description: string;
  date: string;
  type: number;
  categoryId: string;
}

export const transactionsApi = {
  getByMonth: (year: number, month: number) =>
    api.get<Transaction[]>('/transactions', { params: { year, month } }),

  getById: (id: string) => api.get<Transaction>(`/transactions/${id}`),

  create: (data: CreateTransactionData) =>
    api.post<{ id: string }>('/transactions', data),

  update: (data: UpdateTransactionData) =>
    api.put(`/transactions/${data.id}`, data),

  delete: (id: string) => api.delete(`/transactions/${id}`),
};