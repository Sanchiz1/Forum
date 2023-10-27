import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import { Button } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { getUserAccount } from '../Redux/Epics/AccountEpics';
import { RootState } from '../Redux/store';

interface MainProps {
  posts: ReadonlyArray<string>;
  title: string;
}

export default function Main() {
  const dispatch = useDispatch();

  const User = useSelector((state: RootState) => state.account.Account);

  return (
    <Grid
      item
      xs={12}
      md={8}
      sx={{
        '& .markdown': {
          py: 3,
        },
      }}
    >
      <Typography variant="h6" gutterBottom>
        {User.username}
      </Typography>
      <Divider />
      <Button onClick={() => dispatch(getUserAccount())}>Get</Button>
    </Grid>
  );
}