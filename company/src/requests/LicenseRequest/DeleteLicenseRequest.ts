export class DeleteLicenseRequest {
    constructor(deviceNumber = "") {
        this.deviceNumber = deviceNumber;
    }
    deviceNumber: string
}