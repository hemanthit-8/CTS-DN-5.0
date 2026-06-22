package mvcpatternexample;

/**
 * View: knows only how to display data it's handed - it never reads from
 * or modifies the Model directly, and has no business logic of its own.
 */
public class StudentView {
    public void displayStudentDetails(String name, String id, String grade) {
        System.out.println("---- Student Record ----");
        System.out.println("  ID:    " + id);
        System.out.println("  Name:  " + name);
        System.out.println("  Grade: " + grade);
        System.out.println("-------------------------");
    }
}
