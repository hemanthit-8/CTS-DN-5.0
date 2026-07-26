import { Injectable } from '@angular/core';

/**
 * AuthService — Hands-On 7: a minimal, hardcoded auth state used to demonstrate
 * route guards (CanActivate). In a real app this would call a backend and store a token.
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn = true;

  login(): void {
    this.isLoggedIn = true;
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
