import React, { useEffect, useState } from 'react';
import Toolbar from '@mui/material/Toolbar';
import { Outlet, useLocation, useNavigate, Link as RouterLink } from 'react-router-dom';
import { LogoutRequest, isSigned } from '../API/loginRequests';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import ListItemIcon from '@mui/material/ListItemIcon';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Settings from '@mui/icons-material/Settings';
import Logout from '@mui/icons-material/Logout';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { AppBar, Container } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../Redux/store';
import { Backdrop, CircularProgress } from '@mui/material';
import { getAccount, setLogInError } from '../Redux/Reducers/AccountReducer';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Link from '@mui/material/Link';
import { getUserAccount } from '../Redux/Epics/AccountEpics';
import { setCookie } from '../Helpers/CookieHelper';
import { User } from '../Types/User';

export default function Header() {
  const dispatch = useDispatch();
  const navigator = useNavigate();
  const location = useLocation();
  const User = useSelector((state: RootState) => state.account.Account);
  const error = useSelector((state: RootState) => state.account.LogInError);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  useEffect(() => {
    setCookie({ name: "refresh_sent", value: "false" })
    if (isSigned()) {
      dispatch(getUserAccount())
    }
  }, [])

  //Sign in error
  const handleErrorClose = () => {
    dispatch(setLogInError(''))
  };
  //Sign in error


  //Account menu
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };
  //Account menu

  return (
    <React.Fragment>
      <Backdrop
        sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={error == "Not signed in"}
        onClick={handleErrorClose}
      >
        <Card sx={{ minWidth: 275 }}>
          <CardContent>
            <Typography sx={{ fontSize: 30, textAlign: 'center' }} gutterBottom>
              Sign in
            </Typography>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onClick={() => navigator("/Sign-in", { state: location })}
            >
              Sign In
            </Button>
            <Link onClick={() => navigator("/Sign-up", { state: location })} sx={{ cursor: 'pointer' }} variant="body2">
              {"Don't have an account? Sign Up"}
            </Link>
          </CardContent>
        </Card>
      </Backdrop>
      <Backdrop
        sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={error == "Invalid token"}
        onClick={handleErrorClose}
      >
        <Card sx={{ minWidth: 275 }}>
          <CardContent>
            <Typography sx={{ fontSize: 30, textAlign: 'center' }} gutterBottom>
              Invalid token,
            </Typography>
            <Typography variant="subtitle2" align="center" color="text.secondary" component="p" gutterBottom>
              Provided token is invalid, please sign in again
            </Typography>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onClick={() => navigator("/Sign-in", { state: location })}
            >
              Sign In
            </Button>
            <Link href="#" variant="body2" onClick={() => navigator("/Sign-up", { state: location })}>
              {"Don't have an account? Sign Up"}
            </Link>
          </CardContent>
        </Card>
      </Backdrop>
      <AppBar position="sticky">
        <Toolbar>
          <Link component={RouterLink} variant="h5" to={'/'}
            sx={{ mr: 'auto', textDecoration: 'none', color: 'text.secondary' }}>
            Forum
          </Link>
          <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
            {isSigned() ?
              <>
                {User.username ?
                  <IconButton
                    onClick={handleClick}
                    size="small"
                    sx={{ ml: 2 }}
                    aria-controls={open ? 'account-menu' : undefined}
                    aria-haspopup="true"
                    aria-expanded={open ? 'true' : undefined}
                  >
                    <Avatar>{User!.username.charAt(0).toUpperCase()}</Avatar>

                  </IconButton>
                  :
                  <AccountCircleIcon />
                }</>
              :
              <Button variant="outlined" onClick={() => navigator("/Sign-in", { state: location })}>Sign in</Button>
            }

          </Box>
          <Menu
            anchorEl={anchorEl}
            id="account-menu"
            open={open}
            onClose={handleClose}
            onClick={handleClose}
            PaperProps={{
              elevation: 0,
              sx: {
                overflow: 'visible',
                filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                mt: 1.5,
                '& .MuiAvatar-root': {
                  width: 32,
                  height: 32,
                  ml: -0.5,
                  mr: 1,
                },
                '&:before': {
                  content: '""',
                  display: 'block',
                  position: 'absolute',
                  top: 0,
                  right: 14,
                  width: 10,
                  height: 10,
                  bgcolor: 'background.paper',
                  transform: 'translateY(-50%) rotate(45deg)',
                  zIndex: 0,
                },
              },
            }}
            transformOrigin={{ horizontal: 'right', vertical: 'top' }}
            anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
          >
            <MenuItem onClick={() => { navigator('/user/' + User.username); handleClose(); }}>
              <Avatar /> My account
            </MenuItem>
            <Divider />
            <MenuItem onClick={handleClose}>
              <ListItemIcon>
                <Settings fontSize="small" />
              </ListItemIcon>
              Settings
            </MenuItem>
            <MenuItem onClick={() => { LogoutRequest().subscribe(() => { dispatch(getAccount({} as User)); navigator(location) }); handleClose() }}>
              <ListItemIcon>
                <Logout fontSize="small" />
              </ListItemIcon>
              Logout
            </MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>
      <Outlet />
    </React.Fragment>
  );
}