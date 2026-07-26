# Student Course Portal — Angular (v20.0) Hands-On Exercise Book

Digital Nurture 5.0 | .NET Full Stack Engineer Track

A single Angular 20 (standalone components) application built incrementally
across all 10 hands-on exercises: project setup, data binding & lifecycle
hooks, directives & pipes, template-driven forms, reactive forms, services &
DI, routing & guards, HTTP client & interceptors, NgRx state management, and
unit testing.

## Where each hands-on lives

| Hands-On | Topic | Where to look |
|---|---|---|
| 1 | Setup & first component | `notes.txt`, `src/app/components/header`, `src/app/pages/home` |
| 2 | Binding & lifecycle hooks | `src/app/pages/home`, `src/app/components/course-card` |
| 3 | Directives & pipes | `src/app/directives/highlight.ts`, `src/app/pipes/credit-label-pipe.ts`, `src/app/components/course-card` |
| 4 | Template-driven forms | `src/app/pages/enrollment-form` |
| 5 | Reactive forms | `src/app/pages/reactive-enrollment-form` |
| 6 | Services & DI | `src/app/services/*`, `src/app/components/notification` |
| 7 | Routing, guards, lazy loading | `src/app/app.routes.ts`, `src/app/guards/*` |
| 8 | HTTP client & interceptors | `src/app/services/course.ts`, `src/app/interceptors/*` |
| 9 | NgRx state management | `src/app/store/*` |
| 10 | Unit testing | `*.spec.ts` files throughout `src/app` |

## Getting started

```bash
npm install
npm install -g json-server   # one-time, for the mock backend

# Terminal 1 — mock REST API (courses/students/enrollments)
json-server --watch db.json --port 3000

# Terminal 2 — Angular dev server
ng serve
```

Then open http://localhost:4200.

## Running unit tests

```bash
ng test                 # watch mode
ng test --code-coverage # generates a coverage/ report
```

## Building for production

```bash
ng build --configuration production
```

Output is written to `dist/student-course-portal/`.

## Notes

- This project uses Angular 20's modern standalone-component API (no
  NgModules) — `app.config.ts` registers the router, HttpClient (with
  interceptors), and the NgRx store/effects.
- Route-level code splitting is done with `loadComponent` in `app.routes.ts`
  (the standalone equivalent of the classic `loadChildren` + feature-module
  pattern) — verified in `ng build` output as separate lazy chunks per feature.
