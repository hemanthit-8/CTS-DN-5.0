package mvcpatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 10: MVC Pattern ===\n");

        // Model: starts with initial data.
        Student student = new Student("Aarav Mehta", "S2026-101", "B+");

        // View: knows nothing about Student or StudentController.
        StudentView studentView = new StudentView();

        // Controller: wires Model and View together.
        StudentController controller = new StudentController(student, studentView);

        System.out.println("Initial record:");
        controller.updateView();

        System.out.println("\nUpdating the grade through the Controller (e.g. after a re-grade)...");
        controller.setStudentGrade("A-");

        System.out.println("\nRecord after update:");
        controller.updateView();

        System.out.println("\nNote the Model (Student) was never displayed directly, and the View");
        System.out.println("(StudentView) never read from the Model directly - the Controller");
        System.out.println("was the only thing that touched both.");
    }
}
