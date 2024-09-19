import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../responses/BaseResponse';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { MakePaymentAndConfirmRequest } from '../../requests/PaymentRequest/MakePaymentRequest';

@Injectable()
export class PaymentService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public CreatePayment(request: CreatePaymentRequest): Observable<any> {
        const url = configs.AuthorisationServiceHost + "/" + configs.paymentUrl + "/" + configs.createPaymentUrl;
        const header = new HttpHeaders({'Authorization': `Bearer ${this.token}`, 'X-Require-Auth': 'true'});
        const options = {headers : header}

        return this.httpClient.post<any>(url, request, options);
    }

    public MakePayment(request: MakePaymentAndConfirmRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.paymentUrl + "/" + configs.makePaymentUrl;
        const header = new HttpHeaders({'Authorization': `Bearer ${this.token}`, 'X-Require-Auth': 'true'});
        const options = {headers : header}

        return this.httpClient.post<BaseResponse>(url, request, options);
    }
}