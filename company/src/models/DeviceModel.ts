export class DeviceModel {
    constructor(id: number, expirationLicense = new Date, deviceNumber = "", userId: number) {
        this.id = id;
        this.expirationLicense = expirationLicense;
        this.deviceNumber = deviceNumber;
        this.userId = userId;
    }
    id: number
    expirationLicense: Date
    deviceNumber: string
    userId: number
}