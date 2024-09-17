export class CreatePaymentRequest {
    constructor(deviceNumber = "", licenseType = "") {
        this.deviceNumber = deviceNumber;
        this.licenseType = licenseType;
    }
    deviceNumber: string;
    licenseType: string;
}