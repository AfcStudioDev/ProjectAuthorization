import { Input, Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
declare let YooMoneyCheckoutWidget: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  constructor(private router: Router){

  }
  login: string = "tg";

  getLogin(){
    return this.login;
  }
  OnLoginButtonClick(){
    this.router.navigate(["/login"]);
  }
  ngAfterViewInit()
  {
    alert("Телеграм: https://t.me/NeedMoreRamonaFlowers");
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
