// packages
import { useState } from 'react';
import { NavLink } from 'react-router';
import {
  AccountCircle as AccountCircleIcon,
  DarkMode as DarkModeIcon,
  Email as EmailIcon,
  Landscape as LandscapeIcon,
  LightMode as LightModeIcon,
  Logout as LogoutIcon,
} from '@mui/icons-material';
import {
  AppBar,
  Box,
  Divider,
  Fade,
  IconButton,
  ListItemIcon,
  ListItemText,
  Menu,
  MenuItem,
  Switch,
  Toolbar,
  Typography,
  type PaletteMode,
} from '@mui/material';

type AppHeaderProps = {
  username: string;
  email: string;
  logout: () => void;
  togglePaletteMode: () => void;
  paletteMode: PaletteMode;
};

const navStyles = {
  color: 'inherit',
  typography: 'h6',
  textDecoration: 'none',
  '&:hover': { color: 'grey.500' },
  '&.active': {
    color: '#ffffff',
  },
};

export const AppHeader: React.FC<AppHeaderProps> = ({
  username,
  email,
  logout,
  togglePaletteMode,
  paletteMode,
}) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <AppBar
      position='fixed'
      sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}
    >
      <Toolbar
        sx={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
        }}
      >
        <Box display='flex' alignItems='center'>
          <LandscapeIcon
            sx={{ display: { xs: 'none', md: 'flex' }, mr: 1, fontSize: 48 }}
          />
          <Typography
            variant='h6'
            component={NavLink}
            to='/dashboard'
            sx={navStyles}
          >
            Offgrid - Admin Portal
          </Typography>
          <IconButton sx={{ mx: 2 }}>
            {paletteMode === 'dark' ? (
              <DarkModeIcon />
            ) : (
              <LightModeIcon sx={{ color: '#FFF59D' }} />
            )}
            <Switch onChange={togglePaletteMode} />
          </IconButton>
        </Box>
        <div>
          <IconButton size='large' onClick={handleMenu} color='inherit'>
            <AccountCircleIcon />
            <Typography variant='subtitle1' component='span' sx={{ ml: 1 }}>
              {username}
            </Typography>
          </IconButton>
          <Menu
            id='fade-menu'
            anchorEl={anchorEl}
            open={open}
            onClose={handleClose}
            TransitionComponent={Fade}
          >
            <MenuItem>
              <ListItemIcon>
                <EmailIcon />
              </ListItemIcon>
              <ListItemText>{email}</ListItemText>
            </MenuItem>
            <Divider />
            <MenuItem onClick={logout}>
              <ListItemIcon>
                <LogoutIcon />
              </ListItemIcon>
              <ListItemText>Logout</ListItemText>
            </MenuItem>
          </Menu>
        </div>
      </Toolbar>
    </AppBar>
  );
};
