export class MakePaymentAndConfirmRequest {
    constructor(paymentId = "", deviceNumber = "", licenseType: number = 0) {
        this.paymentId = paymentId;
        this.deviceNumber = deviceNumber;
        this.licenseType = licenseType;
    }
    paymentId: string;
    deviceNumber: string;
    licenseType: number;
}