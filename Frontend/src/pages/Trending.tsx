import {
    Box,
    Button,
    Card,
    CardContent,
    Container,
    Fab,
    Grid,
    IconButton,
    Modal, Paper,
    styled,
    Switch,
    Table,
    TableBody,
    TableCell,
    tableCellClasses,
    TableContainer,
    TableHead,
    TableRow,
    TextField,
    Typography
} from "@mui/material";
import React, {useEffect, useState} from "react";
import {CreateTagPopup} from "../components/CreateTagPopup.tsx";
import axios from "axios";
import {environment} from "../utils/Environment";
import {AnalogData, DigitalData} from "../models/DataInterfaces";
import AlarmList from "../components/AlarmList";
import signalRTagService from "../services/signalRTagService.ts";
import {Add, Delete, Edit, Notifications} from "@mui/icons-material";

export default function Trending() {
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
    const styleSmall = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 300,
        height: 150,
        bgcolor: 'white',
        boxShadow: 24,
        p: 4,
    };
    const iconButtonStyle = {
        display: "flex",
        flexDirection: "column",
        alignSelf: "center",
        justifyContent: "center",
        width: "30px",
        height: "30px"
    }

    const [open, setOpen] = React.useState(false);
    const [openModal, setOpenModal] = React.useState(false);
    const [openModalAnalog, setOpenModalAnalog] = React.useState(false);
    const [openModalDigital, setOpenModalDigital] = React.useState(false);
    const [currentTag, setCurrentTag] = React.useState({id: ""});
    const [tags, setTags] = useState({analogInputs: [], digitalInputs: []});

    const handleOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        axios.get(environment + `/api/Tag/getMyInputs`).then(response => {
            setTags(response.data);
        });
    }, [open, openModalAnalog, openModalDigital])


    useEffect(() => {
        signalRTagService.startConnection();

        signalRTagService.receiveAnalogData((newAnalogData: AnalogData) => {
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

        signalRTagService.receiveDigitalData((newDigitalData: DigitalData) => {
            setTags(prevTags => {
                const updatedDigitalInputs = prevTags.digitalInputs.map(digitalInput => {
                    if (digitalInput.id === newDigitalData.tagId) {
                        digitalInput['value'] = newDigitalData.value
                    }
                    return digitalInput;
                });

                return {
                    ...prevTags,
                    digitalInputs: updatedDigitalInputs
                };
            });
        });
    }, [tags]);
    const StyledTableCell = styled(TableCell)(({ theme }) => ({
        [`&.${tableCellClasses.head}`]: {
            backgroundColor: theme.palette.common.black,
            color: theme.palette.common.white,
        },
        [`&.${tableCellClasses.body}`]: {
            fontSize: 14,
        },
    }));

    const StyledTableRow = styled(TableRow)(({ theme }) => ({
        '&:nth-of-type(odd)': {
            backgroundColor: theme.palette.action.hover,
        },
        // hide last border
        '&:last-child td, &:last-child th': {
            border: 0,
        },
    }));
        return (<div>
                <Typography sx={{textAlign: "center", fontSize: "35px", fontWeight: "bold", mb: 3}}>Trending</Typography>
                <Modal
                    aria-labelledby="transition-modal-title"
                    aria-describedby="transition-modal-description"
                    open={openModal}
                    onClose={() => {
                        setOpenModal(false)
                    }}
                    closeAfterTransition
                >
                    <Box sx={style}>
                        <AlarmList tag={currentTag}></AlarmList>
                    </Box>

                </Modal>
            <TableContainer  >
                <Table sx={{ minWidth: 700,maxWidth:1500,backgroundColor:"white",margin:"0 auto"}} aria-label="customized table">
                    <TableHead>
                        <TableRow>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell align="center">Type</StyledTableCell>
                            <StyledTableCell align="center">IO Address</StyledTableCell>
                            <StyledTableCell align="center">Driver</StyledTableCell>
                            <StyledTableCell align="center">Description</StyledTableCell>
                            <StyledTableCell align="center">Scan Time(s)</StyledTableCell>
                            <StyledTableCell align="center">Scanning</StyledTableCell>
                            <StyledTableCell align="center">Low Limit</StyledTableCell>
                            <StyledTableCell align="center">High Limit</StyledTableCell>
                            <StyledTableCell align="center">Unit</StyledTableCell>
                            <StyledTableCell align="center">Value</StyledTableCell>
                            <StyledTableCell align="center">Alarm</StyledTableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {tags.analogInputs.map((tag) => (
                            <StyledTableRow key={tag.id}>
                                <StyledTableCell component="th" scope="row">
                                    {tag.id}
                                </StyledTableCell>
                                <StyledTableCell align="center">{tag.lowLimit==undefined?"Digital":"Analog"}</StyledTableCell>
                                <StyledTableCell align="center">{tag.ioAddress}</StyledTableCell>
                                <StyledTableCell align="center">{tag.driver}</StyledTableCell>
                                <StyledTableCell align="center">{tag.description}</StyledTableCell>
                                <StyledTableCell align="center">{tag.scanTime}s</StyledTableCell>
                                <StyledTableCell align="center">{tag.scanOn?"On":"Off"}</StyledTableCell>
                                <StyledTableCell align="center">{tag.lowLimit}</StyledTableCell>
                                <StyledTableCell align="center">{tag.highLimit}</StyledTableCell>
                                <StyledTableCell align="center">{tag.unit}</StyledTableCell>
                                <StyledTableCell align="center">{tag.value.toFixed(3)}</StyledTableCell>
                                <StyledTableCell align="center"><IconButton aria-label="add alert"
                                                                            onClick={() => {
                                                                                setCurrentTag(tag);
                                                                                setOpenModal(true);
                                                                            }}>
                                    <Notifications></Notifications>
                                </IconButton></StyledTableCell>
                            </StyledTableRow>
                        ))}
                        {tags.digitalInputs.map((tag) => (
                            <StyledTableRow key={tag.id}>
                                <StyledTableCell component="th" scope="row">
                                    {tag.id}
                                </StyledTableCell>
                                <StyledTableCell align="center">{tag.lowLimit==undefined?"Digital":"Analog"}</StyledTableCell>
                                <StyledTableCell align="center">{tag.ioAddress}</StyledTableCell>
                                <StyledTableCell align="center">{tag.driver}</StyledTableCell>
                                <StyledTableCell align="center">{tag.description}</StyledTableCell>
                                <StyledTableCell align="center">{tag.scanTime}s</StyledTableCell>
                                <StyledTableCell align="center">{tag.scanOn?"On":"Off"}</StyledTableCell>
                                <StyledTableCell align="center"></StyledTableCell>
                                <StyledTableCell align="center"></StyledTableCell>
                                <StyledTableCell align="center"></StyledTableCell>
                                <StyledTableCell align="center">{tag.value == 1 ? "On" : "Off"}</StyledTableCell>
                                <StyledTableCell align="center"></StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            </div>
        );


}