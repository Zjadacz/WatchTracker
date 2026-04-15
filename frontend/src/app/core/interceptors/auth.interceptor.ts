import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');
  const router = inject(Router);
  // If token exists, clone the request and add the Authorization header
  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req).pipe(
    // Global errors handling
    catchError((error) => {
      if (error.status === 401) {
        // Token invalid / not authorized
        localStorage.removeItem('token');

        // przekierowanie na login
        router.navigate(['/user/login']);
      }

      throw error;
    })
  );
};