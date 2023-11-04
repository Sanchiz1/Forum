import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { getUserAccount } from '../../Redux/Epics/AccountEpics';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../Redux/store';
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar, Collapse, TextField, Alert, Link, MenuItem, Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { Link as RouterLink } from 'react-router-dom';
import { Post } from '../../Types/Post';
import { deletePostRequest, requestPostById, updatePostRequest } from '../../API/postRequests';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import EditIcon from '@mui/icons-material/Edit';
import Divider from '@mui/material/Divider';
import DeleteIcon from '@mui/icons-material/Delete';
import ArchiveIcon from '@mui/icons-material/Archive';
import FileCopyIcon from '@mui/icons-material/FileCopy';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import { StyledMenu } from '../StyledMenu';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { enqueueSnackbar } from 'notistack';

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
    const { state } = useLocation();
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



    //menu
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClickMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleCloseMenu = () => {
        setAnchorEl(null);
    };


    //edit
    const [openEdit, setOpenEdit] = useState(false);
    const [error, setError] = useState<String>('');

    const handleSubmitEdit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const text = data.get('text')!.toString().trim();



        updatePostRequest(text, post?.id!).subscribe({
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
                navigator("/post/" + post?.id!);
            },
            error(err) {
                setError(err.message)
            },
        })
    }


    // delete
    const [openDelete, setOpenDelete] = useState(false);
    const handleSubmitDelete = () => {
        deletePostRequest(post?.id!).subscribe({
            next(value) {
                enqueueSnackbar(value, {
                    variant: 'success', anchorOrigin: {
                        vertical: 'top',
                        horizontal: 'center'
                    },
                    autoHideDuration: 1500
                });
                setError('');
                setOpenDelete(false);
                if (state == null) {
                    navigator('/');
                    return;
                }
                navigator(state);
            },
            error(err) {
                setError(err.message)
            },
        })
    }

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
                                    minHeight: '100vh',
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
                                                    <Link variant="caption" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + post.user_Username} color="primary" sx={{
                                                        mr: 0.5, textDecoration: 'none', cursor: 'pointer', color: 'white',
                                                        ":hover": {
                                                            textDecoration: 'underline'
                                                        }
                                                    }
                                                    } >
                                                        {post.user_Username}
                                                    </Link>
                                                    <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: 3, mr: 0.5 }}>
                                                        {'\u2B24'}
                                                    </Typography>
                                                    <Typography variant="caption" color="text.disabled" component="p">
                                                        {timeSince(GetLocalDate(new Date(post.date)))}
                                                    </Typography>
                                                    {post.user_Id == Account.id ? <>
                                                        <IconButton
                                                            aria-label="more"
                                                            id="long-button"
                                                            aria-controls={open ? 'long-menu' : undefined}
                                                            aria-expanded={open ? 'true' : undefined}
                                                            aria-haspopup="true"
                                                            onClick={handleClickMenu}
                                                            sx={{ ml: 'auto' }}
                                                        >
                                                            <MoreVertIcon />
                                                        </IconButton>
                                                        <StyledMenu
                                                            id="demo-customized-menu"
                                                            MenuListProps={{
                                                                'aria-labelledby': 'demo-customized-button',
                                                            }}
                                                            anchorEl={anchorEl}
                                                            open={open}
                                                            onClose={handleCloseMenu}
                                                        >
                                                            <MenuItem onClick={() => { setOpenEdit(true); handleCloseMenu(); }} disableRipple>
                                                                <EditIcon />
                                                                Edit
                                                            </MenuItem>
                                                            <MenuItem onClick={() => { setOpenDelete(true); handleCloseMenu(); }} disableRipple>
                                                                <DeleteIcon />
                                                                Delete
                                                            </MenuItem>
                                                        </StyledMenu>
                                                    </> : <></>}
                                                </Grid>
                                                <Typography variant="subtitle1" component="p">
                                                    {post.title}
                                                </Typography>
                                                <Divider sx={{ mb: 1 }} />
                                                {openEdit ?
                                                    <Box component="form" onSubmit={handleSubmitEdit} noValidate sx={{ mt: 1 }}>
                                                        <TextField
                                                            fullWidth
                                                            id="text"
                                                            label="Text"
                                                            name="text"
                                                            multiline
                                                            defaultValue={post.text}
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
                                                        <Box sx={{ my: 1, display: 'flex' }}>

                                                            <Button
                                                                color='secondary'
                                                                sx={{ ml: 'auto', mr: 1 }}
                                                                onClick={() => setOpenEdit(false)}
                                                                variant="outlined"
                                                            >
                                                                Cancel
                                                            </Button>
                                                            <Button
                                                                type="submit"
                                                                variant="outlined"
                                                            >
                                                                Submit
                                                            </Button>
                                                        </Box>
                                                    </Box>
                                                    :
                                                    <Typography variant="subtitle1" component="p" sx={{ mt: 2, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
                                                        {post.text}
                                                    </Typography>
                                                }
                                            </Paper>
                                        </Grid>
                                    </Grid>
                                    <Dialog
                                        open={openDelete}
                                        onClose={() => setOpenDelete(false)}
                                        aria-labelledby="alert-dialog-title"
                                        aria-describedby="alert-dialog-description"
                                    >
                                        <DialogTitle id="alert-dialog-title">
                                            {"Are You sure you want to delete this post?"}
                                        </DialogTitle>
                                        <DialogActions>
                                            <Button onClick={() => setOpenDelete(false)}>Cancel</Button>
                                            <Button onClick={() => handleSubmitDelete()} autoFocus>
                                                Delete
                                            </Button>
                                        </DialogActions>
                                    </Dialog>
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