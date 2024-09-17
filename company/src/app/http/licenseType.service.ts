import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { ListLicenseTypeResponse } from '../../responses/LicenseTypeResponse/ListLicenseTypeResponse';

@Injectable()
export class LicenseTypeService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public GetTypeLicense(): Observable<ListLicenseTypeResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.licenseTypeUrl;
        console.log("sdfsdfsdfsdfsdf");
        return this.httpClient.get<ListLicenseTypeResponse>(url);
    }
}