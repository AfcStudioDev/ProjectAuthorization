import { BaseResponse } from "../BaseResponse";

export type GetFilesUploadResponse = BaseResponse & {
    File: Blob;
}