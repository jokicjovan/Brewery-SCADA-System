import { useEffect, useState } from "react";
import signalRAlarmService from "../services/signalRAlarmService.ts";
import {AlarmData} from "../models/DataInterfaces.ts";
import "./AlarmPopup.css"
const Alarm = ({ alarmData }: { alarmData: AlarmData }) => {
    const [visible, setVisible] = useState(true);

    useEffect(() => {
        const timer = setTimeout(() => {
            setVisible(false);
        }, 5000);

        return () => {
            clearTimeout(timer);
        };
    }, []);

    const handleDelete = () => {
        setVisible(false);
    };

    return visible ?
        <div className="alarm">
            <button className="delete-button" onClick={handleDelete}>X</button>
            <div>{alarmData.alarm.analogInput.ioAddress}</div>
            <div>{new Date(alarmData.timestamp).toLocaleString()}</div>
        </div> : null;
};

export default function AlarmPopup() {
    const [alarmsData, setAlarmsData] = useState<AlarmData[]>([]);

    useEffect(() => {
        signalRAlarmService.startConnection();

        signalRAlarmService.receiveAlarmData((newAlarmData : AlarmData) => {
            setAlarmsData((prevAlarmsData) => [...prevAlarmsData, newAlarmData]);
            console.log(newAlarmData.alarm.analogInput.ioAddress);
        });
    }, []);

    return (
        <div className="alarm-container">
            {alarmsData.map((alarmData, index) => (
                <Alarm key={index} alarmData={alarmData} />
            ))}
        </div>
    );
}
