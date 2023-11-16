import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Box, Button, Container, CssBaseline, Paper, Link, IconButton } from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import { Reply, ReplyInput } from '../../Types/Reply';
import { Comment } from '../../Types/Comment';
import { useState } from 'react';
import ButtonWithCheck from '../UtilComponents/ButtonWithCheck';
import ReplyInputElement from '../UtilComponents/ReplyInputElement';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../Redux/store';
import { createReplyRequest } from '../../API/replyRequests';
import { enqueueSnackbar } from 'notistack';
import { setGlobalError } from '../../Redux/Reducers/AccountReducer';

interface Props {
  reply: Reply;
  refreshComment: () => void
}

export default function ReplyElement(props: Props) {
  const [liked, setLiked] = useState(props.reply.liked);
  const [likes, setLikes] = useState(props.reply.likes.valueOf());
  const [openReplyInput, setOpenReplyInput] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch()
  const Account = useSelector((state: RootState) => state.account.Account);

  return (
    <Grid item xs={12} md={12} lg={12} sx={{ mb: 2 }}>
      <Grid sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        pl: 0.5
      }}>
        <Link variant="caption" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + props.reply.user_Username} color="primary" sx={{
          mr: 0.5, textDecoration: 'none', cursor: 'pointer', color: 'white',
          ":hover": {
            textDecoration: 'underline'
          }
        }
        } >
          {props.reply.user_Username}
        </Link>
        {
          props.reply.reply_Id && props.reply.reply_Username ?
            <>
              <Typography variant="caption" component="p" sx={{ mr: 0.5 }}>to</Typography>
              <Link variant="caption" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + props.reply.user_Username} color="primary" sx={{
                mr: 0.5, textDecoration: 'none', cursor: 'pointer', color: 'white',
                ":hover": {
                  textDecoration: 'underline'
                }
              }
              } >
                {props.reply.user_Username}
              </Link>
            </>
            : <></>
        }
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: 3, mr: 0.5 }}>
          {'\u2B24'}
        </Typography>
        <Typography variant="caption" color="text.disabled" component="p">
          {timeSince(GetLocalDate(new Date(props.reply.date)))}
        </Typography>
      </Grid>
      <Typography variant="subtitle1" component="p" sx={{ pl: 0.5, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
        {props.reply.text}
      </Typography>
      <Grid lg={1} md={2} xs={3} item sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center'
      }}>
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: '14px', display: 'flex', alignItems: 'center', mr: 3 }}>
          <IconButton sx={{ p: 0.5, color: 'inherit' }} onClick={(e) => { e.stopPropagation(); setLikes(liked ? likes - 1 : likes + 1); setLiked(!liked); }}>
            {liked ? <FavoriteIcon sx={{ fontSize: '18px' }}></FavoriteIcon> : <FavoriteBorderIcon sx={{ fontSize: '18px' }}></FavoriteBorderIcon>}
          </IconButton>
          {likes.toString()}
        </Typography>
        <ButtonWithCheck variant='text' sx={{ color: "text.secondary" }} ActionWithCheck={() => {
          setOpenReplyInput(!openReplyInput);
        }
        }>Reply</ButtonWithCheck>
      </Grid>
      {openReplyInput ?
        <Box>
          <ReplyInputElement
            setState={setOpenReplyInput}
            Action={(e: string) => {
              if (e.trim() === '') return;
              const replyInput: ReplyInput = {
                comment_Id: props.reply.comment_Id,
                reply_Id: props.reply.id,
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
                    props.refreshComment()
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
    </Grid>
  )
}