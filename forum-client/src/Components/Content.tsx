import React, { useEffect } from 'react';
import { createBrowserRouter, RouterProvider, Outlet, redirect } from 'react-router-dom';
import { getTokenOrNavigate, setCookie } from '../API/login';
import SignIn from './Sign-in';
import Main from './Main';
import Header from './Navbar';

const sections = [
    { title: 'Technology', url: '#' },
    { title: 'Design', url: '#' },
    { title: 'Culture', url: '#' },
    { title: 'Business', url: '#' },
    { title: 'Politics', url: '#' },
    { title: 'Opinion', url: '#' },
    { title: 'Science', url: '#' },
    { title: 'Health', url: '#' },
    { title: 'Style', url: '#' },
    { title: 'Travel', url: '#' },
  ];

const router = () => createBrowserRouter([
    {
        element: <Header title="Blog" sections={sections} />,
        //loader: async () => getTokenOrNavigate(),
        children: [
            {
                path: "/",
                element: <Main></Main>
            },
        ]
    },
    {
        path: "/Login",
        element: <SignIn />,
        //loader: async () => getTokenOrNavigate(true),
    }
])


function AppContent() {
    useEffect(() => {
        setCookie({ name: "refresh_sent", value: "false" })
    }, [])

    return (
        <div className='Content container-fluid p-0 h-100'>
            <RouterProvider router={router()} />
        </div>
    );
}

export default AppContent;
