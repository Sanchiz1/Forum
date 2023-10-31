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
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar, Collapse, TextField, Alert, Link } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';
import CloseIcon from '@mui/icons-material/Close';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import { GetDateString } from '../../Helpers/DateFormatHelper';
import { SnackbarProvider, VariantType, enqueueSnackbar, useSnackbar } from 'notistack';
import { Post } from '../../Types/Post';
import { requestPostById } from '../../API/postRequests';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';

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

export default function PostPage() {
    const [post, setPost] = useState<Post>();
    const [postExists, setPostExists] = useState(true);
    let { PostId } = useParams();
    const dispatch = useDispatch();
    const navigator = useNavigate();

    const Account = useSelector((state: RootState) => state.account.Account);

    useEffect(() => {
        requestPostById(parseInt(PostId!)).subscribe({
            next(post) {
                setPost(post);
                if (post === null) {
                    setPostExists(false);
                }
            },
            error(err) {
            },
        })
    }, [PostId])




    return (
        <>
            {postExists ?
                <>
                    {post ?
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
                                        <Grid item xs={12} md={12} lg={12}>
                                            <Paper sx={{
                                                p: 1,
                                                width: 1,
                                            }}>
                                                <Grid sx={{
                                                    display: 'flex',
                                                    flexDirection: 'row',
                                                    alignItems: 'center'
                                                }}>
                                                    <Link variant="subtitle1" component={RouterLink} to={'/user/' + post.user_Username} color="primary" sx={{
                                                        mr: 1, textDecoration: 'none', cursor: 'pointer', color: 'white',
                                                        ":hover": {
                                                            textDecoration: 'underline'
                                                        }
                                                    }
                                                    } >
                                                        {post.user_Username}
                                                    </Link>
                                                    <Typography variant="subtitle2" color="text.disabled" component="p" sx={{ fontSize: 5, mr: 0.2 }}>
                                                        {'\u2B24'}
                                                    </Typography>
                                                    <Typography variant="subtitle2" color="text.disabled" component="p">
                                                        {timeSince(GetLocalDate(new Date(post.date)))}
                                                    </Typography>
                                                </Grid>
                                                <Typography variant="subtitle1" color="text.disabled" component="p">
                                                    {post.title}
                                                </Typography>
                                                <Divider />
                                                <br />
                                                <Typography variant="subtitle1" color="text.disabled" component="p">
                                                    {post.text}
                                                </Typography>
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
                <></>
            }
        </>
    );
}