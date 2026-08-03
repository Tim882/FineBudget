import api from './axios';

export interface CategoryStat {
  categoryName: string;
  categoryIcon: string;
  total: number;
}

export const statisticsApi = {
  getByCategory: (year: number, month: number) =>
    api.get<CategoryStat[]>('/statistics/by-category', { params: { year, month } }),
};