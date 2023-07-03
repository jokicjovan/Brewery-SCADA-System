import {createContext, useState, useEffect} from 'react';
import axios from "axios";
import {environment} from "./Environment.tsx";

export const AuthContext = createContext({
    isAuthenticated: false,
    role: null,
    isLoading: true
});

export const AuthProvider = ({ children } : any) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [role, setRole] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        setIsLoading(true);
        axios.get(environment + `/api/User/whoAmI`)
            .then(res => {
                if (res.status === 200){
                    setIsAuthenticated(true);
                    setRole(res.data.role);
                }
                setIsLoading(false);
            })
            .catch(() => {
                setIsAuthenticated(false);
                setIsLoading(false);
            });
    }, []);


    return (
        <AuthContext.Provider value={{ isAuthenticated, isLoading, role }}>
            {children}
        </AuthContext.Provider>
    );
};
