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
import {
    FormControl, Select, Stack, Container, CssBaseline, IconButton, LinearProgress,
    Toolbar, Collapse, TextField, Alert, Link, MenuItem, Button, Dialog, DialogTitle, DialogActions
} from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
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
import { StyledMenu } from '../UtilComponents/StyledMenu';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { enqueueSnackbar } from 'notistack';
import { Reply } from '../../Types/Reply';
import { requestReplies } from '../../API/replyRequests';
import ReplyElement from '../Comments/ReplyElement';
import { createCommentRequest, requestComments } from '../../API/commentRequests';
import { Comment, CommentInput } from '../../Types/Comment';
import CommentElement from '../Comments/CommentElement';
import { BootstrapInput } from '../UtilComponents/BootstrapInput';
import { isSigned } from '../../API/loginRequests';
import { setGlobalError, setLogInError } from '../../Redux/Reducers/AccountReducer';
import { User } from '../../Types/User';
import CommentInputElement from '../UtilComponents/CommentInputElement';

const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(1),
    textAlign: 'center',
    color: theme.palette.text.secondary,
}));

interface CommentsSectionProps {
    Post: Post
}

export default function CommentsSection(Props: CommentsSectionProps) {

    const [comments, setComments] = useState<Comment[]>([]);

    const [hasMore, setHasMore] = useState(true);

    const next = 4;
    const [userTimestamp, setUserTimestamp] = useState(new Date());
    const [offset, setOffset] = useState(0);
    const [order, setOrder] = useState("Date");

    let { PostId } = useParams();
    const { state } = useLocation();
    const dispatch = useDispatch();
    const navigator = useNavigate();

    const Account: User = useSelector((state: RootState) => state.account.Account);


    const refetchComments = () => {
        setComments([]);
        setUserTimestamp(new Date())
        setOffset(0);
    }

    const fetchComments = () => {
        requestComments(parseInt(PostId!), offset, next, order, userTimestamp).subscribe({
            next(value) {
                if (value.length == 0) {
                    setHasMore(false);
                    return;
                }
                setComments([...comments, ...value]);
                if (document.documentElement.offsetHeight - window.innerHeight < 100) {
                    setOffset(offset + next);
                }
            },
            error(err) {
                dispatch(setGlobalError(err));
            },
        })
    }

    useEffect(() => {
        fetchComments()
        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, [offset])

    useEffect(() => {
        refetchComments()
    }, [order])

    function handleScroll() {
        if (window.innerHeight + document.documentElement.scrollTop <= document.documentElement.scrollHeight - 10 || !hasMore) return;
        setOffset(offset + next);
    }


    return (
        <Grid item xs={12} md={12} lg={12}>
            <Paper sx={{
                p: 1,
                width: 1
            }}>
                <Grid sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    alignItems: 'stretch',
                    py: 0,
                }}>
                    <Typography variant="caption" sx={{ mr: 1, fontSize: '18px', display: 'flex', alignItems: 'center' }}>
                        {Props.Post.comments.toString()} Comments
                    </Typography>
                    <Typography variant="caption" sx={{ fontSize: '15px', display: 'flex', alignItems: 'center' }}>
                        <Select
                            value={order}
                            onChange={(e) => setOrder(e.target.value)}
                            input={<BootstrapInput sx={{ height: 1, display: 'flex' }} />}
                        >
                            <MenuItem value={"Likes"}>Top</MenuItem>
                            <MenuItem value={"Date"}>New</MenuItem>
                        </Select>
                    </Typography>
                </Grid>
                <CommentInputElement
                    Action={(e: string) => {
                        if (e.trim() === '') return;
                        const commentInput: CommentInput = {
                            post_Id: Props.Post.id,
                            text: e,
                            user_Id: Account.id
                        }
                        createCommentRequest(commentInput).subscribe(
                            {
                                next(value) {
                                    enqueueSnackbar(value, {
                                        variant: 'success', anchorOrigin: {
                                            vertical: 'top',
                                            horizontal: 'center'
                                        },
                                        autoHideDuration: 1500
                                    });
                                    refetchComments();
                                },
                                error(err) {
                                    dispatch(setGlobalError(err));
                                },
                            }
                        )
                    }
                    }></CommentInputElement>
                {
                    comments?.length === 0 ? <></> :
                        <>
                            {

                                comments?.map((comment, index) =>
                                    <CommentElement comment={comment} key={index}></CommentElement>
                                )
                            }
                        </>
                }
            </Paper>
        </Grid>
    );
}