import { createBrowserRouter, RouterProvider, Outlet, redirect, useLocation, useNavigate } from 'react-router-dom';
import SignIn from './Sign/Sign-in';
import Main from './Main';
import Header from './Navbar';
import UserPage from './User/UserPage';
import SignUp from './Sign/Sign-up';
import { isSigned } from '../API/loginRequests';
import CreatePost from './Posts/CreatePost';
import { useDispatch } from 'react-redux';
import { setLogInError } from '../Redux/Reducers/AccountReducer';
import PostPage from './Posts/PostPage';

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

    const setSignError = () => {
        dispatch(setLogInError('Not signed in'));
    }

    return (
        <div className='Content container-fluid p-0 h-100'>
            <RouterProvider router={router(setSignError)} />
        </div>
    );
}



function CheckAndNavigate(Action: () => void) {

    if (!isSigned()) { 
        Action();
        return redirect("/") };
    return null;
}

