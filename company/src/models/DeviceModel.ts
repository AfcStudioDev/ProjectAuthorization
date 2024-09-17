import { LicenseModel } from "./LicenseModel";
import { UserModel } from "./UserModel";

export class DeviceModel {
    constructor(id = "", deviceNumber = "", licenseKey = "", userId = "", user : any, licenses = []) {
        this.id = id;
        this.deviceNumber = deviceNumber;
        this.licenseKey = licenseKey;
        this.userId = userId;
        this.user = user;
        this.licenses = licenses;
    }
    id: string
    deviceNumber: string
    licenseKey: string
    userId: string
    user: UserModel
    licenses: LicenseModel[]
}