import { HttpInterceptorFn } from '@angular/common/http';

/**
 * authInterceptor — Hands-On 8, Task 3: clones every outgoing request and adds
 * a mock Authorization header. Interceptors run in registration order on the
 * way out, and in reverse order on the way back.
 */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authReq = req.clone({
    setHeaders: { Authorization: 'Bearer mock-token-12345' },
  });
  return next(authReq);
};
