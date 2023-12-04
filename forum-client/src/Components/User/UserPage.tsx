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
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../Redux/store';
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar, Collapse, TextField, Alert, Select, MenuItem, Skeleton, SelectChangeEvent } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import { GetDateString } from '../../Helpers/DateFormatHelper';
import { User, UserInput } from '../../Types/User';
import { updateUserRoleRequest, requestUserByUsername, updateUserRequest } from '../../API/userRequests';
import UserNotFoundPage from './UserNotFoundPage';
import { getAccount, setGlobalError } from '../../Redux/Reducers/AccountReducer';
import { SnackbarProvider, VariantType, enqueueSnackbar, useSnackbar } from 'notistack';
import { BootstrapInput } from '../UtilComponents/BootstrapInput';
import { Post } from '../../Types/Post';
import { requestUserPosts } from '../../API/postRequests';
import PostElement from '../Posts/PostElement';

const validUsernamePattern = /^[a-zA-Z0-9_.]+$/;
const validEmailPattern = /^(?=.{0,64}$)[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
const validPasswordPattern = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[a-zA-Z]).{8,21}$/;

const roles = {
  0: "User",
  1: "Admin",
  2: "Moderator",
};

export default function UserPage() {
  const [user, setUser] = useState<User>();
  const [role, setRole] = useState(0);
  const [userExists, setUserExists] = useState(true);
  const [openEdit, setOpenEdit] = useState(false);
  let { Username } = useParams();
  const dispatch = useDispatch();
  const navigator = useNavigate();
  const { state } = useLocation()

  const Account = useSelector((state: RootState) => state.account.Account);

  useEffect(() => {
    requestUserByUsername(Username!).subscribe({
      next(user) {
        setUser(user);
        setRole(user.role_Id)
        if (user === null) {
          setUserExists(false);
        }
        refetchposts()
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


  // Posts

  const next = 4;
  const [userTimestamp, setUserTimestamp] = useState(new Date());
  const [offset, setOffset] = useState(0);
  const [order, setOrder] = useState("Date_Created");
  const [posts, setPosts] = useState<Post[]>([]);
  const [hasMore, setHasMore] = useState(true);

  const refetchposts = () => {
    setPosts([]);
    setOffset(0);
    setUserTimestamp(new Date());
  }

  useEffect(() => {
    refetchposts()
  }, [order]);

  useEffect(() => {
    requestUserPosts(Username!, offset, next, order, userTimestamp).subscribe({
      next(value) {
        if (value.length == 0) {
          setHasMore(false);
          return;
        }
        setPosts([...posts, ...value])
        if (document.documentElement.offsetHeight - window.innerHeight < 100) {
          setOffset(offset + next);
        }
      },
      error(err) {
        setHasMore(false);
        dispatch(setGlobalError(err.message));
      },
    })
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, [offset]);

  function handleScroll() {
    if (window.innerHeight + document.documentElement.scrollTop <= document.documentElement.scrollHeight - 10 || !hasMore) return;
    setOffset(offset + next);
  }


  // edit role 
  const handleChange = (event: SelectChangeEvent) => {
    setRole(parseInt(event.target.value));
  };

  const handleSubmitEditRole = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    
    let role_id: number | null = role;
    if(role === 0){
      role_id = null;
    }
    updateUserRoleRequest(user?.id!, role_id).subscribe({
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
        navigator("/user/" + Username);
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
                  minHeight: '100vh',
                  overflow: 'auto',
                  display: 'flex'
                }}
              >
                <Container maxWidth='lg' sx={{
                  mt: 4, mb: 4, width: 1
                }}>
                  <Grid container spacing={2}>
                    <Grid item xs={12} md={4} lg={4}>
                      <Grid item xs={12}>
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
                                  <Typography variant="subtitle1" color="text.secondary" component="p" sx={{ mt: 2, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
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
                                      inputProps={{ maxLength: 100 }}
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
                            {(Account.role === 'Admin' && user.role !== 'Admin') ?
                              <>
                                <Divider sx={{ mt: 2 }} />
                                <Button onClick={() => setOpenEdit(!openEdit)}>Edit</Button>
                                <Collapse in={openEdit}>
                                  <Box component="form" onSubmit={handleSubmitEditRole} noValidate sx={{ mt: 1 }}>
                                    <Select
                                      labelId="role-label"
                                      id="role"
                                      value={role.toString()}
                                      fullWidth
                                      onChange={handleChange}
                                      sx={{mb: 2}}
                                    >
                                      <MenuItem value={0}>User</MenuItem>
                                      <MenuItem value={1}>Moderator</MenuItem>
                                      <MenuItem value={2}>Admin</MenuItem>
                                    </Select>
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
                    </Grid>

                    <Grid item xs={12} md={8} lg={8}>
                      <Grid item xs={12} sx={{ mb: 2 }}>
                        <Paper
                          sx={{
                            p: 1,
                            display: 'flex',
                            flexDirection: 'column',
                            height: 1,
                          }}
                        >
                          <Grid item xs={12} sx={{ mb: 2 }}>
                            <Typography variant="subtitle1" color="text.primary" component="p">
                            {user.posts} posts 
                            </Typography>
                            <Typography variant="subtitle1" color="text.primary" component="p">
                            {user.comments} comments 
                            </Typography>
                          </Grid>
                          <Divider sx={{ mb: 1 }} />
                          <Grid item xs={12} sx={{
                            width: 1,
                            display: 'flex',
                            flexDirection: 'row',
                            alignItems: 'stretch',
                          }}>
                            <Typography variant="subtitle1" color="text.primary" component="p" sx={{ display: 'flex', alignItems: 'center' }}>
                              {user.username}`s posts
                            </Typography>
                            <Typography variant="subtitle1" sx={{ ml: 'auto', display: 'flex', alignItems: 'center' }}>
                              <Select
                                value={order}
                                onChange={(e) => setOrder(e.target.value)}
                                input={<BootstrapInput sx={{ height: 1, display: 'flex' }} />}
                              >
                                <MenuItem value={"Likes"}>Top</MenuItem>
                                <MenuItem value={"Date_Created"}>New</MenuItem>
                              </Select>
                            </Typography>
                          </Grid>
                        </Paper>
                      </Grid>
                      {
                        posts?.map((post, index) =>
                          <PostElement
                            post={post}
                            key={index}
                            customClickEvent={(event: React.MouseEvent<HTMLDivElement>) => navigator('/post/' + post.id, { state: state })}
                            sx={{ mb: 2 }}
                          ></PostElement>
                        )
                      }
                      <Grid item xs={12} md={12} lg={12}>
                        {
                          hasMore ?
                            <Paper sx={{
                              p: 1,
                              width: 1,
                              ":hover": {
                                boxShadow: 5
                              }
                            }}>
                              <Skeleton width="10%" animation="wave" sx={{ fontSize: '10px' }} />
                              <Skeleton width="30%" animation="wave" />
                              <Divider />
                              <Skeleton animation="wave" height={40} />
                            </Paper>
                            :
                            <></>
                        }
                      </Grid>
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