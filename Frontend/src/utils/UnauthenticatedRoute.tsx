import {useContext} from "react";
import {AuthContext} from "./AuthContext.tsx";
import {Navigate} from "react-router-dom";

export const UnauthenticatedRoute = ({ children } : any) => {
    const { isAuthenticated, isLoading } = useContext(AuthContext);
    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (isAuthenticated) {
        return <Navigate to="/home" />;
    }

    return children;
};