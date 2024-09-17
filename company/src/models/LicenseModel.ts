export class LicenseModel {
    constructor(id = "", startLicense = "", deviceNumber = "", licenseKey = "", duration : 0, userId: "") {
        this.id = id;
        this.startLicense = startLicense;
        this.deviceNumber = deviceNumber;
        this.licenseKey = licenseKey;
        this.duration = duration;
        this.userId = userId;
    }
    id: string
    startLicense: string
    deviceNumber: string
    licenseKey: string
    duration: number
    userId: string
}