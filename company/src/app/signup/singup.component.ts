import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { FormsModule, Validators } from '@angular/forms';
import { RegistrationRequest } from '../../requests/AuthorizationRequest/RegistrationRequest';
import { AuthorizationService } from '../http/authorization.service';
import { GetFilesUploadRequest } from '../../requests/Download/GetFilesUploadRequest';
import { DownloadDistrService } from '../http/downloadDistr.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
  providers: [AuthorizationService, DownloadDistrService]
})
export class SignUpComponent {
  constructor(private router: Router, private authorizationService: AuthorizationService, private downloadDistrService : DownloadDistrService) { 
    this.CheckAuthToken();
  }
  emailRegex: RegExp = new RegExp("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
  passwordRegex: RegExp = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$");  
  passwordRepeated: string = "";
  prompt: string = "";
  registration: RegistrationRequest = new RegistrationRequest;

  OnSignUpButtonClick() {
    this.prompt = "";
    if (!this.CheckLogin()) {
      this.prompt = "Некорректная почта";
    }
    else if (!this.CheckPassword()) {
      this.prompt = "Недопустимый пароль";
    }
    else if (!this.CheckPasswordsMatch()) {
      this.prompt = "Пароли не совпадают";
    } else {      
      this.authorizationService.Registration(this.registration).subscribe({
        next: (response) => {
          this.router.navigate(["/login"]);
        },
        error: (err) => {
          alert("Не удалось создать аккаунт: " + err.error);
          console.log(err);
        }
      });
    }
  }

  CheckLogin(): boolean {
    if(this.registration.email.length > 30) {
      alert("Почта не должна быть больше 30 символов")
      return false;
    }
    if(!this.emailRegex.test(this.registration.email))
      {
        alert("Имя (до @) ящика может состоять только из букв английского алфавита,\n"
             +"цифр и следующих знаков: [._%+-] \n"
             +"Имя домена (после @) до точки должно содержать только цифры и буквы английского алфавита\n"
             +"Имя домена после точки должно содержать только буквы и не меньше 2");
        return false;
      };
    return true;
  }
  CheckPassword(): boolean {
    if(this.registration.password.length < 8)
    {
      alert("Пароль должен быть не короче 8 символов")
      return false;
    }
    if(!this.passwordRegex.test(this.registration.password))
    {
      alert("Пароль должен содержать не менее 8 символов и включать как минимум:\n"
           +"1 цифру, 1 прописную и 1 строчную букву,\n"
           +"1 спец символ из списка: [@$!%*#?&]")
      return false;
    }
    return true;
  }
  CheckPasswordsMatch(): boolean {
    return this.registration.password == this.passwordRepeated;
  }

  OnLoginButtonClick() {
    this.router.navigate(["/login"]);
  }
  
  CheckAuthToken(){
    let token:string = localStorage.getItem("token") || "";
    if( token != "" )
    {
      this.router.navigate(['/home']);
    }
  }

  OnDownLoadClick(platformType: number){
    let request = new GetFilesUploadRequest(platformType);
    this.downloadDistrService.GetDistr(request);    
  }
}
