import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { ListLicenseResponse } from '../../responses/LicenseResponse/ListLicenseResponse';
import { CreateLicenseRequest } from '../../requests/LicenseRequest/CreateLicenseRequest';
import { BaseResponse } from '../../responses/BaseResponse';
import { DeleteLicenseRequest } from '../../requests/LicenseRequest/DeleteLicenseRequest';

@Injectable()
export class LicenseService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public GetAllLicense(): Observable<ListLicenseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.licenseUrl + "/" + configs.licenseAllUrl;
        const header = new HttpHeaders({'Authorization': `Bearer ${this.token}`, 'X-Require-Auth': 'true'});
        const options = {headers : header}

        return this.httpClient.get<ListLicenseResponse>(url, options);
    }

    public CreateLicense(request: CreateLicenseRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.licenseUrl + "/" + configs.licenseCreateUrl;

        return this.httpClient.post<BaseResponse>(url, request);
    }

    public DeleteLicense(request: DeleteLicenseRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.licenseUrl + "/" + configs.licenseDeleteUrl;

        return this.httpClient.post<BaseResponse>(url, request);
    }
}