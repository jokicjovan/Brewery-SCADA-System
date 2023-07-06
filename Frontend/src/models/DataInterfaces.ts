export interface AnalogData {
    id: string;
    address: string;
    value: number;
    timestamp: string;
    tagId: string;
}

export interface DigitalData {
    id: string;
    address: string;
    value: number;
    timestamp: string;
    tagId: string;
}

export interface AlarmData {
    id: string;
    alarmId: string;
    timestamp: string;
}