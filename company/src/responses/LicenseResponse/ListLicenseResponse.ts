import { DeviceModel } from "../../models/DeviceModel";
import { BaseResponse } from "../BaseResponse";

export type ListLicenseResponse = BaseResponse & {
    licenses: DeviceModel[];
}