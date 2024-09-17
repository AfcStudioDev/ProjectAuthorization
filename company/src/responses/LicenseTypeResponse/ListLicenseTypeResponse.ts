import { LicenseTypeModel } from "../../models/LicenseTypeModel";
import { BaseResponse } from "../BaseResponse";

export type ListLicenseTypeResponse = BaseResponse & {
    licenseTypes: LicenseTypeModel[];
}