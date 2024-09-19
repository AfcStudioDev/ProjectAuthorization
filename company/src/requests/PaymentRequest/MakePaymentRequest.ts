export class MakePaymentAndConfirmRequest {
    constructor(paymentId = "", deviceNumber = "", licenseType = "") {
        this.paymentId = paymentId;
        this.deviceNumber = deviceNumber;
        this.licenseType = licenseType;
    }
    paymentId: string;
    deviceNumber: string;
    licenseType: string;
}