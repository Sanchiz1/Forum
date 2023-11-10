import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { CssBaseline, createTheme } from '@mui/material';
import { ThemeProvider } from '@emotion/react';
import { Provider } from 'react-redux';
import store from './Redux/store';
import { SnackbarProvider } from 'notistack';
import { orange } from '@mui/material/colors';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
const defaultTheme = createTheme({
  palette: {
    mode: 'dark',
    secondary: {
      main: '#9e9e9e',
    }
  }
});
root.render(
  <Provider store={store}>
    <ThemeProvider theme={defaultTheme}>
      <CssBaseline enableColorScheme />
      <SnackbarProvider>
        <App/>
      </SnackbarProvider>
    </ThemeProvider>
  </Provider>
);
