import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignUpComponent } from './signup/singup.component';
import { LoginComponent } from './login/login.component';
import { LicenseComponent } from './license/license.component';
import { UserManagedComponent } from './admin/user/userManaged.component';
import { homeEnter1Guard } from './guards/home-enter1.guard';
import { homeEnter2Guard } from './guards/home-enter2.guard';

export const routes: Routes = [
  {
  path:'', redirectTo: '/signup', pathMatch: 'full'
  },
  {
    path:'login', component: LoginComponent,canActivate: [homeEnter2Guard],
  },
  {
    path:'signup', component: SignUpComponent,canActivate: [homeEnter2Guard],
  },
  {
    path:'home', component: HomeComponent,canActivate: [homeEnter1Guard],
  },
  {
    path:'license', component: LicenseComponent,canActivate: [homeEnter1Guard],
  },
  {
    path:'admin/userManaged', component: UserManagedComponent
  }
];