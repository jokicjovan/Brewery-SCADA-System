import {
    Card,
    CardContent, Container,
    FormControl,
    Grid,
    IconButton,
    InputLabel,
    MenuItem,
    Select,
    Typography
} from "@mui/material";
import {DatePicker} from '@mui/x-date-pickers/DatePicker';
import React, {useEffect} from "react";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {format, parse, parseISO} from "date-fns";
import {environment} from "../utils/Environment";
import axios from "axios";
import {ArrowDownward, ArrowUpward} from "@mui/icons-material";


export default function Reports() {

    const [reportType, setReportType] = React.useState("alarmsbyDate")

    //Alarms By Date
    const [startDate, setStartDate] = React.useState(parse('2023/01/01', 'yyyy/MM/dd', new Date()))
    const [endDate, setEndDate] = React.useState(parse('2024/01/01', 'yyyy/MM/dd', new Date()))
    const [alarmsByTime, setAlarmsByTime] = React.useState([]);
    const [sortAlarmByDate, setSortAlarmByDate] = React.useState("Time");
    const [sortDirectionAlarmByDate, setSortDirectionAlarmByDate] = React.useState(true);

    //Tags By Date
    const [startDateTag, setStartDateTag] = React.useState(parse('2023/01/01', 'yyyy/MM/dd', new Date()))
    const [endDateTag, setEndDateTag] = React.useState(parse('2024/01/01', 'yyyy/MM/dd', new Date()))
    const [tagsByTime, setTagsByTime] = React.useState([]);
    const [sortDirectionTagByDate, setSortDirectionTagByDate] = React.useState(true);

    //All Analog Tags
    const [allAnalogTags, setAllAnalogTags] = React.useState([]);
    const [sortDirectionAllAnalogTags, setSortDirectionAllAnalogTags] = React.useState(true);


    //All Digital Tags
    const [allDigitalTags, setAllDigitalTags] = React.useState([]);
    const [sortDirectionAllDigitalTags, setSortDirectionAllDigitalTags] = React.useState(true);

    //Alarms By Priority
    const [priority, setPriority] = React.useState("Normal")
    const [alarmsByPriority, setAlarmsByPriority] = React.useState([]);
    const [sortDirectionAlarmByPriority, setSortDirectionAlarmByPriority] = React.useState(true);

    //Tag By Id
    const [currentTag, setCurrentTag] = React.useState("")
    const [curretTypeOfTag, setCurrentTypeOfTag] = React.useState("")
    const [tagsById, setTagsById] = React.useState([]);
    const [myTags, setMyTags] = React.useState([]);
    const [sortDirectionTagsById, setSortDirectionTagsById] = React.useState(true);

    function getAllAlarmsByTime() {
        axios.get(environment + `/api/Reports/allAlarmsByTime?timeFrom=` + format(startDate, "yyyy-MM-dd") + "&timeTo=" + format(endDate, "yyyy-MM-dd")).then(response => {
            const result = response.data;
            const direction=sortDirectionAlarmByDate ? 1:-1;
            if (sortAlarmByDate == "Priority")
                result.sort((a, b) =>direction*(a.alarm.priority - b.alarm.priority));
            else
                result.sort((a, b) =>direction*(parseISO(a.timestamp) - parseISO(b.timestamp)));
            console.log(result);
            setAlarmsByTime(result);
        });
    }
    function getAllAlarmsByPriority(){
        axios.get(environment + `/api/Reports/allAlarmsByPriority?alarmPriority=`+(priority=="Normal"?0:priority=="High"?1:2).toString()).then(response => {

            const result = response.data;
            const direction=sortDirectionAlarmByPriority ? 1:-1;
            result.sort((a, b) =>direction*(parseISO(a.timestamp) - parseISO(b.timestamp)));
            setAlarmsByPriority(result);
        });
    }

    function getAllTagsByDate(){
        axios.get(environment + `/api/Reports/allTagValuesByTime?timeFrom=` + format(startDateTag, "yyyy-MM-dd") + "&timeTo=" + format(endDateTag, "yyyy-MM-dd")).then(response => {
            const resultObj = response.data;
            const result=resultObj.ioAnalogDataList.concat(resultObj.ioDigitalDataList)
            const direction=sortDirectionTagByDate ? 1:-1;
            result.sort((a, b) =>direction*(parseISO(a.timestamp) - parseISO(b.timestamp)));
            setTagsByTime(result);
        });
    }

    function getAllAnalogTags(){
        axios.get(environment + `/api/Reports/latestAnalogValues`).then(response => {
            const result = response.data;
            const direction=sortDirectionAllAnalogTags ? 1:-1;
            result.sort((a, b) =>direction*(parseISO(a.timestamp) - parseISO(b.timestamp)));
            setAllAnalogTags(result);
        });
    }

    function getAllDigitalTags(){
        axios.get(environment + `/api/Reports/latestDigitalValues`).then(response => {
            const result = response.data;
            const direction=sortDirectionAllDigitalTags ? 1:-1;
            result.sort((a, b) =>direction*(parseISO(a.timestamp) - parseISO(b.timestamp)));
            setAllDigitalTags(result);
        });
    }
    function getAllTagsById(){
        if (currentTag!="" && curretTypeOfTag!="")
            if (curretTypeOfTag=="Analog")
                axios.get(environment + `/api/Reports/allAnalogTagValues?tagId=`+currentTag).then(response => {
                    const result=response.data;
                    const direction=sortDirectionTagsById ? 1:-1;
                    result.sort((a, b) =>direction*(a.value - b.value));
                    setTagsById(result);
                });
            else{
                axios.get(environment + `/api/Reports/allDigitalTagValues?tagId=`+currentTag).then(response => {
                    const result=response.data;
                    const direction=sortDirectionTagsById ? 1:-1;
                    result.sort((a, b) =>direction*(a.value - b.value));
                    setTagsById(result);
                });
            }
    }
    function handleTagChange( val) {
        setCurrentTag(val);
        const selected=myTags.find(obj => obj.id === val);
        setCurrentTypeOfTag(selected.lowLimit!=undefined? "Analog":"Digital")
    }

    useEffect(()=>{
        axios.get(environment + `/api/Tag/getMyInputs`).then(response=>{
            const resultObj = response.data;
            const result=resultObj.analogInputs.concat(resultObj.digitalInputs)
            setMyTags(result);
        });
    },[])

    useEffect(() => {
        updateReport();
    }, [startDate, endDate, sortAlarmByDate,sortDirectionAlarmByDate,priority,sortDirectionAlarmByPriority,reportType,startDateTag,endDateTag,sortDirectionTagByDate,sortDirectionAllAnalogTags,sortDirectionAllDigitalTags,currentTag,curretTypeOfTag,sortDirectionTagsById])

    function updateReport() {
        if (reportType == "alarmsByDate")
            getAllAlarmsByTime();
        else if (reportType == "alarmsByPriority")
            getAllAlarmsByPriority();
        else if (reportType == "tagsByDate")
            getAllTagsByDate();
        else if (reportType == "allAnalogTags")
            getAllAnalogTags();
        else if (reportType == "allDigitalTags")
            getAllDigitalTags();
        else if (reportType == "tagsById")
            getAllTagsById();
    }

    return <LocalizationProvider dateAdapter={AdapterDateFns}>
        <div style={{display: "flex", justifyContent: "center", flexDirection: "column"}}>
            <h1 style={{textAlign: "center"}}>Reports</h1>
            <FormControl sx={{width: "100%", maxWidth:"800px", justifyContent: "center",backgroundColor:"white", margin:"0 auto"}}>
                <InputLabel>Priority</InputLabel>
                <Select
                    value={reportType}
                    required
                    fullWidth
                    autoFocus
                    id="priorityAlarm"
                    label="Alarm Priority"
                    name="priorityAlarm"
                    placeholder={"(e.g. Low)"}
                    onChange={(e) => {
                        setReportType(e.target.value);
                    }}
                >
                    <MenuItem key="alarmsByDate" value="alarmsByDate">Get Alarms By Date</MenuItem>
                    <MenuItem key="alarmsByPriority" value="alarmsByPriority">Get Alarms By Priority</MenuItem>
                    <MenuItem key="tagsByDate" value="tagsByDate">Get All Tags By Date</MenuItem>
                    <MenuItem key="allAnalogTags" value="allAnalogTags">Get All Analog Tags</MenuItem>
                    <MenuItem key="allDigitalTags" value="allDigitalTags">Get All Digital Tags</MenuItem>
                    <MenuItem key="tagsById" value="tagsById">Get Tag Values By Id</MenuItem>
                </Select>
            </FormControl>
            {reportType === "alarmsByDate" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <DatePicker

                            sx={{mt: 3, marginRight: 3,backgroundColor:"white"}}
                            label="Start Date"
                            value={startDate}
                            onChange={(newValue) => {
                                setStartDate(newValue);
                            }}
                        />
                        <DatePicker
                            sx={{mt: 3, marginLeft: 3, marginRight: 3,backgroundColor:"white"}}
                            label="End Date"
                            value={endDate}
                            onChange={(newValue) => {
                                setEndDate(newValue);
                            }}
                        />
                        <FormControl sx={{width: "150px", justifyContent: "center", marginLeft: 3, marginTop: 3,backgroundColor:"white"}}>
                            <InputLabel>Sort Criterium</InputLabel>
                            <Select
                                value={sortAlarmByDate}
                                required
                                fullWidth
                                autoFocus
                                id="sortAlarmByDate"
                                label="Sort By"
                                name="sortAlarmByDate"
                                placeholder={"(e.g. Low)"}
                                onChange={(e) => {
                                    setSortAlarmByDate(e.target.value);
                                }}
                            >
                                <MenuItem key="Time" value="Time">Time</MenuItem>
                                <MenuItem key="Priority" value="Priority">Priority</MenuItem>
                            </Select>
                        </FormControl>
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionAlarmByDate(!sortDirectionAlarmByDate);
                        }}>
                            {sortDirectionAlarmByDate &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionAlarmByDate &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {alarmsByTime.map((alarm) => (
                            <Card key={alarm.timestamp} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={1}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Type
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.type === 0 ? "Low" : "High"}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={1}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Priority
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.priority === 0 ? "Normal" : alarm.alarm.priority === 1 ? "High" : "Urgent"}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Edge Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.edgeValue}{alarm.alarm.analogInput.unit}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.value != null && (alarm.value.toFixed(3) + " " + alarm.alarm.analogInput.unit)}
                                                {alarm.value == null && "NaN"}
                                            </Typography>
                                        </Grid>

                                        <Grid item sm={3}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(alarm.timestamp), "dd.MM.yyyy.")}, {format(parseISO(alarm.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={3}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.analogInput.id}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }
            {reportType === "alarmsByPriority" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <FormControl sx={{width: "150px", justifyContent: "center", marginLeft: 3, marginTop: 3,backgroundColor:"white"}}>
                            <InputLabel>Priority</InputLabel>
                            <Select
                                value={priority}
                                required
                                fullWidth
                                autoFocus
                                id="priorityAlarmByPriority"
                                label="Select Priority"
                                name="priorityAlarmByPriority"
                                placeholder={"(e.g. Low)"}
                                onChange={(e) => {
                                    setPriority(e.target.value);
                                }}
                            >
                                <MenuItem key="Normal" value="Normal">Normal</MenuItem>
                                <MenuItem key="High" value="High">High</MenuItem>
                                <MenuItem key="Urgent" value="Urgent">Urgent</MenuItem>
                            </Select>
                        </FormControl>
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionAlarmByPriority(!sortDirectionAlarmByPriority);
                        }}>
                            {sortDirectionAlarmByPriority &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionAlarmByPriority &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {alarmsByPriority.map((alarm) => (
                            <Card key={alarm.timestamp} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={1}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Type
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.type === 0 ? "Low" : "High"}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={1}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Priority
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.priority === 0 ? "Normal" : alarm.alarm.priority === 1 ? "High" : "Urgent"}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Edge Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.edgeValue}{alarm.alarm.analogInput.unit}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.value != null && (alarm.value + " " + alarm.alarm.analogInput.unit)}
                                                {alarm.value == null && "NaN"}
                                            </Typography>
                                        </Grid>

                                        <Grid item sm={3}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(alarm.timestamp), "dd.MM.yyyy.")}, {format(parseISO(alarm.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                        <Grid item sm={3}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {alarm.alarm.analogInput.id}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }
            {reportType === "tagsByDate" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <DatePicker

                            sx={{mt: 3, marginRight: 3,backgroundColor:"white"}}
                            label="Start Date"
                            value={startDateTag}
                            onChange={(newValue) => {
                                setStartDateTag(newValue);
                            }}
                        />
                        <DatePicker
                            sx={{mt: 3, marginLeft: 3, marginRight: 3,backgroundColor:"white"}}
                            label="End Date"
                            value={endDateTag}
                            onChange={(newValue) => {
                                setEndDateTag(newValue);
                            }}
                        />
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionTagByDate(!sortDirectionTagByDate);
                        }}>
                            {sortDirectionTagByDate &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionTagByDate &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {tagsByTime.map((tag) => (
                            <Card key={tag.id} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.tagId}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {Math.round(1000 * tag.value) / 1000}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.ioAddress}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(tag.timestamp), "dd.MM.yyyy.")}, {format(parseISO(tag.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }
            {reportType === "allAnalogTags" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <Typography sx={{fontSize: 18,mt:3,display:"flex",alignSelf:"center"}}>
                            Sort By Time
                        </Typography>
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionAllAnalogTags(!sortDirectionAllAnalogTags);
                        }}>
                            {sortDirectionAllAnalogTags &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionAllAnalogTags &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {allAnalogTags.map((tag) => (
                            <Card key={tag.id} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.tagId}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {Math.round(1000 * tag.value) / 1000}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.ioAddress}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(tag.timestamp), "dd.MM.yyyy.")}, {format(parseISO(tag.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }

            {reportType === "allDigitalTags" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <Typography sx={{fontSize: 18,mt:3,display:"flex",alignSelf:"center"}}>
                            Sort By Time
                        </Typography>
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionAllDigitalTags(!sortDirectionAllDigitalTags);
                        }}>
                            {sortDirectionAllDigitalTags &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionAllDigitalTags &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {allDigitalTags.map((tag) => (
                            <Card key={tag.id} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.tagId}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {Math.round(1000 * tag.value) / 1000}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.ioAddress}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(tag.timestamp), "dd.MM.yyyy.")}, {format(parseISO(tag.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }

            {reportType === "tagsById" &&
                <div style={{display: "flex", flexDirection: "column"}}>
                    <div style={{display: "flex", flexDirection: "row", justifyContent: "center"}}>
                        <FormControl sx={{mt:3,width:"350px",backgroundColor:"white"}} >
                            <InputLabel >Tags</InputLabel>
                            <Select
                                value={currentTag}
                                required
                                fullWidth
                                autoFocus
                                id="currentTag"
                                label="Tags"
                                name="currentTag"
                                placeholder={"(e.g. TagId)"}
                                onChange={(e) => {handleTagChange(e.target.value)}}
                            >
                                {myTags.map((myTag) => (
                                    <MenuItem key={myTag.id} value={myTag.id}>{myTag.id}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                        <IconButton sx={{mt: 3, width:"55px",height:"55px"}} aria-label="delete" onClick={() => {
                            setSortDirectionTagsById(!sortDirectionTagsById);
                        }}>
                            {sortDirectionTagsById &&
                                <ArrowUpward></ArrowUpward>}
                            {!sortDirectionTagsById &&
                                <ArrowDownward></ArrowDownward>}
                        </IconButton>
                    </div>
                    <Container sx={{width: "90%", mt: 2}}>
                        {tagsById.map((tag) => (
                            <Card key={tag.id} sx={{height: "100px", mb: 2}}>
                                <CardContent>
                                    <Grid container spacing={3}>
                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Tag Id
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.tagId}
                                            </Typography>

                                        </Grid>
                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Value
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {Math.round(1000 * tag.value) / 1000}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={2}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                IO Address
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {tag.ioAddress}
                                            </Typography>

                                        </Grid>

                                        <Grid item sm={4}>
                                            <Typography sx={{fontSize: 12}} color="text.secondary" gutterBottom>
                                                Timestamp
                                            </Typography>
                                            <Typography sx={{fontSize: 18}} gutterBottom>
                                                {format(parseISO(tag.timestamp), "dd.MM.yyyy.")}, {format(parseISO(tag.timestamp), "HH:mm.ss")}
                                            </Typography>
                                        </Grid>
                                    </Grid>

                                </CardContent>
                            </Card>))}
                    </Container>
                </div>
            }
        </div>
    </LocalizationProvider>;
}