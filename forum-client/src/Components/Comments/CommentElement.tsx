import { useState, useEffect } from 'react';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Box, Button, Container, CssBaseline, Paper, Link, IconButton, TextField, Collapse } from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import { Reply, ReplyInput } from '../../Types/Reply';
import { Comment } from '../../Types/Comment';
import ReplyElement from './ReplyElement';
import { createReplyRequest, requestReplies } from '../../API/replyRequests';
import ButtonWithCheck from '../UtilComponents/ButtonWithCheck';
import { isSigned } from '../../API/loginRequests';
import CommentInputElement from '../UtilComponents/CommentInputElement';
import { enqueueSnackbar } from 'notistack';
import ReplyInputElement from '../UtilComponents/ReplyInputElement';
import { useDispatch, useSelector } from 'react-redux';
import { setGlobalError } from '../../Redux/Reducers/AccountReducer';
import { RootState } from '../../Redux/store';
import { requestCommentById } from '../../API/commentRequests';

interface Props {
  comment: Comment;
}

export default function CommentElement(props: Props) {
  const [comment, setComment] = useState(props.comment);
  const [liked, setLiked] = useState(props.comment.liked);
  const [likes, setLikes] = useState(props.comment.likes.valueOf());
  const [replies, SetReplies] = useState<Reply[]>([])
  const [openReplyInput, setOpenReplyInput] = useState(false);
  const [openReplies, setOpenReplies] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const Account = useSelector((state: RootState) => state.account.Account);


  const refetchComment = () => {
    setUserTimestamp(new Date())
    requestCommentById(comment.id.valueOf()).subscribe({
      next(result) {
        setComment(result);
        refetchReplies();
      },
      error(err) {
        dispatch(setGlobalError(err));
      }
    })
  }

  //replies
  const next = 3;
  const order = "Date"
  const [userTimestamp, setUserTimestamp] = useState(new Date());
  const [offset, setOffset] = useState(0);

  const refetchReplies = () => {
    SetReplies([]);
    setUserTimestamp(new Date())
    setOffset(0);
  }
  const fetchReplies = () => {
    requestReplies(comment.id, offset, next, order, userTimestamp).subscribe({
      next(result) {
        SetReplies([...replies, ...result]);
      },
      error(err) {
        dispatch(setGlobalError(err));
      }
    })
  };

  useEffect(() => {
    if (!openReplies) return;
    fetchReplies()
  }, [offset, userTimestamp])

  useEffect(() => {
    refetchReplies()
  }, [openReplies])

  return (
    <Grid item xs={12} md={12} lg={12} sx={{ mb: 2 }}>
      <Grid sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        pl: 0.5
      }}>
        <Link variant="caption" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + comment.user_Username} color="primary" sx={{
          mr: 0.5, textDecoration: 'none', cursor: 'pointer', color: 'white',
          ":hover": {
            textDecoration: 'underline'
          }
        }
        } >
          {comment.user_Username}
        </Link>
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: 3, mr: 0.5 }}>
          {'\u2B24'}
        </Typography>
        <Typography variant="caption" color="text.disabled" component="p">
          {timeSince(GetLocalDate(new Date(comment.date)))}
        </Typography>
      </Grid>
      <Typography variant="subtitle1" component="p" sx={{ pl: 0.5, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
        {comment.text}
      </Typography>
      <Grid lg={12} md={12} xs={12} item sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center'
      }}>
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: '14px', display: 'flex', alignItems: 'center', mr: 3 }}>
          <IconButton sx={{ p: 0.5, color: 'inherit' }} onClick={(e) => { e.stopPropagation(); setLikes(liked ? likes - 1 : likes + 1); setLiked(!liked); }}>
            {liked ? <FavoriteIcon sx={{ fontSize: '18px' }}></FavoriteIcon> :
              <FavoriteBorderIcon sx={{ fontSize: '18px' }}></FavoriteBorderIcon>}
          </IconButton>
          {likes.toString()}
        </Typography>
        <ButtonWithCheck variant='text' sx={{ color: "text.secondary", fontSize: "14px important!" }} ActionWithCheck={() => {
          setOpenReplyInput(!openReplyInput);
        }}>Reply</ButtonWithCheck>
      </Grid>
      {openReplyInput ?

        <Box sx={{ pl: 3 }}>
          <ReplyInputElement
            setState={setOpenReplyInput}
            Action={(e: string) => {
              if (e.trim() === '') return;
              const replyInput: ReplyInput = {
                comment_Id: props.comment.id,
                text: e,
                user_Id: Account.id
              }
              createReplyRequest(replyInput).subscribe(
                {
                  next(value) {
                    enqueueSnackbar(value, {
                      variant: 'success', anchorOrigin: {
                        vertical: 'top',
                        horizontal: 'center'
                      },
                      autoHideDuration: 1500
                    });
                    refetchComment()
                  },
                  error(err) {
                    dispatch(setGlobalError(err));
                  },
                }
              )
            }
            }></ReplyInputElement>
        </Box>
        : <></>}
      {
        comment.replies.valueOf() > 0 ?
          <Button onClick={() => setOpenReplies(!openReplies)}>{comment.replies.valueOf()} Replies</Button>
          :
          <></>
      }
      {
        openReplies ?
          <Box sx={{ pl: 3 }}>
            {
              replies.map((reply, index) =>
                <ReplyElement reply={reply} key={index} refreshComment={refetchComment}></ReplyElement>
              )
            }
            {
              comment.replies.valueOf() > replies.length ?
                <Button onClick={() => setOffset(offset + next)}>Load More</Button>
                : <></>
            }
          </Box>
          :
          <></>
      }
    </Grid >
  )
}