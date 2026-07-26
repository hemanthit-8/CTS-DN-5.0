import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth';

/**
 * authGuard — Hands-On 7: functional CanActivate guard (modern Angular style).
 * Protects /profile and /enroll. Redirects unauthenticated users to home.
 */
export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (auth.isLoggedIn) {
    return true;
  }

  router.navigate(['/']);
  return false;
};
