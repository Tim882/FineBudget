import { useMemo } from 'react';
import { useQuery } from '@tanstack/react-query';
import {
  Typography, Grid as Grid, Card, CardContent, Box, CircularProgress,
  alpha,
} from '@mui/material';
import {
  PieChart, Pie, Cell, ResponsiveContainer,
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend,
  Area, ComposedChart,
} from 'recharts';
import { TrendingUp, TrendingDown, Wallet } from '@mui/icons-material';
import { transactionsApi } from '../api/transactions';
import { statisticsApi } from '../api/statistics';

const COLORS = ['#818cf8', '#34d399', '#fbbf24', '#f472b6', '#fb923c', '#38bdf8', '#a78bfa', '#f87171'];

const DashboardPage = () => {
  const now = new Date();
  const year = now.getFullYear();
  const month = now.getMonth() + 1;

  const { data: transactions, isLoading: txLoading } = useQuery({
    queryKey: ['transactions', year, month],
    queryFn: async () => {
      const response = await transactionsApi.getByMonth(year, month);
      return response.data;
    },
  });

  const { data: stats, isLoading: statsLoading } = useQuery({
    queryKey: ['statistics', year, month],
    queryFn: async () => {
      const response = await statisticsApi.getByCategory(year, month);
      return response.data;
    },
  });

  const totals = useMemo(() => {
    if (!transactions) return { income: 0, expense: 0, balance: 0 };
    const income = transactions.filter(t => t.type === 'Income').reduce((s, t) => s + t.amount, 0);
    const expense = transactions.filter(t => t.type === 'Expense').reduce((s, t) => s + t.amount, 0);
    return { income, expense, balance: income - expense };
  }, [transactions]);

  const pieData = useMemo(() => {
    if (!stats) return [];
    return stats.map(s => ({ name: `${s.categoryIcon} ${s.categoryName}`, value: s.total }));
  }, [stats]);

  const barData = useMemo(() => {
    if (!transactions) return [];
    const months: Record<string, { income: number; expense: number }> = {};
    for (let i = 5; i >= 0; i--) {
      const d = new Date(year, month - 1 - i, 1);
      const key = d.toLocaleString('ru', { month: 'short' });
      months[key] = { income: 0, expense: 0 };
    }
    transactions.forEach(t => {
      const d = new Date(t.date);
      const key = d.toLocaleString('ru', { month: 'short' });
      if (months[key]) {
        if (t.type === 'Income') months[key].income += t.amount;
        else months[key].expense += t.amount;
      }
    });
    return Object.entries(months).map(([month, val]) => ({
      месяц: month,
      доходы: val.income,
      расходы: val.expense,
    }));
  }, [transactions, year, month]);

  if (txLoading || statsLoading) {
    return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;
  }

  const statCards = [
    {
      label: 'Баланс',
      value: `₽ ${totals.balance.toLocaleString()}`,
      icon: <Wallet sx={{ fontSize: 32 }} />,
      gradient: 'linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%)',
      color: totals.balance >= 0 ? 'success.main' : 'error.main',
    },
    {
      label: 'Доходы',
      value: `₽ ${totals.income.toLocaleString()}`,
      icon: <TrendingUp sx={{ fontSize: 32 }} />,
      gradient: 'linear-gradient(135deg, #10b981 0%, #34d399 100%)',
      color: 'success.main',
    },
    {
      label: 'Расходы',
      value: `₽ ${totals.expense.toLocaleString()}`,
      icon: <TrendingDown sx={{ fontSize: 32 }} />,
      gradient: 'linear-gradient(135deg, #ef4444 0%, #f87171 100%)',
      color: 'error.main',
    },
  ];

  return (
    <Box>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        Дашборд
      </Typography>

      <Grid container spacing={3} sx={{ mb: 4 }}>
        {statCards.map((card) => (
          <Grid key={card.label} size={{ xs: 12, md: 4 }}>
            <Card
              sx={{
                position: 'relative',
                overflow: 'hidden',
                '&::before': {
                  content: '""',
                  position: 'absolute',
                  top: 0,
                  left: 0,
                  right: 0,
                  height: 4,
                  background: card.gradient,
                },
              }}
            >
              <CardContent sx={{ pt: 4 }}>
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
                  <Box>
                    <Typography variant="body2" color="text.secondary" gutterBottom>
                      {card.label}
                    </Typography>
                    <Typography variant="h5" sx={{ fontWeight: 700 }} color={card.color}>
                        {card.value}
                    </Typography>
                  </Box>
                  <Box
                    sx={{
                        p: 1.5,
                        borderRadius: 3,
                        background: card.label === 'Доходы'
                        ? 'rgba(16, 185, 129, 0.1)'
                        : card.label === 'Расходы'
                        ? 'rgba(239, 68, 68, 0.1)'
                        : 'rgba(99, 102, 241, 0.1)',
                        color: card.label === 'Доходы'
                        ? 'success.main'
                        : card.label === 'Расходы'
                        ? 'error.main'
                        : 'primary.main',
                    }}
                    >
                    {card.icon}
                  </Box>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3}>
        <Grid size={{ xs: 12, lg: 5 }}>
          <Card sx={{ height: '100%' }}>
            <CardContent>
              <Typography variant="h6" gutterBottom>Расходы по категориям</Typography>
              {pieData.length > 0 ? (
                <ResponsiveContainer width="100%" height={320}>
                  <PieChart>
                    <Pie
                      data={pieData}
                      cx="50%"
                      cy="50%"
                      innerRadius={60}
                      outerRadius={110}
                      paddingAngle={3}
                      dataKey="value"
                      label={({ name, value }) => `${name.split(' ')[1]}: ₽${value.toLocaleString()}`}
                    >
                      {pieData.map((_, index) => (
                        <Cell key={index} fill={COLORS[index % COLORS.length]} stroke="transparent" />
                      ))}
                    </Pie>
                    <Tooltip
                      formatter={(value: number) => `₽${value.toLocaleString()}`}
                      contentStyle={{
                        background: 'rgba(15, 23, 42, 0.9)',
                        border: '1px solid rgba(148, 163, 184, 0.2)',
                        borderRadius: 12,
                        backdropFilter: 'blur(8px)',
                      }}
                    />
                  </PieChart>
                </ResponsiveContainer>
              ) : (
                <Typography color="text.secondary" sx={{ textAlign: 'center', py: 8 }}>
                  Нет данных за этот месяц
                </Typography>
              )}
            </CardContent>
          </Card>
        </Grid>

        <Grid size={{ xs: 12, lg: 7 }}>
          <Card sx={{ height: '100%' }}>
            <CardContent>
              <Typography variant="h6" gutterBottom>Динамика за полгода</Typography>
              {barData.length > 0 ? (
                <ResponsiveContainer width="100%" height={320}>
                  <ComposedChart data={barData}>
                    <defs>
                      <linearGradient id="incomeGradient" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#10b981" stopOpacity={0.8} />
                        <stop offset="100%" stopColor="#10b981" stopOpacity={0.2} />
                      </linearGradient>
                      <linearGradient id="expenseGradient" x1="0" y1="0" x2="0" y2="1">
                        <stop offset="0%" stopColor="#ef4444" stopOpacity={0.8} />
                        <stop offset="100%" stopColor="#ef4444" stopOpacity={0.2} />
                      </linearGradient>
                    </defs>
                    <CartesianGrid strokeDasharray="3 3" stroke="rgba(148, 163, 184, 0.1)" />
                    <XAxis dataKey="месяц" stroke="#94a3b8" fontSize={12} />
                    <YAxis stroke="#94a3b8" fontSize={12} />
                    <Tooltip
                      formatter={(value: number) => `₽${value.toLocaleString()}`}
                      contentStyle={{
                        background: 'rgba(15, 23, 42, 0.9)',
                        border: '1px solid rgba(148, 163, 184, 0.2)',
                        borderRadius: 12,
                        backdropFilter: 'blur(8px)',
                      }}
                    />
                    <Legend />
                    <Bar dataKey="доходы" fill="url(#incomeGradient)" radius={[6, 6, 0, 0]} />
                    <Bar dataKey="расходы" fill="url(#expenseGradient)" radius={[6, 6, 0, 0]} />
                    <Area type="monotone" dataKey="доходы" stroke="#10b981" fill="transparent" strokeWidth={2} />
                  </ComposedChart>
                </ResponsiveContainer>
              ) : (
                <Typography color="text.secondary" sx={{ textAlign: 'center', py: 8 }}>
                  Нет данных
                </Typography>
              )}
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Box>
  );
};

export default DashboardPage;