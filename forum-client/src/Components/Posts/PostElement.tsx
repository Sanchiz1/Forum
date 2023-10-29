import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Box, Button, Container, CssBaseline, Paper } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { Post } from '../../Types/Post';
import { timeSince } from '../../Helpers/TimeHelper';

interface Props {
  post: Post;
}

export default function PostElement(props: Props) {
  return (
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
          <Typography variant="subtitle1" component="p" sx={{mr: 1}}>
            Sanchiz
          </Typography>
          <Typography variant="subtitle2" color="text.disabled" component="p" sx={{fontSize: 5, mr: 0.2}}>
            {'\u2B24'}
          </Typography>
          <Typography variant="subtitle2" color="text.disabled" component="p">
          {timeSince(new Date(props.post.date))}
          </Typography>
        </Grid>
        <Typography variant="subtitle1" color="text.disabled" component="p">
          {props.post.title}
        </Typography>
        <Divider />
        <br />
        <Typography variant="subtitle1" color="text.disabled" component="p" sx={{
          maxHeight: '75px', overflow: 'hidden',
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
            background: 'linear-gradient(transparent 45px, #1E1E1E)'
          }
        }}>
          {props.post.text}
        </Typography>
      </Paper>
    </Grid>
  )
}