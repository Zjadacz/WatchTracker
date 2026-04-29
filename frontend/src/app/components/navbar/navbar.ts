import { Component } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { AuthService } from '@app/core/services/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
  constructor(
    public authService: AuthService,
    public router: Router
  ) {}

  isOpen = false;

  toggleMenu() {
    this.isOpen = !this.isOpen;
  }

  logout() {
    this.authService.logout();
  }

  showLogin(): boolean {
    return !this.authService.isLoggedIn() && this.router.url !== '/user/login';
  }

  showRegister(): boolean {
    return !this.authService.isLoggedIn() && this.router.url !== '/user/register';
  }
}
