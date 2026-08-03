import { useState } from 'react';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import {
  AppBar, Box, Drawer, IconButton, List, ListItem, ListItemButton,
  ListItemIcon, ListItemText, Toolbar, Typography, Button, Divider,
  Avatar, Chip,
} from '@mui/material';
import {
  Menu as MenuIcon, Dashboard as DashboardIcon,
  Receipt as ReceiptIcon, Category as CategoryIcon,
  Logout as LogoutIcon, Savings as SavingsIcon,
} from '@mui/icons-material';
import { useAuthStore } from '../../store/authStore';

const DRAWER_WIDTH = 260;

const menuItems = [
  { text: 'Дашборд', icon: <DashboardIcon />, path: '/' },
  { text: 'Транзакции', icon: <ReceiptIcon />, path: '/transactions' },
  { text: 'Категории', icon: <CategoryIcon />, path: '/categories' },
];

const AppLayout = () => {
  const [mobileOpen, setMobileOpen] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout } = useAuthStore();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const getInitials = (name: string) => {
    return name
      .split(' ')
      .map(w => w[0])
      .join('')
      .toUpperCase()
      .slice(0, 2);
  };

  return (
    <Box sx={{ display: 'flex', minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar
        position="fixed"
        elevation={0}
        sx={{
          zIndex: (theme) => theme.zIndex.drawer + 1,
          backdropFilter: 'blur(12px)',
          background: 'rgba(15, 23, 42, 0.8)',
          borderBottom: '1px solid rgba(148, 163, 184, 0.1)',
        }}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            edge="start"
            onClick={() => setMobileOpen(!mobileOpen)}
            sx={{ mr: 2, display: { sm: 'none' } }}
          >
            <MenuIcon />
          </IconButton>

          <SavingsIcon sx={{ mr: 1.5, color: 'primary.main', fontSize: 28 }} />
          <Typography variant="h6" sx={{ flexGrow: 1, fontWeight: 700 }}>
            Fine<span style={{ color: '#818cf8' }}>Budget</span>
          </Typography>

          <Chip
            avatar={<Avatar sx={{ bgcolor: 'primary.main' }}>{getInitials(user?.displayName || '')}</Avatar>}
            label={user?.displayName}
            variant="outlined"
            sx={{
              mr: 2,
              borderColor: 'rgba(148, 163, 184, 0.2)',
              color: 'text.primary',
            }}
          />

          <Button
            color="inherit"
            onClick={handleLogout}
            startIcon={<LogoutIcon />}
            sx={{
              borderRadius: 3,
              color: 'text.secondary',
              '&:hover': { color: 'error.main', bgcolor: 'transparent' },
            }}
          >
            Выйти
          </Button>
        </Toolbar>
      </AppBar>

      <Drawer
        variant="permanent"
        sx={{
          width: DRAWER_WIDTH,
          display: { xs: 'none', sm: 'block' },
          '& .MuiDrawer-paper': {
            width: DRAWER_WIDTH,
            boxSizing: 'border-box',
            bgcolor: 'transparent',
            borderRight: '1px solid rgba(148, 163, 184, 0.1)',
            backdropFilter: 'blur(12px)',
            background: 'rgba(15, 23, 42, 0.6)',
          },
        }}
      >
        <Toolbar />
        <Box sx={{ overflow: 'auto', mt: 2, px: 1.5 }}>
          <List>
            {menuItems.map((item) => {
              const isActive = location.pathname === item.path;
              return (
                <ListItem key={item.text} disablePadding sx={{ mb: 0.5 }}>
                  <ListItemButton
                    onClick={() => navigate(item.path)}
                    sx={{
                      borderRadius: 3,
                      bgcolor: isActive ? 'rgba(99, 102, 241, 0.15)' : 'transparent',
                      color: isActive ? 'primary.light' : 'text.secondary',
                      '&:hover': {
                        bgcolor: 'rgba(99, 102, 241, 0.1)',
                        color: 'primary.light',
                      },
                      transition: 'all 0.2s',
                    }}
                  >
                    <ListItemIcon sx={{ color: isActive ? 'primary.light' : 'text.secondary', minWidth: 40 }}>
                      {item.icon}
                    </ListItemIcon>
                    <ListItemText
                        primary={item.text}
                        slotProps={{ primary: { sx: { fontWeight: isActive ? 600 : 400 } } }}
                    />
                    {isActive && (
                      <Box sx={{ width: 3, height: 20, bgcolor: 'primary.main', borderRadius: 2, ml: 1 }} />
                    )}
                  </ListItemButton>
                </ListItem>
              );
            })}
          </List>

          <Divider sx={{ my: 2, borderColor: 'rgba(148, 163, 184, 0.1)' }} />

          <List>
            <ListItem disablePadding>
              <ListItemButton
                onClick={handleLogout}
                sx={{
                  borderRadius: 3,
                  color: 'text.secondary',
                  '&:hover': { color: 'error.light', bgcolor: 'rgba(239, 68, 68, 0.1)' },
                }}
              >
                <ListItemIcon sx={{ color: 'text.secondary', minWidth: 40 }}>
                  <LogoutIcon />
                </ListItemIcon>
                <ListItemText primary="Выйти" />
              </ListItemButton>
            </ListItem>
          </List>
        </Box>
      </Drawer>

      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 3,
          width: { sm: `calc(100% - ${DRAWER_WIDTH}px)` },
          minHeight: '100vh',
        }}
      >
        <Toolbar />
        <Box sx={{ maxWidth: 1200, mx: 'auto' }}>
          <Outlet />
        </Box>
      </Box>
    </Box>
  );
};

export default AppLayout;