import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { HttpEvent, HttpHandler, HttpHandlerFn, HttpRequest, provideHttpClient, withInterceptors } from '@angular/common/http'
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { SignUpComponent } from './signup/singup.component';
import { Observable } from 'rxjs/internal/Observable';

const appRoutes: Routes =[
  { path: "", component: AppComponent},
  { path: "home", component: HomeComponent},
  { path: "signup", component: SignUpComponent }
];

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideClientHydration(),    provideHttpClient(withInterceptors([AuthInterceptor]))]
};

export function  AuthInterceptor(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
  const token = localStorage.getItem('token') 
  if (!token) {
    return next(req);
  }
  const req1 = req.clone({
    headers: req.headers.set('Authorization', `Bearer ${token}`),
  });

  return next(req1);
}

