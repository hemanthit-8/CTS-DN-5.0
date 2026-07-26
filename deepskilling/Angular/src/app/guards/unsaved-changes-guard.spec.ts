import { unsavedChangesGuard, CanComponentDeactivate } from './unsaved-changes-guard';

describe('unsavedChangesGuard', () => {
  it('should be created', () => {
    expect(unsavedChangesGuard).toBeTruthy();
  });

  it('should allow navigation when there are no unsaved changes', () => {
    const component: CanComponentDeactivate = { hasUnsavedChanges: () => false };
    const result = (unsavedChangesGuard as any)(component);
    expect(result).toBeTrue();
  });

  it('should prompt and respect the user choice when there are unsaved changes', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    const component: CanComponentDeactivate = { hasUnsavedChanges: () => true };
    const result = (unsavedChangesGuard as any)(component);
    expect(window.confirm).toHaveBeenCalled();
    expect(result).toBeTrue();
  });
});
