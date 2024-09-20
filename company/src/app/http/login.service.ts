import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { ListLoginResponse } from '../responses/AuthorizationResponse/ListLoginResponse';
// import { CreateLoginRequest } from '../../requests/LoginRequest/CreateLoginRequest';
import { BaseResponse } from '../../responses/BaseResponse';
// import { DeleteLoginRequest } from '../../requests/LoginRequest/DeleteLoginRequest';

@Injectable()
export class LoginService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public GetAllEmails(): Observable<ListLoginResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.authorizeUrl + "/" + configs.loginAllUrl;

        return this.httpClient.get<ListLoginResponse>(url);
    }
}