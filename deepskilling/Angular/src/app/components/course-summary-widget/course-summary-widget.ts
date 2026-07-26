import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course';

/**
 * CourseSummaryWidget — Hands-On 6, Task 1: a second component injecting the
 * same root-provided CourseService, used to demonstrate the singleton pattern
 * (adding a course elsewhere is reflected here too).
 */
@Component({
  selector: 'app-course-summary-widget',
  imports: [],
  templateUrl: './course-summary-widget.html',
  styleUrl: './course-summary-widget.css',
})
export class CourseSummaryWidget implements OnInit {
  totalCourses = 0;

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.totalCourses = this.courseService.getSeedCourses().length;
  }
}
