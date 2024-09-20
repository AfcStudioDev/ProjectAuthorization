import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../responses/BaseResponse';
import { LoginRequest } from '../../requests/AuthorizationRequest/LoginRequest';
import { RegistrationRequest } from '../../requests/AuthorizationRequest/RegistrationRequest';
import { LoginResponse } from '../../responses/AuthorizationResponse/loginResponse';

@Injectable()
export class AuthorizationService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public Login(request: LoginRequest): Observable<LoginResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.authorizeUrl + "/" + configs.loginUrl;

        return this.httpClient.post<LoginResponse>(url, request);
    }

    public Registration(request: RegistrationRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.authorizeUrl + "/" + configs.registrationUrl;

        return this.httpClient.post<BaseResponse>(url, request);
    }

    public Verification(): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.authorizeUrl + "/" + configs.verificationUrl;

        return this.httpClient.get<BaseResponse>(url);
    }
}