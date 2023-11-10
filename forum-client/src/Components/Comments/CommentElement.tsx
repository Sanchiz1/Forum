import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Box, Button, Container, CssBaseline, Paper, Link, IconButton } from '@mui/material';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import { Reply } from '../../Types/Reply';
import { Comment } from '../../Types/Comment';
import { useState } from 'react';
import ReplyElement from './ReplyElement';
import { requestReplies } from '../../API/replyRequests';
import ButtonWithCheck from '../ButtonWithCheck/ButtonWithCheck';
import { isSigned } from '../../API/loginRequests';

interface Props {
  comment: Comment;
}

export default function CommentElement(props: Props) {
  const [liked, setLiked] = useState(props.comment.liked);
  const [likes, setLikes] = useState(props.comment.likes.valueOf());
  const [replies, SetReplies] = useState<Reply[]>([])
  const navigate = useNavigate();


  //replies
  const next = 10;
  const order = "Date"
  const [userTimestamp, setUserTimestamp] = useState(new Date());
  const [offset, setOffset] = useState(0);

  const fetchReplies = () => {
    if(replies.length > 0){
      SetReplies([]);
      setOffset(0);
      return;
    }
    requestReplies(props.comment.id, offset, next, order, userTimestamp).subscribe({
      next(result) {
        SetReplies([...replies, ...result]);
        setOffset(offset.valueOf() + next.valueOf());
      },
      error(err) {
      }
    })
  };


  return (
    <Grid item xs={12} md={12} lg={12}>
      <Grid sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        pl: 0.5
      }}>
        <Link variant="caption" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + props.comment.user_Username} color="primary" sx={{
          mr: 0.5, textDecoration: 'none', cursor: 'pointer', color: 'white',
          ":hover": {
            textDecoration: 'underline'
          }
        }
        } >
          {props.comment.user_Username}
        </Link>
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: 3, mr: 0.5 }}>
          {'\u2B24'}
        </Typography>
        <Typography variant="caption" color="text.disabled" component="p">
          {timeSince(GetLocalDate(new Date(props.comment.date)))}
        </Typography>
      </Grid>
      <Typography variant="subtitle1" component="p" sx={{ pl: 0.5, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
        {props.comment.text}
      </Typography>
      <Grid lg={12} md={12} xs={12} item sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center'
      }}>
        <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: '16px', display: 'flex', alignItems: 'center', mr: 3 }}>
          <IconButton sx={{ p: 0.5, color: 'inherit' }} onClick={(e) => { e.stopPropagation(); setLikes(liked ? likes - 1 : likes + 1); setLiked(!liked); }}>
            {liked ? <FavoriteIcon></FavoriteIcon> : <FavoriteBorderIcon></FavoriteBorderIcon>}
          </IconButton>
          {likes.toString()}
        </Typography>
        <ButtonWithCheck variant='text' sx={{color: "text.secondary"}} ActionWithCheck={() => {

        }}>Reply</ButtonWithCheck>
      </Grid>
      {
        props.comment.replies.valueOf() > 0 ?
          <Button onClick={fetchReplies}>{props.comment.replies.valueOf()} Replies</Button>
          :
          <></>
      }
      {
        replies ?
          <Box sx={{ pl: 3 }}>
            {
              replies.map((reply, index) =>
                <ReplyElement reply={reply} key={index}></ReplyElement>
              )
            }
          </Box>

          :
          <></>
      }
    </Grid>
  )
}