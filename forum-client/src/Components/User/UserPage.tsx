import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { getUserAccount } from '../../Redux/Epics/AccountEpics';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../Redux/store';
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar } from '@mui/material';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import { GetDateString } from '../../Helpers/DateFormatHelper';
import { User } from '../../Types/User';
import { requestUserByUsername } from '../../API/userRequests';
import UserNotFoundPage from './UserNotFoundPage';

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

export default function UserPage() {
  const [user, setUser] = useState<User>();
  const [userExists, setUserExists] = useState(true);
  let { Username } = useParams();
  const dispatch = useDispatch();
  const navigator = useNavigate();

  useEffect(() => {
    requestUserByUsername(Username!).subscribe({
      next(user) {
        setUser(user);
        if (user === null) {
          setUserExists(false);
        }
      },
      error(err) {
      },
    })
  }, [Username])


  return (
    <>
      {userExists ?
        <>
          {user != undefined ?
            <Box sx={{ display: 'flex' }}>
              <CssBaseline />
              <Box
                component="main"
                sx={{
                  backgroundColor: (theme) =>
                    theme.palette.mode === 'light'
                      ? theme.palette.grey[100]
                      : theme.palette.grey[900],
                  flexGrow: 1,
                  height: '100vh',
                  overflow: 'auto',
                }}
              >
                <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                  <Grid container spacing={3}>
                    <Grid item xs={12} md={4} lg={3}>
                      <Paper
                        sx={{
                          p: 2,
                          display: 'flex',
                          flexDirection: 'column',
                          height: 1,
                        }}
                      >
                        <Avatar sx={{ width: 100, height: 100, fontSize: 50, mx: 'auto' }}>{user!.username.charAt(0).toUpperCase()}</Avatar>
                        <Typography variant="h5" color="text.secondary" component="p" sx={{ my: 1 }}>
                          {user.username}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary" component="p">
                          Registered: {GetDateString(new Date(user.registered_At))}
                        </Typography>
                        {
                          user.bio ?
                            <Typography variant="subtitle1" color="text.secondary" component="p">
                              user.bio
                            </Typography>
                            :
                            <></>
                        }
                      </Paper>
                    </Grid>

                    <Grid item xs={12} md={8} lg={9}>
                      <Paper
                        sx={{
                          p: 2,
                          display: 'flex',
                          flexDirection: 'column',
                          height: 1,
                        }}
                      >
                        2
                      </Paper>
                    </Grid>
                    <Grid item xs={12}>
                      <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                      </Paper>
                    </Grid>
                  </Grid>
                </Container>
              </Box>
            </Box>
            :
            <Box sx={{ width: '100%' }}>
              <LinearProgress />
            </Box>
          }
        </>
        :
        <UserNotFoundPage></UserNotFoundPage>
      }
    </>
  );
}