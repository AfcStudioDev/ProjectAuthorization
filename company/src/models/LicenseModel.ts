export class LicenseModel {
    constructor(id = "", startLicense = new Date, deviceNumber = "", licenseKey = "", duration : 0, userId: "") {
        this.id = id;
        this.startLicense = startLicense;
        this.deviceNumber = deviceNumber;
        this.licenseKey = licenseKey;
        this.duration = duration;
        this.userId = userId;
    }
    id: string
    startLicense: Date
    deviceNumber: string
    licenseKey: string
    duration: number
    userId: string
}