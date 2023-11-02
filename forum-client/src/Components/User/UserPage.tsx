import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { getUserAccount } from '../../Redux/Epics/AccountEpics';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../Redux/store';
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar, Collapse, TextField, Alert } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import { GetDateString } from '../../Helpers/DateFormatHelper';
import { User, UserInput } from '../../Types/User';
import { requestUserByUsername, updateUserRequest } from '../../API/userRequests';
import UserNotFoundPage from './UserNotFoundPage';
import { getAccount } from '../../Redux/Reducers/AccountReducer';
import { SnackbarProvider, VariantType, enqueueSnackbar, useSnackbar } from 'notistack';

const validUsernamePattern = /^[a-zA-Z0-9_.]+$/;
const validEmailPattern = /^(?=.{0,64}$)[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
const validPasswordPattern = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[a-zA-Z]).{8,21}$/;

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
  const [openEdit, setOpenEdit] = useState(false);
  let { Username } = useParams();
  const dispatch = useDispatch();
  const navigator = useNavigate();

  const Account = useSelector((state: RootState) => state.account.Account);

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



  //Edit
  const [usernameError, SetUsernameError] = useState('');
  const [emailError, SetEmailError] = useState('');
  const [bioError, SetBioError] = useState('');
  const [error, setError] = useState<String>('');

  const handleSubmitEdit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    const username = data.get('username')!.toString().trim();
    const email = data.get('email')!.toString().trim();
    const bio = data.get('bio')!.toString().trim();

    if (!validUsernamePattern.test(username)) {
      SetUsernameError('Invalid username');
      return;
    }
    if (!validEmailPattern.test(email)) {
      SetEmailError('Invalid email');
      return;
    }
    if (bio.length > 100) {
      SetBioError('Bio can have maximum of 100 characters');
      return;
    }

    const userInput: UserInput = {
      username: username,
      email: email,
      bio: bio
    }

    updateUserRequest(userInput).subscribe({
      next(value) {
        enqueueSnackbar(value, {
          variant: 'success', anchorOrigin: {
            vertical: 'top',
            horizontal: 'center'
          },
          autoHideDuration: 1500
        });
        setError('');
        setOpenEdit(false);
        navigator("/user/" + userInput.username);
      },
      error(err) {
        setError(err.message)
      },
    })
  }
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
                  display: 'flex'
                }}
              >
                <Container maxWidth='lg' sx={{
                  mt: 4, mb: 4, width: 1
                }}>
                  <Grid container spacing={3}>
                    <Grid item xs={12} md={6} lg={4}>
                      <Paper
                        sx={{
                          p: 2,
                          display: 'flex',
                          flexDirection: 'column',
                          height: 1,
                        }}
                      >
                        <Grid sx={{
                          display: 'flex',
                          flexDirection: 'column',
                        }}>
                          <Typography variant="h4" color="text.secondary" component="p">
                            {user.username}
                          </Typography>
                          <Typography variant="subtitle1" color="text.secondary" component="p">
                            Joined: {GetDateString(new Date(user.registered_At))}
                          </Typography>
                          {
                            user.bio ?
                              <>
                                <Typography variant="subtitle1" color="text.secondary" component="p" sx={{ mt: 2, whiteSpace: 'pre-line', overflowWrap: 'break-word'}}>
                                  {user.bio}
                                </Typography>
                              </>
                              :
                              <></>
                          }
                          {(Account != null && user.id === Account.id) ?
                            <>
                              <Divider sx={{ mt: 2 }} />
                              <Button onClick={() => setOpenEdit(!openEdit)}>Edit</Button>
                              <Collapse in={openEdit}>
                                <Box component="form" onSubmit={handleSubmitEdit} noValidate sx={{ mt: 1 }}>
                                  <TextField
                                    margin="normal"
                                    required
                                    fullWidth
                                    id="username"
                                    label="Username"
                                    name="username"
                                    autoComplete="off"
                                    autoFocus
                                    defaultValue={Account.username}
                                    error={usernameError != ''}
                                    onFocus={() => SetUsernameError('')}
                                    helperText={usernameError}
                                  />
                                  <TextField
                                    margin="normal"
                                    required
                                    fullWidth
                                    id="email"
                                    label="Email address"
                                    name="email"
                                    autoComplete="off"
                                    autoFocus
                                    defaultValue={Account.email}
                                    error={emailError != ''}
                                    onFocus={() => SetEmailError('')}
                                    helperText={emailError}
                                  />
                                  <TextField
                                    margin="normal"
                                    fullWidth
                                    id="bio"
                                    label="Bio"
                                    name="bio"
                                    multiline
                                    rows={4}
                                    inputProps={{maxLength: 100}}
                                    defaultValue={Account.bio}
                                    error={bioError != ''}
                                    onFocus={() => SetBioError('')}
                                    helperText={bioError}
                                  />
                                  <Collapse in={error != ''}>
                                    <Alert
                                      severity="error"
                                      action={
                                        <IconButton
                                          aria-label="close"
                                          color="inherit"
                                          onClick={() => {
                                            setError('');
                                          }}
                                        >
                                          <CloseIcon />
                                        </IconButton>
                                      }
                                      sx={{ mb: 2, fontSize: 15 }}
                                    >
                                      {error}
                                    </Alert>
                                  </Collapse>
                                  <Button
                                    type="submit"
                                    fullWidth
                                    variant="outlined"
                                  >
                                    Submit
                                  </Button>
                                </Box>
                              </Collapse>
                            </>
                            : <></>}
                        </Grid>
                      </Paper>
                    </Grid>
                    <Grid item xs={12} md={6} lg={8}>
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