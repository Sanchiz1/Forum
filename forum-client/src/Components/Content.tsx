import { createBrowserRouter, RouterProvider, Outlet, redirect, useLocation, useNavigate } from 'react-router-dom';
import SignIn from './Sign/Sign-in';
import Main from './Main';
import Header from './Navbar';
import UserPage from './User/UserPage';
import SignUp from './Sign/Sign-up';

const router = () => createBrowserRouter([
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


function AppContent() {
    
    return (
        <div className='Content container-fluid p-0 h-100'>
            <RouterProvider router={router()} />
        </div>
    );
}

export default AppContent;
