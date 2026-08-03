import { useState, useMemo } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  Box, Typography, Button, Table, TableBody, TableCell,
  TableContainer, TableHead, TableRow, Paper, IconButton,
  Dialog, DialogTitle, DialogContent, DialogActions,
  TextField, Select, MenuItem, FormControl, InputLabel,
  Alert, CircularProgress, Chip,
} from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon, Add as AddIcon } from '@mui/icons-material';
import { transactionsApi, type Transaction, type CreateTransactionData } from '../api/transactions';
import { categoriesApi, type Category } from '../api/categories';

const TransactionsPage = () => {
  const now = new Date();
  const [year, setYear] = useState(now.getFullYear());
  const [month, setMonth] = useState(now.getMonth() + 1);
  const [openDialog, setOpenDialog] = useState(false);
  const [editingTransaction, setEditingTransaction] = useState<Transaction | null>(null);
  const [formData, setFormData] = useState({
    amount: 0, description: '', date: new Date().toISOString().split('T')[0],
    type: 2, categoryId: '',
  });
  const [error, setError] = useState('');

  const queryClient = useQueryClient();

  const { data: transactions, isLoading } = useQuery({
    queryKey: ['transactions', year, month],
    queryFn: async () => {
      const response = await transactionsApi.getByMonth(year, month);
      return response.data;
    },
  });

  const { data: categories } = useQuery({
    queryKey: ['categories'],
    queryFn: async () => {
      const response = await categoriesApi.getAll();
      return response.data;
    },
  });

  const createMutation = useMutation({
    mutationFn: (data: CreateTransactionData) => transactionsApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['transactions'] });
      handleCloseDialog();
    },
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка создания'),
  });

  const updateMutation = useMutation({
    mutationFn: (data: { id: string; amount: number; description: string; date: string; type: number; categoryId: string }) =>
      transactionsApi.update(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['transactions'] });
      handleCloseDialog();
    },
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка обновления'),
  });

  const deleteMutation = useMutation({
    mutationFn: (id: string) => transactionsApi.delete(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['transactions'] }),
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка удаления'),
  });

  // Суммы за месяц
  const totals = useMemo(() => {
    if (!transactions) return { income: 0, expense: 0, balance: 0 };
    const income = transactions.filter(t => t.type === 'Income').reduce((s, t) => s + t.amount, 0);
    const expense = transactions.filter(t => t.type === 'Expense').reduce((s, t) => s + t.amount, 0);
    return { income, expense, balance: income - expense };
  }, [transactions]);

  const handleOpenCreate = () => {
    setEditingTransaction(null);
    setFormData({ amount: 0, description: '', date: new Date().toISOString().split('T')[0], type: 2, categoryId: categories?.[0]?.id || '' });
    setError('');
    setOpenDialog(true);
  };

  const handleOpenEdit = (tx: Transaction) => {
    setEditingTransaction(tx);
    setFormData({
      amount: tx.amount, description: tx.description,
      date: tx.date.split('T')[0],
      type: tx.type === 'Income' ? 1 : 2,
      categoryId: tx.categoryId,
    });
    setError('');
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
    setEditingTransaction(null);
  };

  const handleSubmit = () => {
    if (formData.amount <= 0) { setError('Сумма должна быть больше нуля'); return; }
    if (!formData.description.trim()) { setError('Описание обязательно'); return; }
    if (!formData.categoryId) { setError('Выберите категорию'); return; }

    if (editingTransaction) {
      updateMutation.mutate({ id: editingTransaction.id, ...formData });
    } else {
      createMutation.mutate(formData);
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Удалить транзакцию?')) {
      deleteMutation.mutate(id);
    }
  };

  if (isLoading) {
    return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;
  }

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3, flexWrap: 'wrap', gap: 2 }}>
        <Typography variant="h4">Транзакции</Typography>
        <Box sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
          <FormControl size="small">
            <InputLabel>Год</InputLabel>
            <Select value={year} onChange={(e) => setYear(Number(e.target.value))} label="Год">
              {[2024, 2025, 2026, 2027].map(y => <MenuItem key={y} value={y}>{y}</MenuItem>)}
            </Select>
          </FormControl>
          <FormControl size="small">
            <InputLabel>Месяц</InputLabel>
            <Select value={month} onChange={(e) => setMonth(Number(e.target.value))} label="Месяц">
              {Array.from({ length: 12 }, (_, i) => i + 1).map(m =>
                <MenuItem key={m} value={m}>{new Date(2000, m - 1).toLocaleString('ru', { month: 'long' })}</MenuItem>
              )}
            </Select>
          </FormControl>
          <Button variant="contained" startIcon={<AddIcon />} onClick={handleOpenCreate}>
            Добавить
          </Button>
        </Box>
      </Box>

      {/* Карточки с итогами */}
      <Box sx={{ display: 'flex', gap: 2, mb: 3, flexWrap: 'wrap' }}>
        <Paper sx={{ p: 2, flex: 1, minWidth: 150 }}>
          <Typography color="text.secondary">Доходы</Typography>
          <Typography variant="h5" color="success.main">₽ {totals.income.toLocaleString()}</Typography>
        </Paper>
        <Paper sx={{ p: 2, flex: 1, minWidth: 150 }}>
          <Typography color="text.secondary">Расходы</Typography>
          <Typography variant="h5" color="error.main">₽ {totals.expense.toLocaleString()}</Typography>
        </Paper>
        <Paper sx={{ p: 2, flex: 1, minWidth: 150 }}>
          <Typography color="text.secondary">Баланс</Typography>
          <Typography variant="h5" color={totals.balance >= 0 ? 'success.main' : 'error.main'}>
            ₽ {totals.balance.toLocaleString()}
          </Typography>
        </Paper>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Дата</TableCell>
              <TableCell>Категория</TableCell>
              <TableCell>Описание</TableCell>
              <TableCell align="right">Сумма</TableCell>
              <TableCell align="right">Действия</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {transactions?.map((tx) => (
              <TableRow key={tx.id}>
                <TableCell>{new Date(tx.date).toLocaleDateString('ru')}</TableCell>
                <TableCell>{tx.categoryIcon} {tx.categoryName}</TableCell>
                <TableCell>{tx.description}</TableCell>
                <TableCell align="right">
                  <Chip
                    label={`${tx.type === 'Income' ? '+' : '-'}₽${tx.amount.toLocaleString()}`}
                    color={tx.type === 'Income' ? 'success' : 'error'}
                    size="small"
                  />
                </TableCell>
                <TableCell align="right">
                  <IconButton onClick={() => handleOpenEdit(tx)}><EditIcon /></IconButton>
                  <IconButton onClick={() => handleDelete(tx.id)} color="error"><DeleteIcon /></IconButton>
                </TableCell>
              </TableRow>
            ))}
            {transactions?.length === 0 && (
              <TableRow>
                <TableCell colSpan={5} align="center">Нет транзакций за этот месяц</TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Диалог */}
      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>{editingTransaction ? 'Редактировать' : 'Новая транзакция'}</DialogTitle>
        <DialogContent>
          {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
          <FormControl fullWidth margin="normal">
            <InputLabel>Тип</InputLabel>
            <Select value={formData.type}
              onChange={(e) => setFormData({ ...formData, type: Number(e.target.value) })} label="Тип">
              <MenuItem value={2}>Расход</MenuItem>
              <MenuItem value={1}>Доход</MenuItem>
            </Select>
          </FormControl>
          <TextField fullWidth label="Сумма" type="number" value={formData.amount || ''}
            onChange={(e) => setFormData({ ...formData, amount: Number(e.target.value) })}
            margin="normal" />
          <TextField fullWidth label="Описание" value={formData.description}
            onChange={(e) => setFormData({ ...formData, description: e.target.value })}
            margin="normal" />
          <TextField fullWidth label="Дата" type="date" value={formData.date}
            onChange={(e) => setFormData({ ...formData, date: e.target.value })}
            margin="normal" slotProps={{ inputLabel: { shrink: true } }} />
          <FormControl fullWidth margin="normal">
            <InputLabel>Категория</InputLabel>
            <Select value={formData.categoryId}
              onChange={(e) => setFormData({ ...formData, categoryId: e.target.value })}
              label="Категория">
              {categories?.map((cat) => (
                <MenuItem key={cat.id} value={cat.id}>{cat.icon} {cat.name}</MenuItem>
              ))}
            </Select>
          </FormControl>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDialog}>Отмена</Button>
          <Button onClick={handleSubmit} variant="contained"
            disabled={createMutation.isPending || updateMutation.isPending}>
            {createMutation.isPending || updateMutation.isPending ? 'Сохранение...' : 'Сохранить'}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default TransactionsPage;