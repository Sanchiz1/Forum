import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
import Grid from '@mui/material/Grid';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import Button from '@mui/material/Button';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { getUserAccount } from '../../Redux/Epics/AccountEpics';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { RootState } from '../../Redux/store';
import { Backdrop, Badge, CircularProgress, Container, CssBaseline, IconButton, LinearProgress, Toolbar, Collapse, TextField, Alert, Select, MenuItem, Skeleton, SelectChangeEvent } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import { GetDateString } from '../../Helpers/DateFormatHelper';
import { User, UserInput } from '../../Types/User';
import { requestUserByUsername, updateUserRequest } from '../../API/userRequests';
import { SnackbarProvider, VariantType, enqueueSnackbar, useSnackbar } from 'notistack';
import { BootstrapInput } from '../UtilComponents/BootstrapInput';
import { Post } from '../../Types/Post';
import { requestUserPosts } from '../../API/postRequests';
import PostElement from '../Posts/PostElement';

const validUsernamePattern = /^[a-zA-Z0-9_.]+$/;
const validEmailPattern = /^(?=.{0,64}$)[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
const validPasswordPattern = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[a-zA-Z]).{8,21}$/;

const roles = {
    0: "User",
    1: "Admin",
    2: "Moderator",
};

export default function AdminPage() {
    const [user, setUser] = useState<User>();
    const [role, setRole] = useState(0);
    const [userExists, setUserExists] = useState(true);
    const [openEdit, setOpenEdit] = useState(false);
    let { Username } = useParams();
    const dispatch = useDispatch();
    const navigator = useNavigate();
    const { state } = useLocation()

    const Account = useSelector((state: RootState) => state.account.Account);

    return (
        <Box
            component="main"
            sx={{
                backgroundColor: (theme) =>
                    theme.palette.mode === 'light'
                        ? theme.palette.grey[100]
                        : theme.palette.grey[900],
                flexGrow: 1,
                minHeight: '100vh',
                overflow: 'auto',
                display: 'flex'
            }}
        >
            <Container maxWidth='lg' sx={{
                mt: 4, mb: 4, width: 1
            }}>
                <Grid container spacing={2}>
                    <Button onClick={() => navigator("/Categories")}>Categories</Button>
                </Grid>
            </Container>
        </Box>
    );
}