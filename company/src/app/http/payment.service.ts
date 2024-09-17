import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { configs } from '../config';
import { Observable } from 'rxjs';
import { BaseResponse } from '../../responses/BaseResponse';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { MakePaymentRequest } from '../../requests/PaymentRequest/MakePaymentRequest';

@Injectable()
export class PaymentService {
    constructor(private httpClient: HttpClient) { }

    token = localStorage.getItem('token');

    public CreatePayment(request: CreatePaymentRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.paymentUrl + "/" + configs.createPaymentUrl;

        return this.httpClient.post<BaseResponse>(url, request);
    }

    public MakePayment(request: MakePaymentRequest): Observable<BaseResponse> {
        const url = configs.AuthorisationServiceHost + "/" + configs.paymentUrl + "/" + configs.makePaymentUrl;

        return this.httpClient.post<BaseResponse>(url, request);
    }
}