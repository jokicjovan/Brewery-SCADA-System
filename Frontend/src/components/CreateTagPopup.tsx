import {useEffect, useState} from "react";
import {
    Box,
    Button,
    FormControl, FormControlLabel,
    Grid, InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    styled, Switch, SwitchProps,
    Tab,
    Tabs,
    TextField
} from "@mui/material";
import axios from "axios";
import {environment} from "../utils/Environment.tsx";



export function CreateTagPopup(){

    //Global
    const [tabIndex, setTabIndex] = useState(0);
    const [devices, setDevices] = useState([]);


    //Analog Input Tab
    const [driver, setDriver] = useState("");
    const [description, setDescription] = useState("");
    const [unit, setUnit] = useState("");
    const [error, setError] = useState("")
    const [iOaddress, setIOaddress] = useState("");
    const [scanTime, setScanTime] = useState(10);
    const [lowLimit, setLowLimit] = useState(0);
    const [highLimit, setHighLimit] = useState(10);
    const [scanOn, setScanOn] = useState(false);
    const SwitchAnalog = styled((props: SwitchProps) => (
        <Switch focusVisibleClassName=".Mui-focusVisible" checked={scanOn} onChange={(e)=>{setScanOn(e.target.checked)}}  size="medium" disableRipple {...props} />
    ))(({ theme }) => ({
        width: 42,
        height: 26,
        padding: 0,
        '& .MuiSwitch-switchBase': {
            padding: 0,
            margin: 2,
            transitionDuration: '300ms',
            '&.Mui-checked': {
                transform: 'translateX(16px)',
                color: '#fff',
                '& + .MuiSwitch-track': {
                    backgroundColor: theme.palette.mode === 'dark' ? '#2ECA45' : '#65C466',
                    opacity: 1,
                    border: 0,
                },
                '&.Mui-disabled + .MuiSwitch-track': {
                    opacity: 0.5,
                },
            },
            '&.Mui-focusVisible .MuiSwitch-thumb': {
                color: '#33cf4d',
                border: '6px solid #fff',
            },
            '&.Mui-disabled .MuiSwitch-thumb': {
                color:
                    theme.palette.mode === 'light'
                        ? theme.palette.grey[100]
                        : theme.palette.grey[600],
            },
            '&.Mui-disabled + .MuiSwitch-track': {
                opacity: theme.palette.mode === 'light' ? 0.7 : 0.3,
            },
        },
        '& .MuiSwitch-thumb': {
            boxSizing: 'border-box',
            width: 22,
            height: 22,
        },
        '& .MuiSwitch-track': {
            borderRadius: 26 / 2,
            backgroundColor: theme.palette.mode === 'light' ? '#E9E9EA' : '#39393D',
            opacity: 1,
            transition: theme.transitions.create(['background-color'], {
                duration: 500,
            }),
        },
    }));
    function handleSubmitAnalog(event : any) {
        event.preventDefault()
        axios.post(environment + `/api/Tag/addAnalogInput`, {
            description:description,
            driver:driver,
            iOAddress:iOaddress,
            scanTime:scanTime,
            scanOn:scanOn,
            lowLimit:lowLimit,
            highLimit:highLimit,
            unit:unit
        }).then(res => {
            if (res.status === 200){
                console.log("everythingOK");
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

    //Digital Input Tab
    const [driverDigital, setDriverDigital] = useState("");
    const [descriptionDigital, setDescriptionDigital] = useState("");
    const [errorDigital, setErrorDigital] = useState("")
    const [iOaddressDigital, setIOaddressDigital] = useState("");
    const [scanTimeDigital, setScanTimeDigital] = useState(10);
    const [scanOnDigital, setScanOnDigital] = useState(false);
    const SwitchDigital = styled((props: SwitchProps) => (
        <Switch focusVisibleClassName=".Mui-focusVisible" checked={scanOnDigital} onChange={(e)=>{setScanOnDigital(e.target.checked)}}  size="medium" disableRipple {...props} />
    ))(({ theme }) => ({
        width: 42,
        height: 26,
        padding: 0,
        '& .MuiSwitch-switchBase': {
            padding: 0,
            margin: 2,
            transitionDuration: '300ms',
            '&.Mui-checked': {
                transform: 'translateX(16px)',
                color: '#fff',
                '& + .MuiSwitch-track': {
                    backgroundColor: theme.palette.mode === 'dark' ? '#2ECA45' : '#65C466',
                    opacity: 1,
                    border: 0,
                },
                '&.Mui-disabled + .MuiSwitch-track': {
                    opacity: 0.5,
                },
            },
            '&.Mui-focusVisible .MuiSwitch-thumb': {
                color: '#33cf4d',
                border: '6px solid #fff',
            },
            '&.Mui-disabled .MuiSwitch-thumb': {
                color:
                    theme.palette.mode === 'light'
                        ? theme.palette.grey[100]
                        : theme.palette.grey[600],
            },
            '&.Mui-disabled + .MuiSwitch-track': {
                opacity: theme.palette.mode === 'light' ? 0.7 : 0.3,
            },
        },
        '& .MuiSwitch-thumb': {
            boxSizing: 'border-box',
            width: 22,
            height: 22,
        },
        '& .MuiSwitch-track': {
            borderRadius: 26 / 2,
            backgroundColor: theme.palette.mode === 'light' ? '#E9E9EA' : '#39393D',
            opacity: 1,
            transition: theme.transitions.create(['background-color'], {
                duration: 500,
            }),
        },
    }));
    function handleSubmitDigital(event : any) {
        event.preventDefault()
        axios.post(environment + `/api/Tag/addDigitalInput`, {
            description:descriptionDigital,
            driver:driverDigital,
            iOAddress:iOaddressDigital,
            scanTime:scanTimeDigital,
            scanOn:scanOnDigital,
        }).then(res => {
            if (res.status === 200){
                console.log("everythingOK");
            }
        }).catch((error) => {
            console.log(error);
            if (error.response?.status !== undefined && error.response.status === 404){
                setErrorDigital("Resource not found!");
            }
            else if (error.response?.status !== undefined && error.response.status === 400){
                setErrorDigital("Invalid input!");
            }
            else{
                setErrorDigital("An error occurred!");
            }
        });
    }



    useEffect( ()=>{
        axios.get(environment + `/api/Device/getAllAddresses`).then(response=>{
            setDevices(response.data);
            if (devices.length>0){
                setIOaddress(devices[0]);
                setIOaddressDigital(devices[0]);
            }
        });
    },[])





    return (
        <Box>
            <Box>
                <Tabs value={tabIndex} onChange={(_,newTabIndex)=>{setTabIndex(newTabIndex);}}
                      variant="fullWidth">
                    <Tab label="Analog Input" />
                    <Tab label="Digital Input" />
                </Tabs>
            </Box>
            <Box sx={{ padding: 2 }}>
                {tabIndex === 0 && (
                    <Box component="form" onSubmit={handleSubmitAnalog} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    required
                                    fullWidth
                                    autoFocus
                                    id="driverAnalog"
                                    label="Driver"
                                    name="driverAnalog"
                                    placeholder={"(e.g. DriverName)"}
                                    autoComplete="given-name"
                                    onChange={(e) => {setDriver(e.target.value)}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <FormControl sx={{width:"100%"}}>
                                    <InputLabel >IO Address</InputLabel>
                                    <Select
                                        value={iOaddress}
                                        required
                                        fullWidth
                                        autoFocus
                                        id="iOaddressAnalog"
                                        label="IO Address"
                                        name="iOaddressAnalog"
                                        placeholder={"(e.g. DriverName)"}
                                        onChange={(e) => {setIOaddress(e.target.value)}}
                                    >
                                            {devices.map((device) => (
                                                <MenuItem key={device} value={device}>{device}</MenuItem>
                                            ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12} sm={4}>
                                <TextField
                                    required
                                    fullWidth
                                    id="lowLimitAnalog"
                                    label="Low Limit"
                                    name="lowLimitAnalog"
                                    placeholder={"(e.g. 0)"}
                                    autoComplete="number"
                                    type="number"
                                    onChange={(e) => {setLowLimit(parseFloat(e.target.value))}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={4}>
                                <TextField
                                    required
                                    fullWidth
                                    id="highLimitAnalog"
                                    label="High Limit"
                                    name="highLimitAnalog"
                                    placeholder={"(e.g. 100)"}
                                    autoComplete="number"
                                    type="number"
                                    onChange={(e) => {setHighLimit(parseFloat(e.target.value))}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={4}>
                                <TextField
                                    required
                                    fullWidth
                                    autoFocus
                                    id="unitAnalog"
                                    label="Unit"
                                    name="unitAnalog"
                                    placeholder={"(e.g. cm)"}
                                    autoComplete="given-name"
                                    onChange={(e) => {setUnit(e.target.value)}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={8} sx={{margin:"auto",paddingLeft:"0",verticalAlign:"middle"}}>
                                <TextField
                                    required
                                    fullWidth
                                    id="scanTimeAnalog"
                                    label="Scan Time"
                                    name="scanTimeAnalog"
                                    placeholder={"(e.g. 10)"}
                                    autoComplete="number"
                                    type="number"
                                    inputProps={{min:0}}
                                    InputProps={{
                                        endAdornment: <InputAdornment position="end">sec</InputAdornment>,
                                    }}
                                    onChange={(e) => {setScanTime(parseFloat(e.target.value))}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={4} sx={{display:"flex",justifyContent:"center"}}>
                                <FormControlLabel
                                    control={<SwitchAnalog sx={{ m: 1 }} />}
                                    label="Scanning"
                                    labelPlacement="start"
                                />
                            </Grid>
                            <Grid item xs={12} sm={12} >
                                <TextField
                                    required
                                    fullWidth
                                    id="descriptionAnalog"
                                    label="Description"
                                    name="descriptionAnalog"
                                    placeholder={"(e.g. Some Description)"}
                                    onChange={(e) => {setDescription(e.target.value)}}
                                    multiline
                                    rows={4}
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
                            Create Analog Input
                        </Button>
                    </Box>
                )}
                {tabIndex === 1 && (
                    <Box component="form" onSubmit={handleSubmitDigital} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    required
                                    fullWidth
                                    autoFocus
                                    id="driverDigital"
                                    label="Driver"
                                    name="driverDigital"
                                    placeholder={"(e.g. DriverName)"}
                                    autoComplete="given-name"
                                    onChange={(e) => {setDriverDigital(e.target.value)}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <FormControl sx={{width:"100%"}}>
                                    <InputLabel >IO Address</InputLabel>
                                    <Select
                                        value={iOaddressDigital}
                                        required
                                        fullWidth
                                        autoFocus
                                        id="iOaddressDigital"
                                        label="IO Address"
                                        name="iOaddressDigital"
                                        placeholder={"(e.g. DriverName)"}
                                        onChange={(e) => {setIOaddressDigital(e.target.value)}}
                                    >
                                        {devices.map((device) => (
                                            <MenuItem key={device} value={device}>{device}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12} sm={8} sx={{margin:"auto",paddingLeft:"0",verticalAlign:"middle"}}>
                                <TextField
                                    required
                                    fullWidth
                                    id="scanTimeDigital"
                                    label="Scan Time"
                                    name="scanTimeDigital"
                                    placeholder={"(e.g. 10)"}
                                    autoComplete="number"
                                    type="number"
                                    inputProps={{min:0}}
                                    InputProps={{
                                        endAdornment: <InputAdornment position="end">sec</InputAdornment>,
                                    }}
                                    onChange={(e) => {setScanTimeDigital(parseFloat(e.target.value))}}
                                />
                            </Grid>
                            <Grid item xs={12} sm={4} sx={{display:"flex",justifyContent:"center"}}>
                                <FormControlLabel
                                    control={<SwitchDigital sx={{ m: 1 }} />}
                                    label="Scanning"
                                    labelPlacement="start"
                                />
                            </Grid>
                            <Grid item xs={12} sm={12} sx={{mb:5}} >
                                <TextField sx={{mb:1}}
                                    required
                                    fullWidth
                                    id="descriptionDigital"
                                    label="Description"
                                    name="descriptionDigital"
                                    placeholder={"(e.g. Some Description)"}
                                    onChange={(e) => {setDescriptionDigital(e.target.value)}}
                                    multiline
                                    rows={4}
                                />
                            </Grid>

                        </Grid>
                        <div>
                            <InputLabel style={{color:"red"}} sx={{mt: 5}}>{errorDigital}</InputLabel>
                        </div>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 1}}
                        >
                            Create Digital Input
                        </Button>
                    </Box>
                )}
            </Box>
        </Box>
    );
}