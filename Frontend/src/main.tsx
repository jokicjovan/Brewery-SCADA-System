import React from 'react'
import ReactDOM from 'react-dom/client'
import {createBrowserRouter, Navigate, RouterProvider,} from "react-router-dom"
import './index.css'
import {UnauthenticatedRoute} from "./components/UnauthenticatedRoute.tsx";
import Login from "./pages/Login.tsx";
import {AuthenticatedRoute} from "./components/AuthenticatedRoute.tsx";
import Home from "./pages/Home.tsx";
import {AuthProvider} from "./components/AuthContext.tsx";
import {createTheme, ThemeProvider} from "@mui/material";
import axios from "axios";

axios.defaults.withCredentials = true

const theme = createTheme({
    palette: {
        primary: {
            main: "#394867",
        },
        secondary: {
            main: "#9BA4B5",
            contrastText: 'white'
        },
    },
});

const router = createBrowserRouter([
    {path:"/login", element: <UnauthenticatedRoute><Login/></UnauthenticatedRoute>},
    {path:"/home", element: <AuthenticatedRoute><Home/></AuthenticatedRoute>},
    {path:"*", element: <Navigate to="/home" replace />},
])

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <React.StrictMode>
      <ThemeProvider theme={theme}>
          <AuthProvider>
            <RouterProvider router={router}/>
          </AuthProvider>
      </ThemeProvider>
  </React.StrictMode>,
)
