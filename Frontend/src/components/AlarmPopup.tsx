import { useEffect, useState } from "react";
import signalRAlarmService from "../services/signalRAlarmService.ts";
import {AlarmData} from "../models/DataInterfaces.ts";
import "./AlarmPopup.css"
import toast from "react-hot-toast";

export default function AlarmPopup() {
    const [, setAlarmsData] = useState<AlarmData[]>([]);

    useEffect(() => {
        signalRAlarmService.startConnection();

        signalRAlarmService.receiveAlarmData((newAlarmData : AlarmData) => {
            setAlarmsData((prevAlarmsData) => [...prevAlarmsData, newAlarmData]);
            let duration = 5000;
            let backgroundColor = "#ffe900";
            console.log(newAlarmData.alarm.priority)
            //let icon = "⚠️";
            if (newAlarmData.alarm.priority == 1){
                duration = 10000;
                backgroundColor = "#ff9c00"
                //icon = "⛔"
            }
            else if (newAlarmData.alarm.priority == 2){
                duration = Infinity;
                backgroundColor = "#ff2a00"
                //icon = "🚨"
            }
            toast.error((t) => (
                <div className="alarm">
                    <button className="delete-button" onClick={() => toast.dismiss(t.id)}>
                      X
                    </button>
                    <span>
                        <b>Device:</b> {newAlarmData.alarm.analogInput.ioAddress}
                    </span>
                    <span>
                        <b>TagId:</b> {newAlarmData.alarm.analogInput.id}
                    </span>
                    <span>
                        <b>Type:</b> {newAlarmData.alarm.type == 0 ? "Low" : "High"}
                    </span>
                    <span>
                        <b>Edge value:</b> {newAlarmData.alarm.edgeValue}
                    </span>
                    <span>
                        <b>Current value:</b> {newAlarmData.value.toFixed(3)}
                    </span>
                    <span>
                        <b>Time:</b> {new Date(newAlarmData.timestamp).toLocaleString()}
                    </span>
                </div>
            ), {duration: duration, style:{backgroundColor:backgroundColor}})
        });
    }, []);

    return <></>
}
