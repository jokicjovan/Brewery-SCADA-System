import {useState} from "react";
import {Avatar, Box, Button, Container, CssBaseline, InputLabel, Stack, TextField, Typography} from "@mui/material";
import {Link, useNavigate} from "react-router-dom";
import axios from "axios";
import {environment} from "../utils/Environment.tsx";

export default function LoginForm(){
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [error, setError] = useState("")
    const navigate = useNavigate()


    function submitHandler(event : any) {
        event.preventDefault()
        axios.post(environment + `/api/User/login`, {
            email: email,
            password: password
        }).then(res => {
            if (res.status === 200){
                navigate(0);
            }
        }).catch((error) => {
            console.log(error)
            if (error.response?.status !== undefined && error.response.status === 404){
                setError("Invalid username or password!");
            }
            else if (error.response?.status !== undefined && error.response.status === 400){
                setError("Invalid input!");
            }
            else{
                setError("An error occurred!");
            }
        });
    }


    return <>
        <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
            sx={{
                marginTop: 8,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}
        >
            <Avatar sx={{ m: 1, bgcolor: 'primary.main' }}/>

            <Typography component="h1" variant="h3">
                Sign in
            </Typography>
            <Box component="form" onSubmit={submitHandler} sx={{ mt: 1 }}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    autoFocus
                    type="text"
                    id="email"
                    label="Email Address"
                    name="email"
                    autoComplete="email"
                    variant="outlined"
                    onChange={(e) => {setEmail(e.target.value)}}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="current-password"
                    variant="outlined"
                    onChange={(e) => {setPassword(e.target.value)}}
                />
                <div>
                    <InputLabel style={{color:"red"}}>{error}</InputLabel>
                </div>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{mt:3, mb: 3 }}
                >
                    Sign In
                </Button>
                <Stack style={{textAlign:"center"}}>
                    <Stack>
                        {"Don't have an account? "}
                    </Stack>
                    <Stack>
                        <Link to="/register" style={{color:"dodgerblue", textDecoration:"None"}}>
                            {"Sign up"}
                        </Link>
                    </Stack>
                </Stack>
            </Box>
        </Box>
        </Container>
    </>
}