import ReactDOM from 'react-dom/client'
import {createBrowserRouter, Navigate, RouterProvider,} from "react-router-dom"
import './index.css'
import {UnauthenticatedRoute} from "./utils/UnauthenticatedRoute.tsx";
import Login from "./pages/Login.tsx";
import {AuthenticatedRoute} from "./utils/AuthenticatedRoute.tsx";
import {AuthProvider} from "./utils/AuthContext.tsx";
import {createTheme, ThemeProvider} from "@mui/material";
import axios from "axios";
import Register from "./pages/Register.tsx";
import {Toaster} from "react-hot-toast";
import Reports from "./pages/Reports";
import Navbar from "./components/Navbar";
import TagsCRUD from "./pages/TagsCRUD.tsx";
import Trending from "./pages/Trending.tsx";
import {AdminRoute} from "./utils/AdminRoute.tsx";

axios.defaults.withCredentials = true

const theme = createTheme({
    palette: {
        primary: {
            main: "#0f0b0a",
        },
        secondary: {
            main: "#fdefc7",
            contrastText: 'white'
        },
    },
});

const router = createBrowserRouter([
    {path:"/login", element: <UnauthenticatedRoute><Login/></UnauthenticatedRoute>},
    {path:"/register", element: <AuthenticatedRoute><AdminRoute><Navbar/><Register/></AdminRoute></AuthenticatedRoute>},
    {path:"/manager", element: <AuthenticatedRoute><AdminRoute><Navbar/><TagsCRUD/></AdminRoute></AuthenticatedRoute>},
    {path:"/trending", element: <AuthenticatedRoute><Navbar/><Trending/></AuthenticatedRoute>},
    {path:"/reports", element: <AuthenticatedRoute><AdminRoute><Navbar/><Reports/></AdminRoute></AuthenticatedRoute>},
    {path:"*", element: <Navigate to="/trending" replace />},
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
