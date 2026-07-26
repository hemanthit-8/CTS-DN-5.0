import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CourseService } from './course';
import { Course } from '../models/course.model';
import { Student } from '../models/student.model';

/**
 * EnrollmentService — Hands-On 6: demonstrates service-to-service injection
 * (this service depends on CourseService to resolve IDs to full Course objects).
 * Hands-On 8: adds a switchMap-based lookup for students enrolled in a course.
 */
@Injectable({
  providedIn: 'root',
})
export class EnrollmentService {
  private enrolledCourseIds: number[] = [];

  constructor(private courseService: CourseService) {}

  enroll(courseId: number): void {
    if (!this.enrolledCourseIds.includes(courseId)) {
      this.enrolledCourseIds.push(courseId);
    }
  }

  unenroll(courseId: number): void {
    this.enrolledCourseIds = this.enrolledCourseIds.filter((id) => id !== courseId);
  }

  isEnrolled(courseId: number): boolean {
    return this.enrolledCourseIds.includes(courseId);
  }

  getEnrolledCourses(): Course[] {
    return this.courseService
      .getSeedCourses()
      .filter((c) => this.enrolledCourseIds.includes(c.id));
  }

  getStudentsByCourse(courseId: number): Observable<Student[]> {
    return this.courseService.getCourseById(courseId).pipe(
      // switchMap cancels the previous inner Observable whenever a new courseId
      // arrives, preventing out-of-order responses for dependent HTTP calls.
      switchMap(() =>
        this.courseService.getCourses().pipe(
          switchMap(() => {
            // Placeholder: in a real backend this would hit /courses/:id/students
            const mock: Student[] = [{ id: 1, name: 'Sample Student', email: 'student@example.com' }];
            return new Observable<Student[]>((subscriber) => {
              subscriber.next(mock);
              subscriber.complete();
            });
          })
        )
      )
    );
  }

  /** Course-to-Course cross reference for the enrolled-courses selector pattern used in NgRx. */
  resolveEnrolledIds(): number[] {
    return this.enrolledCourseIds;
  }
}
