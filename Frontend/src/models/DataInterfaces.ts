export interface AnalogData {
    id: string;
    ioAddress: string;
    value: number;
    timestamp: string;
    tagId: string;
}

export interface DigitalData {
    id: string;
    ioAddress: string;
    value: number;
    timestamp: string;
    tagId: string;
}
export interface Alarm{
    id:string;
    type:string;
    priority:string;
    edgeValue:number;
    unit:string;
    analogInput:AnalogData;
}

export interface AlarmData {
    alarm:Alarm;
    timestamp: string;
}

export interface MyTags{
    analogInputs:AnalogData[];
    digitalInputs:DigitalData[];
}

