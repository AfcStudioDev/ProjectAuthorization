export class CreatePaymentRequest {
    constructor(deviceNumber = "", licenseType: number = 0) {
        this.deviceNumber = deviceNumber;
        this.licenseType = licenseType;
    }
    deviceNumber: string;
    licenseType: number;
}