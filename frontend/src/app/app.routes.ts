import { Routes } from '@angular/router';
import { authGuard } from '@app/core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    title: 'Home Page',
    // canActivate: [authGuard],
    loadComponent: () => import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'user',
    children: [
      {
        path: 'login',
        title: 'Login Page',
        loadComponent: () =>
          import('./features/auth/login/login.component').then((m) => m.LoginComponent),
      },
      {
        path: 'register',
        title: 'Register Page',
        loadComponent: () =>
          import('./features/auth/register/register.component').then((m) => m.RegisterComponent),
      },
      {
        path: 'email-sent',
        title: 'Email Sent Page',
        loadComponent: () =>
          import('./features/auth/email-sent/email-sent.component').then(
            (m) => m.EmailSentComponent,
          ),
      },
      {
        path: 'confirm',
        title: 'Confirm Email Page',
        loadComponent: () =>
          import('./features/auth/confirm/confirm.component').then((m) => m.ConfirmComponent),
      },
    ],
  },
];
