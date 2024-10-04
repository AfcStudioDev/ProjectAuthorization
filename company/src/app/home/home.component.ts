import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { LicenseTypeModel } from '../../models/LicenseTypeModel';
import { LicenseTypeService } from '../http/licenseType.service';
import { LicenseService } from '../http/license.service';
import { CreatePaymentRequest } from '../../requests/PaymentRequest/CreatePaymentRequest';
import { PaymentService } from '../http/payment.service';
import { MakePaymentAndConfirmRequest } from '../../requests/PaymentRequest/MakePaymentRequest';
import { DeviceModel } from '../../models/DeviceModel';
import { DownloadDistrService } from '../http/downloadDistr.service';
import { ContactsLicenseComponent } from '../contactsLicense/contactsLicense.component';
import { DownloadModuleComponent } from '../shared/download.module.component';
declare let YooMoneyCheckoutWidget: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, ContactsLicenseComponent, DownloadModuleComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [LicenseTypeService, LicenseService, PaymentService, DownloadDistrService]
})
export class HomeComponent {
  constructor(
    private router: Router,
    private licenseTypeService: LicenseTypeService,
    private licenseService: LicenseService,
    private paymentService: PaymentService) { }

  login: string = "tg";
  typeLicenses: LicenseTypeModel[] = [];
  licenseList: DeviceModel[] = [];
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

  getCountDaysEnd(expirationLicense: Date) {

    return Math.round((new Date(expirationLicense).setHours(0) - Date.now()) / (60 * 60 * 24 * 1000));
  }

  getTypeLicense() {
    this.licenseTypeService.GetTypeLicense().subscribe({
      next: (response) => {
        this.typeLicenses = response.licenseTypes.sort((a, b) => a.duration - b.duration);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getLicense() {
    this.licenseService.GetAllLicense().subscribe({
      next: (response) => {
        this.licenseList = response.devices;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OpenModalBay(id: number) {
    this.succesPay = false;

    let selectLicenseType = this.typeLicenses.find(a => a.id === id);

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
