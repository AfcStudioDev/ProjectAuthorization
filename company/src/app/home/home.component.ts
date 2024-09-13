import { Input, Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
import { LicenseTypeModel } from '../../models/LicenseTypeModel';
import { LicenseTypeService } from '../http/licenseType.service';
import { LicenseService } from '../http/license.service';
import { DeviceModel } from '../../models/DeviceModel';
declare let YooMoneyCheckoutWidget: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [LicenseTypeService, LicenseService]
})
export class HomeComponent {
  constructor(private router: Router, private licenseTypeService: LicenseTypeService, private licenseService: LicenseService) {

  }
  login: string = "tg";
  typeLicense: LicenseTypeModel[] = [];
  licenseList: DeviceModel[] = [];

  getLogin() {
    return this.login;
  }
  OnLoginButtonClick() {
    this.router.navigate(["/login"]);
  }

  ngOnInit() {
    this.getTypeLicense();
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

  GoPay(type: number) {
    var host = window.location.host;
    console.log(host);

    var checkout = new YooMoneyCheckoutWidget({
      confirmation_token: "response.confirmation.confirmation_token",
      customization: {
        modal: true
      },
      error_callback: (error: any) => {
        console.log(error);
      }
    });

    checkout.render()
      .then(() => {

      });

    //     checkout.on('success', () => {
    //       alert("Оплата успешно произведена!");
    //       checkout.destroy();
    //       this.licenseService.ConfirmLicenseCreate(response.id, type.toString()).subscribe({ 
    //         next: (response) => {
    //           if(!response)
    //           {
    //             alert("Произошла ошибка при выдаче лицензии!");
    //           }
    //         }
    //        });
    //       this.router.navigate(['/projects']);
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
    //     alert("У вас уже имеется лицензия!");
    //     this.router.navigate(['/profile']);
    //   }
    // });
  }

}
