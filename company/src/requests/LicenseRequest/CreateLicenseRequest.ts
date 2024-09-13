export class CreateLicenseRequest {
    constructor(userId = "", deviceNumber = "", licenseType = "") {
        this.userId = userId;
        this.deviceNumber = deviceNumber;
        this.licenseType = licenseType;
    }
    userId: string
    deviceNumber: string
    licenseType: string
}