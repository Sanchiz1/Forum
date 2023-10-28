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

interface MainProps {
  posts: ReadonlyArray<string>;
  title: string;
}

export default function Main() {
  const dispatch = useDispatch();
  const navigator = useNavigate()

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
          <Grid container spacing={3}>
            <Grid item xs={12} md={12} lg={12}>
              <Paper sx={{
                p: 1,
                width: 1,
              }}>
                <ButtonWithCheck variant='outlined' ActionWithCheck={() => {
                  if(isSigned()){
                    navigator('/CreatePost');
                  }
                  else{
                    dispatch(setLogInError('Not signed in'));
                  }
                }}></ButtonWithCheck>
              </Paper>
            </Grid>
          </Grid>
        </Container>
      </Box>
    </Box>
  )


}