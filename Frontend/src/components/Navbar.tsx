import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import axios from "axios";
import {useNavigate} from "react-router-dom";
import {environment} from "../utils/Environment.js";
export default function Navbar() {

    const [anchorElNav, setAnchorElNav] = React.useState(null);
    const navigate = useNavigate()
    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleCloseNavMenu = event => {
        setAnchorElNav(null);
    };
    const handleDBManagerClick = (event) => {
        handleCloseNavMenu(event)
        navigate('/home')
    };

    const TrendingClick = (event) => {
        handleCloseNavMenu(event)
        navigate('/trending')
    };

    const handleReportsClick = (event) => {
        handleCloseNavMenu(event)
        navigate('/reports')
    };
    const handleRegisterClick = (event) => {
        handleCloseNavMenu(event)
        navigate('/register')
    };


    const handleLogoutClick = (event) => {
        handleCloseNavMenu(event)
        event.preventDefault()

        axios.post(environment + `/api/User/logout`)
            .then(res => {
                if (res.status === 200){
                    navigate(0);
                }
            }).catch((error) => {
            console.log(error);
        });
    };




    return (
        <AppBar style={{position:"sticky",margin:0,backgroundColor: "#0f0b0a"}}>
            <Container maxWidth="xl">
                <Toolbar disableGutters>

                    <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
                        <IconButton
                            size="large"
                            aria-label="account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleOpenNavMenu}
                            color="inherit"
                        >
                            <MenuIcon />
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorElNav}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left',
                            }}
                            open={Boolean(anchorElNav)}
                            onClose={handleCloseNavMenu}
                            sx={{
                                display: { xs: 'block', md: 'none' },
                            }}
                        >
                            <MenuItem key="certificates" onClick={handleDBManagerClick}>
                                <Typography textAlign="center">DB Manager</Typography>
                            </MenuItem>
                            <MenuItem key="requests" onClick={TrendingClick}>
                                <Typography textAlign="center">Trending</Typography>
                            </MenuItem>
                            <MenuItem key="generate" onClick={handleReportsClick}>
                                <Typography textAlign="center">Reports</Typography>
                            </MenuItem>
                            <MenuItem key="register" onClick={handleRegisterClick}>
                                <Typography textAlign="center">Register</Typography>
                            </MenuItem>
                            <MenuItem key="logout" onClick={handleLogoutClick}>
                                <Typography textAlign="center">Logout</Typography>
                            </MenuItem>
                        </Menu>
                    </Box>
                        <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                            <Button
                                key="certificates"
                                onClick={handleDBManagerClick}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                            >
                                DB Manager
                            </Button>
                            <Button
                                key="requests"
                                onClick={TrendingClick}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                            >
                                Trending
                            </Button>
                            <Button
                                key="generate"
                                onClick={handleReportsClick}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                            >
                                Reports
                            </Button>
                            <Button
                                key="register"
                                onClick={handleRegisterClick}
                                sx={{ my: 2, color: 'white', display: 'block' }}
                            >
                                Register
                            </Button>
                        </Box>

                    <Box sx={{ flexGrow: 0,display: { xs: 'none', md: 'flex' }  }}>
                        <Button
                            key="logout"
                            onClick={handleLogoutClick}
                            sx={{ my: 2, color: 'white', display: 'block' }}
                        >
                            Logout
                        </Button>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
}