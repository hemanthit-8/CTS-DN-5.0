import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry, tap } from 'rxjs/operators';
import { Course } from '../models/course.model';

/**
 * CourseService — Hands-On 6: originally an in-memory data store using
 * providedIn: 'root' (singleton, shared across the whole app).
 * Hands-On 8: refactored to talk to a real backend (JSON Server) via HttpClient,
 * with RxJS operators for transformation, logging and error handling.
 */
@Injectable({
  providedIn: 'root',
})
export class CourseService {
  private readonly apiUrl = 'http://localhost:3000/courses';

  // Fallback in-memory seed data, used only if json-server isn't running.
  private courses: Course[] = [
    { id: 1, name: 'Data Structures', code: 'CS101', credits: 4, gradeStatus: 'passed' },
    { id: 2, name: 'Operating Systems', code: 'CS102', credits: 3, gradeStatus: 'pending' },
    { id: 3, name: 'Database Systems', code: 'CS103', credits: 3, gradeStatus: 'passed' },
    { id: 4, name: 'Computer Networks', code: 'CS104', credits: 2, gradeStatus: 'failed' },
    { id: 5, name: 'Web Technologies', code: 'CS105', credits: 4, gradeStatus: 'pending' },
  ];

  constructor(private http: HttpClient) {}

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl).pipe(
      // tap is for side effects (logging) — it must never mutate the stream's data.
      tap((courses) => console.log('Courses loaded:', courses.length)),
      map((courses) => courses.filter((c) => c.credits > 0)),
      retry(2),
      catchError((err) => {
        console.error(err);
        return throwError(() => new Error('Failed to load courses. Please try again.'));
      })
    );
  }

  getCourseById(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`).pipe(
      catchError((err) => {
        console.error(err);
        return throwError(() => new Error('Failed to load course.'));
      })
    );
  }

  createCourse(course: Omit<Course, 'id'>): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, course);
  }

  updateCourse(id: number, course: Partial<Course>): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, course);
  }

  deleteCourse(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  /** Synchronous fallback accessor used by components before HTTP data has loaded. */
  getSeedCourses(): Course[] {
    return this.courses;
  }

  addSeedCourse(course: Course): void {
    this.courses.push(course);
  }
}
