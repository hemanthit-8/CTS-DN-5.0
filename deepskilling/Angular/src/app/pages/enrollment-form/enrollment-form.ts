import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { CourseService } from '../../services/course';

/**
 * EnrollmentFormComponent — Hands-On 4: template-driven form. Form structure
 * and validation rules live in the HTML template via ngModel + validator attributes.
 */
@Component({
  selector: 'app-enrollment-form',
  imports: [CommonModule, FormsModule],
  templateUrl: './enrollment-form.html',
  styleUrl: './enrollment-form.css',
})
export class EnrollmentForm {
  studentName = '';
  studentEmail = '';
  courseId: number | null = null;
  preferredSemester = 'Odd';
  agreeToTerms = false;
  submitted = false;

  constructor(private courseService: CourseService) {}

  onSubmit(form: NgForm): void {
    console.log(form.value, form.valid);
    if (form.valid) {
      // Hands-On 8, step 81: wire the enrollment form's submit handler to the
      // service's POST method instead of only logging locally.
      this.courseService
        .createCourse({
          name: `Enrollment request for course ${this.courseId}`,
          code: `REQ-${this.courseId}`,
          credits: 0,
          gradeStatus: 'pending',
        })
        .subscribe({
          next: () => (this.submitted = true),
          error: (err) => console.error('Failed to submit enrollment request', err),
        });
    }
  }
}
