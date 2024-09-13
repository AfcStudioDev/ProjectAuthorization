import { BaseResponse } from "../BaseResponse";

export type LoginResponse = BaseResponse & {
    token: string;
}