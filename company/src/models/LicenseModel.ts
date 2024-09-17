import { LicenseTypeModel } from "./LicenseTypeModel";

export class LicenseModel {
    constructor(id = "", startLicense = "", deviceId = "", licenseTypeId = "", licenseType : any) {
        this.id = id;
        this.startLicense = startLicense;
        this.deviceId = deviceId;
        this.licenseTypeId = licenseTypeId;
        this.licenseType = licenseType;
    }
    id: string
    startLicense: string
    deviceId: string
    licenseTypeId: string
    licenseType: LicenseTypeModel
}