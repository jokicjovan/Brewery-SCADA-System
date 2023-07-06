import { Navigate } from "react-router-dom";
import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import NotificationPopup from "../components/NotificationPopup.tsx";
export const AuthenticatedRoute = ({ children } : any) => {
    const { isAuthenticated , isLoading } = useContext(AuthContext);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (!isAuthenticated){
        return <Navigate to="/login" />
    }

    return <>
        <NotificationPopup/>
        {children}
    </>;
};