import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course';

/**
 * Home page — Hands-On 1 (static welcome content), Hands-On 2 (all four
 * binding types + lifecycle hooks), Hands-On 6 (live course count via CourseService).
 */
@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit, OnDestroy {
  portalName = 'Student Course Portal';
  isPortalActive = true;
  message = '';
  searchTerm = '';
  coursesCount = 0;

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    // Simulated fetch — real data comes from CourseService's seed list.
    this.coursesCount = this.courseService.getSeedCourses().length;
    console.log('HomeComponent initialised — courses loaded');
  }

  ngOnDestroy(): void {
    console.log('HomeComponent destroyed');
  }

  onEnrollClick(): void {
    this.message = 'Enrollment opened!';
  }

  // [property] is one-way: component -> DOM only.
  // [(ngModel)] is two-way: DOM <-> component, shorthand for
  // [ngModel]="prop" (ngModelChange)="prop = $event".
}
