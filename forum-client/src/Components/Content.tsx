import { createBrowserRouter, RouterProvider, Outlet, redirect, useLocation, useNavigate } from 'react-router-dom';
import SignIn from './Sign/Sign-in';
import Main from './Main';
import Header from './Navbar';
import UserPage from './User/UserPage';
import SignUp from './Sign/Sign-up';
import { isSigned } from '../API/loginRequests';
import CreatePost from './Posts/CreatePost';
import { useDispatch, useSelector } from 'react-redux';
import { setGlobalError, setLogInError } from '../Redux/Reducers/AccountReducer';
import PostPage from './Posts/PostPage';
import { Alert, Collapse, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { RootState } from '../Redux/store';

const router = (Action: () => void) => createBrowserRouter([
    {
        element: <Header />,
        children: [
            {
                path: "/",
                element: <Main />
            },
            {
                path: "/user/:Username",
                element: <UserPage />
            },
            {
                path: "/post/:PostId",
                element: <PostPage />
            },
            {
                path: "/createPost",
                element: <CreatePost />,
                loader: async () => CheckAndNavigate(Action),
            }
        ]
    },
    {
        path: "/Sign-in",
        element: <SignIn />,
    },
    {
        path: "/Sign-up",
        element: <SignUp />,
    }
])


export default function AppContent() {
    const dispatch = useDispatch();
    const globalError = useSelector((state: RootState) => state.account.GlobalError);

    const setSignError = () => {
        dispatch(setLogInError('Not signed in'));
    }

    return (
        <>
            <Collapse in={globalError != ''}>
                <Alert
                    severity="error"
                    action={
                        <IconButton
                            aria-label="close"
                            color="inherit"
                            onClick={() => {
                                dispatch(setGlobalError(''));
                            }}
                        >
                            <CloseIcon />
                        </IconButton>
                    }
                    sx={{ fontSize: 15 }}
                >
                    {globalError}
                </Alert>
            </Collapse>
            <RouterProvider router={router(setSignError)} />
        </>
    );
}



function CheckAndNavigate(Action: () => void) {

    if (!isSigned()) {
        Action();
        return redirect("/")
    };
    return null;
}

