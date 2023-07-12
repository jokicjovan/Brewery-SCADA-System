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

export interface AnalogInput {
    id: string;
    description: string;
    driver: string;
    ioAddress: string;
    scanTime: boolean;
    lowLimit: number;
    highLimit: number;
    unit: string;
}

export interface Alarm{
    id:string;
    type:string;
    priority:number;
    edgeValue:number;
    unit:string;
    analogInput:AnalogInput;
}

export interface AlarmData {
    alarm:Alarm;
    timestamp: string;
    value: number;
}

export interface MyTags{
    analogInputs:AnalogData[];
    digitalInputs:DigitalData[];
}

