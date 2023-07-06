import { useEffect, useState } from "react";
import signalRAlarmService from "../services/signalRAlarmService.ts";
import {AlarmData} from "../models/DataInterfaces.ts";
export default function NotificationPopup() {
    const [alarmData, setAlarmData] = useState<AlarmData[]>([]);

    useEffect(() => {
        signalRAlarmService.startConnection();

        signalRAlarmService.receiveAlarmData((newAlarmData : AlarmData) => {
            setAlarmData((prevAlarmData) => [...prevAlarmData, newAlarmData]);
        });
    }, []);

    return (
        <div style={{position: "absolute"}}>
            <ul>
                {alarmData.map((data, index) => (
                    <li key={index}>
                        <strong>{data.timestamp}: </strong>
                    </li>
                ))}
            </ul>
        </div>
    );
};
