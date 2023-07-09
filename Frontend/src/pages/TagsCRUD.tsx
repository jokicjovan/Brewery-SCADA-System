import {
    Box,
    Card,
    CardContent,
    Container, Fab,
    Grid,
    IconButton,
    Modal, Switch,
    Typography
} from "@mui/material";
import React, {useEffect, useState} from "react";
import {CreateTagPopup} from "../components/CreateTagPopup.tsx";
import {Delete, Add, Notifications} from "@material-ui/icons";
import axios from "axios";
import {environment} from "../utils/Environment";
import {AnalogData, DigitalData} from "../models/DataInterfaces";
import AlarmList from "../components/AlarmList";
import signalRTagService from "../services/signalRTagService.ts";

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
    const [openModal, setOpenModal] = React.useState(false);
    const [currentTag, setCurrentTag] = React.useState({});
    const [tags, setTags] = useState({analogInputs:[],digitalInputs:[]});
    const handleOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };

    useEffect( ()=>{
        axios.get(environment + `/api/Tag/getMyInputs`).then(response=>{
            setTags(response.data);
        });
    },[open])

    function deleteAnalog(id:string){
        axios.delete(environment + `/api/Tag/deleteAnalogInput/`+ id).then(()=>{
            axios.get(environment + `/api/Tag/getMyInputs`).then(response=>{
                setTags(response.data);
            });
        });
    }
    function deleteDigital(id:string){
        axios.delete(environment + `/api/Tag/deleteDigitalInput/` + id).then(()=>{
            axios.get(environment + `/api/Tag/getMyInputs`).then(response=>{
                setTags(response.data);
            });
        });
    }
    function switchValue(id:string,type:number){
        axios.post(environment + `/api/Tag/switchTag?type=`+ type+"&tagId="+id).then(()=>{
            axios.get(environment + `/api/Tag/getMyInputs`).then(response=>{
                setTags(response.data);
            });
        });
    }

    useEffect(() => {
        signalRTagService.startConnection();

        signalRTagService.receiveAnalogData((newAnalogData : AnalogData) => {
            setTags(prevTags => {
                const updatedAnalogInputs = prevTags.analogInputs.map(analogInput => {
                    if (analogInput.id === newAnalogData.tagId) {
                        analogInput['value'] = newAnalogData.value
                    }
                    return analogInput;
                });

                return {
                    ...prevTags,
                    analogInputs: updatedAnalogInputs
                };
            });
        });

        signalRTagService.receiveDigitalData((newDigitalData : DigitalData) => {
            setTags(prevTags => {
                const updatedDigitalInputs = prevTags.digitalInputs.map(digitalInput => {
                    if (digitalInput.id === newDigitalData.tagId) {
                        digitalInput['value'] = newDigitalData.value
                    }
                    return digitalInput;
                });

                return {
                    ...prevTags,
                    analogInputs: updatedDigitalInputs
                };
            });
        });
    }, [tags]);

    return (
        <div>
            <Typography sx={{textAlign:"center",fontSize:"35px",fontWeight:"bold", mb:3}}>Tags</Typography>
            <Fab variant="extended" sx={{position:"fixed",bottom:"50px",right:"50px"}} onClick={handleOpen}>
                <Add sx={{ mr: 1 }} />
                Add Tag
            </Fab>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={open}
                onClose={()=>{setOpen(false)}}
                closeAfterTransition
            >
                <Box sx={style}>
                    <CreateTagPopup closeModal={handleClose}></CreateTagPopup>
                </Box>
            </Modal>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={openModal}
                onClose={()=>{setOpenModal(false)}}
                closeAfterTransition
            >
                <Box sx={style}>
                    <AlarmList tag={currentTag}></AlarmList>
                </Box>

            </Modal>
            <Container sx={{width:"70%"}}>
                {tags.analogInputs.map((device : any) => (
                    <Card key={device.id} sx={{height:"200px", mb:2}}>
                        <CardContent>
                            <Grid container spacing={4}>
                                <Grid item  sm={6}>
                                    <Grid container spacing={2}>
                                        <Grid item sm={6}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography variant="h6" component="div">
                                                {device.ioAddress}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={6}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Driver
                                            </Typography>
                                            <Typography variant="h6" component="div">
                                                {device.driver}
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Description
                                    </Typography>
                                    <Typography sx={{fontSize: 15}} gutterBottom>
                                        {device.description}
                                    </Typography>

                                </Grid>
                                <Grid item sm={1}>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Unit
                                    </Typography>
                                    <Typography sx={{fontSize:18}} component="div">
                                        {device.unit}
                                    </Typography>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Low Limit
                                    </Typography>
                                    <Typography sx={{fontSize:18}} component="div">
                                        {device.lowLimit}{device.unit}
                                    </Typography>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        High Limit
                                    </Typography>
                                    <Typography sx={{fontSize:18}} component="div">
                                        {device.highLimit}{device.unit}
                                    </Typography>
                                </Grid>
                                <Grid item sm={1}>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Scan Time
                                    </Typography>
                                    <Typography sx={{fontSize:18}} component="div">
                                        {device.scanTime}s
                                    </Typography>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Scanning
                                    </Typography>
                                    {device.scanOn &&
                                    <Typography sx={{fontSize:18}} component="div">
                                        On
                                    </Typography>}
                                    {!device.scanOn &&
                                        <Typography sx={{fontSize:18}} component="div">
                                            Off
                                        </Typography>}
                                </Grid>
                                <Grid item sm={2} sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}>
                                    <Typography sx={{fontSize: 12,textAlign:"center"}} color="text.secondary" gutterBottom>
                                        Value
                                    </Typography>
                                    <Typography variant="h3" sx={{textAlign:"center"}} component="div">
                                        {device.value}
                                    </Typography>
                                </Grid>

                                <Grid item sm={2} sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}>
                                    <IconButton aria-label="delete" onClick={()=>{deleteAnalog(device.id);}}>
                                        <Delete></Delete>
                                    </IconButton>
                                    <IconButton aria-label="add alert" onClick={()=>{setCurrentTag(device);setOpenModal(true);}}>
                                        <Notifications></Notifications>
                                    </IconButton>
                                    <Switch sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}
                                            checked={device.scanOn}
                                            inputProps={{ 'aria-label': 'controlled' }}
                                            onChange={()=>{switchValue(device.id,0);}}
                                    />
                                </Grid>
                            </Grid>


                        </CardContent>
                    </Card>))}
                {tags.digitalInputs.map((device : any) => (
                    <Card key={device.id} sx={{height:"200px", mb:2}}>
                        <CardContent>
                            <Grid container spacing={4}>
                                <Grid item  sm={6}>
                                    <Grid container spacing={2}>
                                        <Grid item sm={6}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography variant="h6" component="div">
                                                {device.ioAddress}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={6}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Driver
                                            </Typography>
                                            <Typography variant="h6" component="div">
                                                {device.driver}
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Description
                                    </Typography>
                                    <Typography sx={{fontSize: 15}} gutterBottom>
                                        {device.description}
                                    </Typography>

                                </Grid>
                                <Grid item sm={1}>
                                </Grid>
                                <Grid item sm={1}>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Scan Time
                                    </Typography>
                                    <Typography sx={{fontSize:18}} component="div">
                                        {device.scanTime}s
                                    </Typography>
                                    <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                        Scanning
                                    </Typography>
                                    {device.scanOn &&
                                        <Typography sx={{fontSize:18}} component="div">
                                            On
                                        </Typography>}
                                    {!device.scanOn &&
                                        <Typography sx={{fontSize:18}} component="div">
                                            Off
                                        </Typography>}
                                </Grid>
                                <Grid item sm={2} sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}>
                                    <Typography sx={{fontSize: 12,textAlign:"center"}} color="text.secondary" gutterBottom>
                                        Value
                                    </Typography>
                                    <Typography variant="h3" sx={{textAlign:"center"}} component="div">
                                        12cm
                                    </Typography>
                                </Grid>

                                <Grid item sm={2} sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}>
                                    <IconButton aria-label="delete" onClick={()=>{deleteDigital(device.id);}}>
                                        <Delete></Delete>
                                    </IconButton>
                                    <Switch sx={{display:"flex",flexDirection:"column",alignSelf:"center",justifyContent:"center"}}
                                            checked={device.scanOn}
                                            inputProps={{ 'aria-label': 'controlled' }}
                                            onChange={()=>{switchValue(device.id,1);}}
                                    />
                                </Grid>
                            </Grid>


                        </CardContent>
                    </Card>))}
            </Container>

        </div>
    );
}