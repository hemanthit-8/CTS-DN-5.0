import { Pipe, PipeTransform } from '@angular/core';

/**
 * CreditLabelPipe — Hands-On 3, Task 3: transforms a numeric credit count into
 * a human-readable label. Pure by default — Angular only re-runs it when the
 * input reference changes, which keeps it cheap.
 */
@Pipe({
  name: 'creditLabel',
})
export class CreditLabelPipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    if (!value || value <= 0) {
      return 'No Credits';
    }
    if (value === 1) {
      return '1 Credit';
    }
    return `${value} Credits`;
  }
}
