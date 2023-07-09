import {useNavigate} from "react-router-dom";
import {useState} from "react";
import {
    Avatar,
    Box, Button,
    Container,
    CssBaseline, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle,
    Grid,
    InputLabel, TextField,
    Typography
} from "@mui/material";
import axios from "axios";
import {environment} from "../utils/Environment.tsx";

export default function RegisterForm() {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [name, setName] = useState("")
    const [surname, setSurname] = useState("")
    const [error, setError] = useState("")
    const [dialogOpen, setDialogOpen] = useState(false);
    const navigate = useNavigate()
    const redirectToHome = () => {
        navigate("/home");
    }

    function handleSubmit(event : any) {
        event.preventDefault()

        axios.post(environment + `/api/User/register`, {
            name: name,
            surname: surname,
            email: email,
            password: password
        }).then(res => {
            if (res.status === 200){
                setDialogOpen(true);
            }
        }).catch((error) => {
            console.log(error);
            if (error.response?.status !== undefined && error.response.status === 404){
                setError("Resource not found!");
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
                <Avatar sx={{ m: 1, bgcolor: 'primary.main' }}>
                </Avatar>
                <Typography component="h1" variant="h3">
                    Sign Up
                </Typography>
                <Box component="form" onSubmit={handleSubmit} sx={{ mt: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                required
                                fullWidth
                                autoFocus
                                id="name"
                                label="First Name"
                                name="name"
                                placeholder={"(e.g. Pera)"}
                                autoComplete="given-name"
                                onChange={(e) => {setName(e.target.value)}}
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                required
                                fullWidth
                                id="surname"
                                label="Last Name"
                                name="surname"
                                placeholder={"(e.g. Peric)"}
                                autoComplete="family-name"
                                onChange={(e) => {setSurname(e.target.value)}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                placeholder={"(e.g. user@example.com)"}
                                autoComplete="email"
                                onChange={(e) => {setEmail(e.target.value)}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                                onChange={(e) => {setPassword(e.target.value)}}
                            />
                        </Grid>
                    </Grid>
                    <div>
                        <InputLabel style={{color:"red"}} sx={{mt: 2}}>{error}</InputLabel>
                    </div>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 1}}
                    >
                        Sign Up
                    </Button>
                </Box>
            </Box>
        </Container>
        <Dialog
            open={dialogOpen}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            <DialogTitle id="alert-dialog-title">
                {"Congratulations, you signed up successfully!"}
            </DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    Next step is to go to login page and sign in to created account.
                </DialogContentText>
            </DialogContent>
            <DialogActions style={{display:"flex", justifyContent:"center"}}>
                <Button onClick={redirectToHome} variant="contained">OK</Button>
            </DialogActions>
        </Dialog>
    </>
}