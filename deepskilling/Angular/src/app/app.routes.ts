import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';
import { unsavedChangesGuard } from './guards/unsaved-changes-guard';

/**
 * Hands-On 7: route configuration with parameters, nested routes, guards and
 * lazy loading. In modern standalone Angular, lazy loading a whole feature is
 * done with loadComponent/loadChildren pointing at a routes array — the
 * equivalent of the classic `loadChildren: () => import('./x.module').then(m => m.XModule)`
 * pattern from NgModule-based apps.
 */
export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/home/home').then((m) => m.Home) },
  {
    path: 'courses',
    loadComponent: () => import('./pages/courses-layout/courses-layout').then((m) => m.CoursesLayout),
    children: [
      { path: '', loadComponent: () => import('./pages/course-list/course-list').then((m) => m.CourseList) },
      { path: ':id', loadComponent: () => import('./pages/course-detail/course-detail').then((m) => m.CourseDetail) },
    ],
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/student-profile/student-profile').then((m) => m.StudentProfile),
  },
  {
    // Lazily-loaded "enrollment" feature area — its JS chunk only downloads
    // the first time the user navigates to /enroll or /enroll-reactive.
    path: 'enroll',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/enrollment-form/enrollment-form').then((m) => m.EnrollmentForm),
  },
  {
    path: 'enroll-reactive',
    canActivate: [authGuard],
    canDeactivate: [unsavedChangesGuard],
    loadComponent: () =>
      import('./pages/reactive-enrollment-form/reactive-enrollment-form').then((m) => m.ReactiveEnrollmentForm),
  },
  { path: '**', loadComponent: () => import('./pages/not-found/not-found').then((m) => m.NotFound) },
];
