import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import {
  Box, Card, CardContent, TextField, Button,
  Typography, Alert, Container,
} from '@mui/material';
import { Savings as SavingsIcon } from '@mui/icons-material';
import { authApi } from '../api/auth';
import { useAuthStore } from '../store/authStore';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { setTokens, setUser } = useAuthStore();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      const response = await authApi.login({ email, password });
      setTokens(response.data.accessToken, response.data.refreshToken);
      setUser(response.data.user);
      navigate('/');
    } catch (err: any) {
      setError(err.response?.data?.error || 'Ошибка входа');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box
      sx={{
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        background: 'linear-gradient(135deg, #0f172a 0%, #1e293b 50%, #0f172a 100%)',
      }}
    >
      <Container maxWidth="xs">
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
          <SavingsIcon sx={{ fontSize: 48, color: 'primary.main', mb: 2 }} />
          <Typography variant="h4" gutterBottom sx={{ fontWeight: 700 }}>
            Fine<span style={{ color: '#818cf8' }}>Budget</span>
          </Typography>

          <Card
            sx={{
              width: '100%',
              backdropFilter: 'blur(20px)',
              background: 'rgba(30, 41, 59, 0.7)',
              border: '1px solid rgba(148, 163, 184, 0.1)',
            }}
          >
            <CardContent sx={{ p: 4 }}>
              <Typography variant="h5" gutterBottom align="center" sx={{ fontWeight: 600 }}>
                Вход
              </Typography>
              {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}
              <form onSubmit={handleSubmit}>
                <TextField fullWidth label="Email" type="email" value={email}
                  onChange={(e) => setEmail(e.target.value)} margin="normal" required />
                <TextField fullWidth label="Пароль" type="password" value={password}
                  onChange={(e) => setPassword(e.target.value)} margin="normal" required />
                <Button fullWidth type="submit" variant="contained" sx={{ mt: 3, py: 1.5 }}
                  disabled={loading}>
                  {loading ? 'Вход...' : 'Войти'}
                </Button>
              </form>
              <Box sx={{ mt: 2, textAlign: 'center' }}>
                <Link to="/register" style={{ color: '#818cf8', textDecoration: 'none' }}>
                  Нет аккаунта? Зарегистрироваться
                </Link>
              </Box>
            </CardContent>
          </Card>
        </Box>
      </Container>
    </Box>
  );
};

export default LoginPage;