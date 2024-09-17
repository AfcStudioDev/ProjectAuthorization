import { DeviceModel } from "../../models/DeviceModel";
import { BaseResponse } from "../BaseResponse";

export type GetFilesUploadResponse = BaseResponse & {
    File: File;
}