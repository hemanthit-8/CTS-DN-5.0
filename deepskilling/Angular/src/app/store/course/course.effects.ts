import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { CourseService } from '../../services/course';
import { loadCourses, loadCoursesFailure, loadCoursesSuccess } from './course.actions';

/**
 * CourseEffects — Hands-On 9, Task 2: the only place side effects (HTTP calls)
 * happen in the NgRx flow; reducers stay pure. Flow: loadCourses dispatched ->
 * effect calls CourseService -> loadCoursesSuccess/Failure dispatched -> reducer
 * updates state -> selectors emit -> component re-renders.
 */
@Injectable()
export class CourseEffects {
  loadCourses$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadCourses),
      switchMap(() =>
        this.courseService.getCourses().pipe(
          map((courses) => loadCoursesSuccess({ courses })),
          catchError((error) => of(loadCoursesFailure({ error: error.message })))
        )
      )
    )
  );

  constructor(private actions$: Actions, private courseService: CourseService) {}
}
