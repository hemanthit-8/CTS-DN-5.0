import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { LoadingService } from '../services/loading';

/**
 * loadingInterceptor — Hands-On 8, Task 3: shows a global spinner while any
 * HTTP request is in flight. `finalize` runs whether the request completes or
 * errors — equivalent to a try/catch/finally block — so the spinner always hides.
 */
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);
  loadingService.show();

  return next(req).pipe(finalize(() => loadingService.hide()));
};
