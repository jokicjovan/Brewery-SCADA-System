import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import {AnalogData, DigitalData} from "../models/DataInterfaces.ts";

interface SignalRTagService {
    connection: HubConnection | null;
    startConnection: () => void;
    receiveAnalogData: (callback: (data : AnalogData) => void) => void;
    receiveDigitalData: (callback: (data : DigitalData) => void) => void;
}

const SignalRTagService: SignalRTagService = {
    connection: null,

    startConnection: () => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5041/Hub/tag", {withCredentials: true})
            .build();

        connection.start().catch((error: Error) => {
            console.error("SignalR connection error: ", error);
        });

        SignalRTagService.connection = connection;
    },

    receiveAnalogData: (callback: (data : AnalogData) => void) => {
        SignalRTagService.connection?.on("ReceiveAnalogData", (data : AnalogData) => {
            callback(data);
        });
    },

    receiveDigitalData: (callback: (data : DigitalData) => void) => {
        SignalRTagService.connection?.on("ReceiveDigitalData", (data : DigitalData) => {
            callback(data);
        });
    },
};

export default SignalRTagService;
