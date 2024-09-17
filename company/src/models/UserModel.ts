import { DeviceModel } from "./DeviceModel";

export class UserModel {
    constructor(id = "", email = "", devices = []) {
        this.id = id;
        this.email = email;
        this.devices = devices;
    }
    id: string
    email: string
    devices: DeviceModel[]
}