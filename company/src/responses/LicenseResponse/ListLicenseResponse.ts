import { LicenseModel } from "../../models/LicenseModel";
import { BaseResponse } from "../BaseResponse";

export type ListLicenseResponse = BaseResponse & {
    licenses: LicenseModel[];
}