import { Directive, ElementRef, HostListener, Input } from '@angular/core';

/**
 * HighlightDirective — Hands-On 3, Task 3: adds a configurable background
 * colour to the host element on hover. @HostListener wires mouseenter/mouseleave
 * without any manual addEventListener/removeEventListener bookkeeping.
 */
@Directive({
  selector: '[appHighlight]',
})
export class HighlightDirective {
  @Input() appHighlight = 'yellow';

  constructor(private el: ElementRef<HTMLElement>) {}

  @HostListener('mouseenter')
  onMouseEnter(): void {
    this.el.nativeElement.style.backgroundColor = this.appHighlight;
  }

  @HostListener('mouseleave')
  onMouseLeave(): void {
    this.el.nativeElement.style.backgroundColor = '';
  }
}
