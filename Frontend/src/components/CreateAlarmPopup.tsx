import React, {useEffect, useState} from "react";
import {
    Box,
    Button, Fab,
    FormControl, FormControlLabel,
    Grid, InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    styled, Switch, SwitchProps,
    Tab,
    Tabs,
    TextField, Typography
} from "@mui/material";
import axios from "axios";
import {environment} from "../utils/Environment.tsx";
import {Add} from "@material-ui/icons";

export function CreateAlarmPopup({tag,closeModal}) {

    //Analog Input Tab
    const [alarmType, setAlarmType] = useState('Low');
    const [priority, setPriority] = useState("Normal");
    const [edgeValue, setEdgeValue] = useState(10);
    const [error, setError] = useState("");

    function handleSubmitAnalog(event : any) {
        event.preventDefault()
        axios.post(environment + `/api/Alarm/addAlarm`, {
            type:alarmType==="Low" ? 0 : 1,
            priority:priority === "High" ? 1 : priority === "Urgent" ? 2:0,
            edgeValue:edgeValue,
            unit:tag.unit,
            tagId:tag.id,
        }).then(res => {
            if (res.status === 200){
                closeModal();
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
    useEffect(()=>{
    },[])


    return (
        <Box>
            <Box component="form" onSubmit={handleSubmitAnalog} sx={{mt: 3}}>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={6}>
                        <FormControl sx={{width: "100%"}}>
                            <InputLabel>Type</InputLabel>
                            <Select
                                value={alarmType}
                                required
                                fullWidth
                                autoFocus
                                id="alarmType"
                                label="Alarm Type"
                                name="Alarm Type"
                                placeholder={"(e.g. Low)"}
                                onChange={(e) => {
                                    setAlarmType(e.target.value)
                                }}
                            >
                                <MenuItem key="Low" value="Low">Low</MenuItem>
                                <MenuItem key="High" value="High">High</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <FormControl sx={{width: "100%"}}>
                            <InputLabel>Priority</InputLabel>
                            <Select
                                value={priority}
                                required
                                fullWidth
                                autoFocus
                                id="priorityAlarm"
                                label="Alarm Priority"
                                name="priorityAlarm"
                                placeholder={"(e.g. Low)"}
                                onChange={(e) => {
                                    setPriority(e.target.value)
                                }}
                            >
                                <MenuItem key="Normal" value="Normal">Normal</MenuItem>
                                <MenuItem key="High" value="High">High</MenuItem>
                                <MenuItem key="Urgent" value="Urgent">Urgent</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={12} sx={{margin: "auto", paddingLeft: "0", verticalAlign: "middle"}}>
                        <TextField
                            required
                            fullWidth
                            id="edgeValueAlarm"
                            label="Edge value"
                            name="edgeValueAlarm"
                            placeholder={"(e.g. 10)"}
                            autoComplete="number"
                            type="number"
                            inputProps={{min: 0}}
                            InputProps={{
                                endAdornment: <InputAdornment position="end">{tag.unit}</InputAdornment>,
                            }}
                            onChange={(e) => {
                                setEdgeValue(parseFloat(e.target.value))
                            }}
                        />
                    </Grid>

                </Grid>
                <div>
                    <InputLabel style={{color: "red"}} sx={{mt: 2}}>{error}</InputLabel>
                </div>
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{mt: 3, mb: 1}}
                >
                    Create Analog Input
                </Button>
            </Box>
        </Box>
    );
}