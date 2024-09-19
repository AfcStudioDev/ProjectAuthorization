import { Input, Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
import { LicenseTypeModel } from '../../models/LicenseTypeModel';
import { LicenseTypeService } from '../http/licenseType.service';
import { LicenseService } from '../http/license.service';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { PaymentService } from '../http/payment.service';
import { MakePaymentAndConfirmRequest } from '../../requests/PaymentRequest/MakePaymentRequest';
import { LicenseModel } from '../../models/LicenseModel';
declare let YooMoneyCheckoutWidget: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [LicenseTypeService, LicenseService, PaymentService]
})
export class HomeComponent {
  constructor(private router: Router, private licenseTypeService: LicenseTypeService, private licenseService: LicenseService, private paymentService: PaymentService) {

  }
  login: string = "tg";
  typeLicense: LicenseTypeModel[] = [];
  licenseList: LicenseModel[] = [];
  typeLicenseModal: LicenseTypeModel = new LicenseTypeModel;
  createPaymentRequest: CreatePaymentRequest = new CreatePaymentRequest;
  succesPay: boolean = false;

  getLogin() {
    return this.login;
  }
  OnLoginButtonClick() {
    let token:string = localStorage.getItem("token") || "";    
    if( token != "" )
    {
      localStorage.removeItem("token");
    }
    this.router.navigate(["/login"]);
  }

  ngOnInit() {
    this.getTypeLicense();
    this.getLicense();
  }

  getCountDaysEnd(startLicense: Date, duration: number) {
    return Math.round((new Date(startLicense).setHours(duration * 24) - Date.now()) / (60 * 60 * 24 * 1000));
  }

  getTypeLicense() {
    this.licenseTypeService.GetTypeLicense().subscribe({
      next: (response) => {
        console.log(response);
        this.typeLicense = response.licenseTypes;
        console.log(this.typeLicense);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getLicense() {
    this.licenseService.GetAllLicense().subscribe({
      next: (response) => {
        console.log(response);
        this.licenseList = response.licenses;
        console.log(this.licenseList);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OpenModalBay(id: string)
  {
    this.succesPay = false;

    let selectLicenseType = this.typeLicense.find(a=>a.id === id);

    if(selectLicenseType != undefined)
    {
      this.createPaymentRequest.licenseType = selectLicenseType.id;
      this.typeLicenseModal = selectLicenseType;
    }
  }

  GoPay(type: number) {

    let makePayment = new MakePaymentAndConfirmRequest;
    makePayment.deviceNumber = this.createPaymentRequest.deviceNumber;
    makePayment.licenseType = this.createPaymentRequest.licenseType;
    makePayment.paymentId = "111";

    this.paymentService.MakePayment(makePayment).subscribe({ 
      next: (response) => {
        this.succesPay = true;
      },
      error: (err) => {
        alert("Произошла ошибка при выдаче лицензии!");
      }
     });

    // var pay_info = this.paymentService.CreatePayment(this.createPaymentRequest).subscribe({
    //   next: (response) => {
    //     console.log(response);
    //     var host = window.location.host;
    //     console.log(host);

    //     var checkout = new YooMoneyCheckoutWidget({
    //       confirmation_token: response.confirmation.confirmation_token,
    //      customization: {
    //         modal: true
    //       },
    //       error_callback: (error: any) => {
    //         console.log(error);
    //       }
    //     });
        
    //     checkout.on('success', () => {
    //       alert("Оплата успешно произведена!");
    //       checkout.destroy();

    //       let makePayment = new MakePaymentAndConfirmRequest;
    //       makePayment.deviceNumber = this.createPaymentRequest.deviceNumber;
    //       makePayment.licenseType = this.createPaymentRequest.licenseType;
    //       makePayment.paymentId = response.id;

    //       this.paymentService.MakePayment(makePayment).subscribe({ 
    //         next: (response) => {
    //           this.succesPay = true;
    //         },
    //         error: (err) => {
    //           alert("Произошла ошибка при выдаче лицензии!");
    //         }
    //        });
    //     });

    //     checkout.on('fail', () => {
    //       alert("Оплата прошла неудачно, попробуйте снова");

    //       checkout.destroy();
    //     });

    //     checkout.render()
    //       .then(() => {

    //       });
    //   },
    //   error: (error)=>{
    //     alert("Произошла ошибка при создании оплаты");
    //   }
    // });
  }
}
