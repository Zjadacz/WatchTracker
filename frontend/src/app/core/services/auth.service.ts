import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login(email: string, password: string) {
    return this.http
      .post<any>(`${environment.apiUrl}/auth/login`, {
        email,
        password,
      })
      .pipe(
        tap((response) => {
          localStorage.setItem('token', response.token);
        }),
      );
  }

  register(email: string, password: string) {
    return this.http
      .post<any>(`${environment.apiUrl}/auth/register`, {
        email,
        password,
      });
  }

  confirmEmail(userId: string, token: string) {
    return this.http.get(`${environment.apiUrl}/auth/confirm-email`, {
      params: { userId, token},
      responseType: 'text'      
    });
  }

  logout() {
    localStorage.removeItem('token');
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;

    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp;

    return Date.now() < exp * 1000;
  }
}
