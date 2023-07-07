import {Box, Button, Card, CardActions, CardContent, Container, Grid, Modal, Typography} from "@mui/material";
import React from "react";
import {CreateTagPopup} from "../components/CreateTagPopup.tsx";

export default function TagsCRUD() {
    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 800,
        height: 500,
        bgcolor: 'white',
        boxShadow: 24,
        p: 4,
    };

    const [open, setOpen] = React.useState(false);
    const handleOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };
    return (
        <div>
            <Button variant="contained" color="secondary" onClick={handleOpen}>
                Open Animated Modal
            </Button>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={open}
                onClose={handleClose}
                closeAfterTransition
            >
                <Box sx={style}>
                    <CreateTagPopup></CreateTagPopup>
                </Box>

            </Modal>
            <Container sx={{width:"70%"}}>
                <Card sx={{height:"200px"}}>
                    <CardContent>
                        <Grid container spacing={2}>
                            <Grid item  sm={5}>
                                <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                    Driver
                                </Typography>
                                <Typography variant="h6" component="div">
                                    DriverName
                                </Typography>
                                <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                    Description
                                </Typography>
                                <Typography sx={{fontSize: 15}} gutterBottom>
                                    This is some description about some tag idk why i write this
                                    This is some description about some tag idk why i write this
                                    This is some description about some tag idk why i write this
                                </Typography>

                            </Grid>
                            <Grid item sm={3}>
                                <Typography sx={{mb: 1.5}} color="text.secondary">
                                    adjective
                                </Typography>
                                <Typography variant="body2">
                                    well meaning and kindly.
                                    <br/>
                                    {'"a benevolent smile"'}
                                </Typography></Grid>
                            <Grid item sm={1}>Column 3</Grid>
                        </Grid>


                    </CardContent>
                </Card>
            </Container>

        </div>
    );
}