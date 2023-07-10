import ReactDOM from 'react-dom/client'
import {createBrowserRouter, Navigate, RouterProvider,} from "react-router-dom"
import './index.css'
import {UnauthenticatedRoute} from "./utils/UnauthenticatedRoute.tsx";
import Login from "./pages/Login.tsx";
import {AuthenticatedRoute} from "./utils/AuthenticatedRoute.tsx";
import Home from "./pages/Home.tsx";
import {AuthProvider} from "./utils/AuthContext.tsx";
import {createTheme, ThemeProvider} from "@mui/material";
import axios from "axios";
import Register from "./pages/Register.tsx";
import {Toaster} from "react-hot-toast";
import Reports from "./pages/Reports";
import {useLayoutEffect} from "react";
import Trending from "./pages/Trending";
import Navbar from "./components/Navbar";

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
    {path:"/register", element: <AuthenticatedRoute><Navbar/><Register/></AuthenticatedRoute>},
    {path:"/home", element: <AuthenticatedRoute><Navbar/><Home/></AuthenticatedRoute>},
    {path:"/reports", element: <AuthenticatedRoute><Navbar/><Reports/></AuthenticatedRoute>},
    {path:"/trending", element: <AuthenticatedRoute><Navbar/><Trending/></AuthenticatedRoute>},
    {path:"*", element: <Navigate to="/home" replace />},
])


ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  //<React.StrictMode>
      <ThemeProvider theme={theme} >
          <AuthProvider>
            <RouterProvider router={router}/>
            <Toaster position="bottom-right"/>
          </AuthProvider>
      </ThemeProvider>
  //</React.StrictMode>,
)
