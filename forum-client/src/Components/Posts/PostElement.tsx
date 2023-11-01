import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Box, Button, Container, CssBaseline, Paper, Link } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { Post } from '../../Types/Post';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';

interface Props {
  post: Post;
  customClickEvent: React.MouseEventHandler<HTMLDivElement>
}

export default function PostElement(props: Props) {
  const navigate = useNavigate();

  return (
    <Grid item xs={12} md={12} lg={12} onClick={props.customClickEvent}>
      <Paper sx={{
        p: 1,
        width: 1,
        ":hover":{
          boxShadow: 5
        }
      }}>
        <Grid sx={{
          display: 'flex',
          flexDirection: 'row',
          alignItems: 'center'
        }}>
          <Link variant="subtitle1" onClick={(e) => e.stopPropagation()} component={RouterLink} to={'/user/' + props.post.user_Username} color="primary" sx={{mr: 1, textDecoration: 'none', cursor: 'pointer', color: 'white', 
          ":hover": {
            textDecoration: 'underline'
          }
          }
        } >
            {props.post.user_Username}
          </Link>
          <Typography variant="subtitle2" color="text.disabled" component="p" sx={{fontSize: 5, mr: 0.2}}>
            {'\u2B24'}
          </Typography>
          <Typography variant="subtitle2" color="text.disabled" component="p">
          {timeSince(GetLocalDate(new Date(props.post.date)))}
          </Typography>
        </Grid>
        <Typography variant="subtitle1" color="text.disabled" component="p">
          {props.post.title}
        </Typography>
        <Divider />
        <br />
        <Typography variant="subtitle1" color="text.disabled" component="p" sx={{
          maxHeight: '150px', overflow: 'hidden', 
          whiteSpace: 'pre-line',
          textOverflow: 'ellipsis',
          content: 'none',
          position: 'relative',
          "&::before": {
            content: 'no-close-quote',
            width: '100%',
            height: '100%',
            position: 'absolute',
            left: 0,
            top: 0,
            background: 'linear-gradient(transparent 70px, #1E1E1E)'
          }
        }}>
          {props.post.text}
        </Typography>
      </Paper>
    </Grid>
  )
}