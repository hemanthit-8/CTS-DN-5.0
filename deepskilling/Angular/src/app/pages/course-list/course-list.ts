import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CourseCard } from '../../components/course-card/course-card';
import { Course } from '../../models/course.model';
import { loadCourses } from '../../store/course/course.actions';
import { selectAllCourses, selectCoursesLoading, selectCoursesError } from '../../store/course/course.selectors';
import { enrollInCourse, unenrollFromCourse } from '../../store/enrollment/enrollment.actions';
import { selectEnrolledIds } from '../../store/enrollment/enrollment.selectors';

/**
 * CourseListComponent — Hands-On 3 (*ngIf/else, *ngFor + trackBy, *ngSwitch via
 * CourseCard), Hands-On 7 (routing + query params), Hands-On 9 (reads/writes
 * course + enrollment state via the NgRx store instead of calling the service directly).
 */
@Component({
  selector: 'app-course-list',
  imports: [CommonModule, FormsModule, CourseCard],
  templateUrl: './course-list.html',
  styleUrl: './course-list.css',
})
export class CourseList implements OnInit {
  courses$: Observable<Course[]>;
  loading$: Observable<boolean>;
  error$: Observable<string | null>;
  enrolledIds$: Observable<number[]>;

  currentEnrolledIds: number[] = [];
  searchTerm = '';

  constructor(private store: Store, private router: Router, private route: ActivatedRoute) {
    this.courses$ = this.store.select(selectAllCourses);
    this.loading$ = this.store.select(selectCoursesLoading);
    this.error$ = this.store.select(selectCoursesError);
    this.enrolledIds$ = this.store.select(selectEnrolledIds);
  }

  ngOnInit(): void {
    this.store.dispatch(loadCourses());
    this.searchTerm = this.route.snapshot.queryParamMap.get('search') ?? '';
    this.enrolledIds$.subscribe((ids) => (this.currentEnrolledIds = ids));
  }

  onSearchChange(): void {
    this.router.navigate(['courses'], {
      queryParams: { search: this.searchTerm || null },
      queryParamsHandling: 'merge',
    });
  }

  openCourse(course: Course): void {
    this.router.navigate(['courses', course.id]);
  }

  onEnroll(courseId: number, enrolledIds: number[]): void {
    if (enrolledIds.includes(courseId)) {
      this.store.dispatch(unenrollFromCourse({ courseId }));
    } else {
      this.store.dispatch(enrollInCourse({ courseId }));
    }
  }

  // trackBy avoids re-rendering every card on any array change — only changed
  // items are re-rendered by Angular's list diffing.
  trackByCourseId(index: number, course: Course): number {
    return course.id;
  }
}
