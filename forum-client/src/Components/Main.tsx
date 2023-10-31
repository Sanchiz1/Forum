import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Box, Button, Container, CssBaseline, Paper } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { getUserAccount } from '../Redux/Epics/AccountEpics';
import { RootState } from '../Redux/store';
import ButtonWithCheck from './ButtonWithCheck/ButtonWithCheck';
import { isSigned } from '../API/loginRequests';
import { useNavigate } from 'react-router-dom';
import { setLogInError } from '../Redux/Reducers/AccountReducer';
import { useEffect, useState } from 'react';
import { requestPosts } from '../API/postRequests';
import { Post } from '../Types/Post';
import PostElement from './Posts/PostElement';

export default function Main() {
  const [posts, setPosts] = useState<Post[]>()
  const dispatch = useDispatch();
  const navigator = useNavigate()

  useEffect(() => {
    requestPosts().subscribe({
      next(value) {
        setPosts(value)
      },
    })
  }, [])

  const Account = useSelector((state: RootState) => state.account.Account);

  return (
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
          mt: 4, mb: 4
        }}>
          <Grid container spacing={1}>
            <Grid item xs={12} md={12} lg={12}>
              <Paper sx={{
                p: 1,
                width: 1,
              }}>
                <ButtonWithCheck variant='outlined' ActionWithCheck={() => {
                  if (isSigned()) {
                    navigator('/CreatePost');
                  }
                  else {
                    dispatch(setLogInError('Not signed in'));
                  }
                }}></ButtonWithCheck>
              </Paper>
            </Grid>
            {
              posts?.map((post) =>
                <PostElement post={post} key={post.id.toString()} customClickEvent={(event: React.MouseEvent<HTMLDivElement>) => navigator('/post/' + post.id)}></PostElement>
              )
            }
          </Grid>
        </Container>
      </Box>
    </Box>
  )


}