import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LicenseTypeModel } from '../../models/LicenseTypeModel';
import { LicenseTypeService } from '../http/licenseType.service';
import { LicenseService } from '../http/license.service';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { PaymentService } from '../http/payment.service';
import { MakePaymentAndConfirmRequest } from '../../requests/PaymentRequest/MakePaymentRequest';
import { LicenseModel } from '../../models/LicenseModel';
import { DownloadModule } from '../shared/download.module';
import { DownloadDistrService } from '../http/downloadDistr.service';
declare let YooMoneyCheckoutWidget: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [LicenseTypeService, LicenseService, PaymentService, DownloadDistrService, DownloadModule]
})
export class HomeComponent {
  constructor(
    private router: Router,
    private licenseTypeService: LicenseTypeService,
    private licenseService: LicenseService,
    private paymentService: PaymentService,
    protected download: DownloadModule) { }

  login: string = "tg";
  typeLicense: LicenseTypeModel[] = [];
  licenseList: LicenseModel[] = [
    {
      "id": "aaa",
      "startLicense": new Date(),
      "deviceNumber": "DEVICE NUM",
      "licenseKey": "LICENSE KEY",
      "duration": 50000,
      "userId": "USER ID",
    },
  ];
  typeLicenseModal: LicenseTypeModel = new LicenseTypeModel;
  createPaymentRequest: CreatePaymentRequest = new CreatePaymentRequest;
  succesPay: boolean = false;

  getLogin() {
    return this.login;
  }
  OnLoginButtonClick() {
    let token: string = localStorage.getItem("token") || "";
    if (token != "") {
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
        this.typeLicense = response.licenseTypes.sort((a,b)=>a.duration - b.duration);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getLicense() {
    this.licenseService.GetAllLicense().subscribe({
      next: (response) => {
        this.licenseList = response.licenses;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OpenModalBay(id: string) {
    this.succesPay = false;

    let selectLicenseType = this.typeLicense.find(a => a.id === id);

    if (selectLicenseType != undefined) {
      this.createPaymentRequest.licenseType = selectLicenseType.id;
      this.typeLicenseModal = selectLicenseType;
    }
  }

  OnCopyDeviceNumberClick(deviceNum: string) {
    navigator.clipboard.writeText(deviceNum);
  }

  GoPay(type: number) {
    var pay_info = this.paymentService.CreatePayment(this.createPaymentRequest).subscribe({
      next: (response) => {
        var checkout = new YooMoneyCheckoutWidget({
          confirmation_token: response.pay.confirmation.confirmation_token,
          customization: {
            modal: true
          },
          error_callback: (error: any) => {
            console.log(error);
          }
        });

        checkout.on('success', () => {
          checkout.destroy();
          let makePayment = new MakePaymentAndConfirmRequest;
          makePayment.deviceNumber = this.createPaymentRequest.deviceNumber;
          makePayment.licenseType = this.createPaymentRequest.licenseType;
          makePayment.paymentId = response.pay.id;

          this.paymentService.MakePayment(makePayment).subscribe({
            next: (response) => {
              this.succesPay = true;
            },
            error: (err) => {
              alert("Произошла ошибка при выдаче лицензии!");
            }
          });
        });

        checkout.on('fail', () => {
          alert("Оплата прошла неудачно, попробуйте снова");

          checkout.destroy();
        });

        checkout.render()
          .then(() => {

          });
      },
      error: (error) => {
        alert("Произошла ошибка при создании оплаты");
      }
    });
  }
}
