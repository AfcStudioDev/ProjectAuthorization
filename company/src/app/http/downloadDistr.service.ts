import { Injectable } from "@angular/core";
import { configs } from "../config";
import { GetFilesUploadRequest } from "../../requests/Download/GetFilesUploadRequest";

@Injectable()
export class  DownloadDistrService{
    constructor() { }

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