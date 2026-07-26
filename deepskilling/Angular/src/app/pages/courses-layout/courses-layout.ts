import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

/** CoursesLayoutComponent — Hands-On 7: parent for the nested /courses routes. */
@Component({
  selector: 'app-courses-layout',
  imports: [RouterOutlet],
  templateUrl: './courses-layout.html',
  styleUrl: './courses-layout.css',
})
export class CoursesLayout {}
