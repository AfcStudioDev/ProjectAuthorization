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

    public GetDistr(request: GetFilesUploadRequest) {        
        let queryString = Object.entries(request)
        .reduce((acc,e,i) => 
          `${acc}${i >0 ? '&' : '?' }${e[0]}=${encodeURIComponent(e[1])}`,
          '');
        const url = configs.AuthorisationServiceHost + "/" + configs.filesUrl + "/" + queryString;
        this.downLoadFile(url);       
    }
    private  downLoadFile(url: string) {
        let anchor = document.createElement("a");
    
        anchor.download = "";
        anchor.href = url;
        anchor.click();
    }
}