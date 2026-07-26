import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification';

/**
 * errorHandlerInterceptor — Hands-On 8, Task 3: global HTTP error handling.
 * 401 -> redirect to login/home. 500 -> surface a global error notification.
 * Always re-throws so the calling component can still react if it needs to.
 */
export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        router.navigate(['/']);
      } else if (error.status === 500) {
        console.error('Server error — please try again later.');
      }
      return throwError(() => error);
    })
  );
};
