import { Input, Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
import { LicenseTypeModel } from '../../models/LicenseTypeModel';
import { LicenseTypeService } from '../http/licenseType.service';
import { LicenseService } from '../http/license.service';
import { DeviceModel } from '../../models/DeviceModel';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { PaymentService } from '../http/payment.service';
import { MakePaymentRequest } from '../../requests/PaymentRequest/MakePaymentRequest';
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
  licenseList: DeviceModel[] = [];
  typeLicenseModal: LicenseTypeModel = new LicenseTypeModel;
  createPaymentRequest: CreatePaymentRequest = new CreatePaymentRequest;

  getLogin() {
    return this.login;
  }
  OnLoginButtonClick() {
    this.router.navigate(["/login"]);
  }

  ngOnInit() {
    this.getTypeLicense();
    this.getLicense();
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
    let selectLicenseType = this.typeLicense.find(a=>a.id === id);

    if(selectLicenseType != undefined)
    {
      this.createPaymentRequest.licenseType = selectLicenseType.id;
      this.typeLicenseModal = selectLicenseType;
    }
  }

  GoPay(type: number) {
    var pay_info = this.paymentService.CreatePayment(this.createPaymentRequest).subscribe({
      next: (response) => {
        console.log(response);
        var host = window.location.host;
        console.log(host);

        var checkout = new YooMoneyCheckoutWidget({
          confirmation_token: response.confirmation.confirmation_token,
         customization: {
            modal: true
          },
          error_callback: (error: any) => {
            console.log(error);
          }
        });
        
        checkout.on('success', () => {
          alert("Оплата успешно произведена!");
          checkout.destroy();

          let makePayment = new MakePaymentRequest;
          makePayment.deviceNumber = this.createPaymentRequest.deviceNumber;
          makePayment.licenseType = this.createPaymentRequest.licenseType;
          makePayment.paymentId = response.id;

          this.paymentService.MakePayment(makePayment).subscribe({ 
            next: (response) => {
              if(!response)
              {
                alert("Произошла ошибка при выдаче лицензии!");
              }
            }
           });
          this.getLicense();
        });

        checkout.on('fail', () => {
          alert("Оплата прошла неудачно, попробуйте снова");

          checkout.destroy();
        });

        checkout.render()
          .then(() => {

          });
      },
      error: (error)=>{
        alert("Произошла ошибка при создании оплаты");
      }
    });
  }

}
