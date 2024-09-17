import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { configs } from "../config";
import { GetFilesUploadRequest } from "../../requests/Download/GetFilesUploadRequest";
import { GetFilesUploadResponse } from "../../responses/Download/GetFilesUploadResponse";
import { Observable } from "rxjs/internal/Observable";
import { HttpParameterCodec } from "@angular/common/http";

@Injectable()
export class  DownloadDistrService{
    constructor(private httpClient: HttpClient) { }

    public GetDistr(request: GetFilesUploadRequest): Observable<GetFilesUploadResponse> {        
        let queryString = encodeURIComponent( JSON.stringify(request));
        const url = configs.AuthorisationServiceHost + "/" + configs.filesUrl + "/" + queryString;
        return this.httpClient.get<GetFilesUploadResponse>(url);
    }    
}