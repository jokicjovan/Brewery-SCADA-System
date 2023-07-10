import {
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    Container, Fab,
    Grid,
    IconButton, List,
    Modal, Switch,
    Typography
} from "@mui/material";
import React, {useEffect, useState} from "react";
import axios from "axios";
import {environment} from "../utils/Environment";
import {CreateAlarmPopup} from "../components/CreateAlarmPopup";
import {Add, Delete} from "@mui/icons-material";

export default function AlarmList({tag}) {
    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 500,
        height: 300,
        bgcolor: 'white',
        boxShadow: 24,
        p: 4,
    };

    const [open, setOpen] = React.useState(false);
    const [alarms, setAlarms] = useState([]);

    useEffect(() => {
        axios.get(environment + `/api/Alarm/getByTag?tagId=` + tag.id).then(response => {
            setAlarms(response.data);
        });
    }, [open])

    function deleteAlarm(id: string) {
        axios.delete(environment + `/api/Alarm/deleteAlarm?tagId=` + tag.id + "&alarmId=" + id).then(() => {
            axios.get(environment + `/api/Alarm/getByTag?tagId=` + tag.id).then(response => {
                setAlarms(response.data);
            });
        });
    }


    return (
        <div>
            <Typography sx={{textAlign: "center", fontSize: "30px", fontWeight: "bold", mb: 3}}>Alarms</Typography>

            <Fab variant="extended" sx={{position: "fixed", top: "30px", right: "30px", boxShadow: "none"}}
                 onClick={() => {
                     setOpen(true);
                 }}>
                <Add sx={{mr: 1}}/>
                Add Alarm
            </Fab>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={open}
                onClose={() => {
                    setOpen(false)
                }}
                closeAfterTransition
            >
                <Box sx={style}>
                    <CreateAlarmPopup tag={tag} closeModal={() => {
                        setOpen(false);
                    }}> </CreateAlarmPopup>
                </Box>
            </Modal>
            <Container sx={{width: "100%", maxHeight: "450px", overflow: "auto"}}>
                <Container sx={{width: "90%"}}>
                    {alarms.map((alarm) => (
                        <Card key={alarm.id} sx={{height: "100px", mb: 2}}>
                            <CardContent>
                                <Grid container spacing={3}>
                                    <Grid item sm={3}>
                                        <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                            Type
                                        </Typography>
                                        <Typography sx={{fontSize: 18}} gutterBottom>
                                            {alarm.type === 0 ? "Low" : "High"}
                                        </Typography>

                                    </Grid>
                                    <Grid item sm={3}>
                                        <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                            Priority
                                        </Typography>
                                        <Typography sx={{fontSize: 18}} gutterBottom>
                                            {alarm.priority === 0 ? "Normal" : alarm.priority === 1 ? "High" : "Urgent"}
                                        </Typography>
                                    </Grid>
                                    <Grid item sm={4}>
                                        <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                            Edge Value
                                        </Typography>
                                        <Typography sx={{fontSize: 18}} gutterBottom>
                                            {alarm.edgeValue}{tag.unit}
                                        </Typography>
                                    </Grid>
                                    <Grid item sm={2} sx={{
                                        display: "flex",
                                        flexDirection: "column",
                                        alignSelf: "center",
                                        justifyContent: "center"
                                    }}>
                                        <IconButton aria-label="delete" onClick={() => {
                                            deleteAlarm(alarm.id);
                                        }}>
                                            <Delete></Delete>
                                        </IconButton>
                                    </Grid>
                                </Grid>

                            </CardContent>
                        </Card>))}
                </Container>
            </Container>

        </div>
    );
}