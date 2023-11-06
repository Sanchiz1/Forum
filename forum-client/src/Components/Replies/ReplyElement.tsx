import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Link as RouterLink, useNavigate } from 'react-router-dom';
import { Box, Button, Container, CssBaseline, Paper, Link } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { Post } from '../../Types/Post';
import { GetLocalDate, timeSince } from '../../Helpers/TimeHelper';
import { Reply } from '../../Types/Reply';

interface Props {
  reply: Reply;
}

export default function ReplyElement(props: Props) {
  const navigate = useNavigate();

  return (
    <Grid item xs={12} md={12} lg={12}>
      <Paper sx={{
        p: 1,
        width: 1,
        ":hover": {
          boxShadow: 5
        }
      }}>
        <Grid sx={{
          display: 'flex',
          flexDirection: 'row',
          alignItems: 'center'
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
          <Typography variant="caption" color="text.disabled" component="p" sx={{ fontSize: 3, mr: 0.5 }}>
            {'\u2B24'}
          </Typography>
          <Typography variant="caption" color="text.disabled" component="p">
            {timeSince(GetLocalDate(new Date(props.reply.date)))}
          </Typography>
        </Grid>
        <Divider sx={{ mb: 1 }} />
        <Typography variant="subtitle1" component="p" sx={{ mt: 2, whiteSpace: 'pre-line', overflowWrap: 'break-word' }}>
          {props.reply.text}
        </Typography>
      </Paper>
    </Grid>
  )
}