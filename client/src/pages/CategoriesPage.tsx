import { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  Box, Typography, Button, Table, TableBody, TableCell,
  TableContainer, TableHead, TableRow, Paper, IconButton,
  Dialog, DialogTitle, DialogContent, DialogActions,
  TextField, Select, MenuItem, FormControl, InputLabel,
  Alert, CircularProgress,
} from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon, Add as AddIcon } from '@mui/icons-material';
import { categoriesApi, type Category, type CreateCategoryData } from '../api/categories';

const CategoriesPage = () => {
  const [openDialog, setOpenDialog] = useState(false);
  const [editingCategory, setEditingCategory] = useState<Category | null>(null);
  const [formData, setFormData] = useState({ name: '', icon: '📦', defaultType: 2 });
  const [error, setError] = useState('');

  const queryClient = useQueryClient();

  const { data: categories, isLoading } = useQuery({
    queryKey: ['categories'],
    queryFn: async () => {
      const response = await categoriesApi.getAll();
      return response.data;
    },
  });

  const createMutation = useMutation({
    mutationFn: (data: CreateCategoryData) => categoriesApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['categories'] });
      handleCloseDialog();
    },
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка создания'),
  });

  const updateMutation = useMutation({
    mutationFn: (data: { id: string; name: string; icon: string; defaultType: number }) =>
      categoriesApi.update(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['categories'] });
      handleCloseDialog();
    },
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка обновления'),
  });

  const deleteMutation = useMutation({
    mutationFn: (id: string) => categoriesApi.delete(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['categories'] }),
    onError: (err: any) => setError(err.response?.data?.error || 'Ошибка удаления'),
  });

  const handleOpenCreate = () => {
    setEditingCategory(null);
    setFormData({ name: '', icon: '📦', defaultType: 2 });
    setError('');
    setOpenDialog(true);
  };

  const handleOpenEdit = (category: Category) => {
    setEditingCategory(category);
    setFormData({
      name: category.name,
      icon: category.icon,
      defaultType: category.defaultType === 'Income' ? 1 : 2,
    });
    setError('');
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
    setEditingCategory(null);
  };

  const handleSubmit = () => {
    if (!formData.name.trim()) {
      setError('Название обязательно');
      return;
    }

    if (editingCategory) {
      updateMutation.mutate({ id: editingCategory.id, ...formData });
    } else {
      createMutation.mutate(formData);
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Удалить категорию?')) {
      deleteMutation.mutate(id);
    }
  };

  if (isLoading) {
    return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;
  }

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4">Категории</Typography>
        <Button variant="contained" startIcon={<AddIcon />} onClick={handleOpenCreate}>
          Добавить
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Иконка</TableCell>
              <TableCell>Название</TableCell>
              <TableCell>Тип по умолчанию</TableCell>
              <TableCell align="right">Действия</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {categories?.map((cat) => (
              <TableRow key={cat.id}>
                <TableCell>{cat.icon}</TableCell>
                <TableCell>{cat.name}</TableCell>
                <TableCell>{cat.defaultType === 'Income' ? 'Доход' : 'Расход'}</TableCell>
                <TableCell align="right">
                  <IconButton onClick={() => handleOpenEdit(cat)}><EditIcon /></IconButton>
                  <IconButton onClick={() => handleDelete(cat.id)} color="error"><DeleteIcon /></IconButton>
                </TableCell>
              </TableRow>
            ))}
            {categories?.length === 0 && (
              <TableRow>
                <TableCell colSpan={4} align="center">Нет категорий</TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>{editingCategory ? 'Редактировать' : 'Новая категория'}</DialogTitle>
        <DialogContent>
          {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
          <TextField fullWidth label="Название" value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            margin="normal" autoFocus />
          <TextField fullWidth label="Иконка (emoji)" value={formData.icon}
            onChange={(e) => setFormData({ ...formData, icon: e.target.value })}
            margin="normal" helperText="Например: 🛒 💰 🚗" />
          <FormControl fullWidth margin="normal">
            <InputLabel>Тип по умолчанию</InputLabel>
            <Select value={formData.defaultType}
              onChange={(e) => setFormData({ ...formData, defaultType: Number(e.target.value) })}
              label="Тип по умолчанию">
              <MenuItem value={1}>Доход</MenuItem>
              <MenuItem value={2}>Расход</MenuItem>
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

export default CategoriesPage;