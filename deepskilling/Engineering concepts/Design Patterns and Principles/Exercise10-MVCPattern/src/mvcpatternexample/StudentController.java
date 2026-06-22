package mvcpatternexample;

/**
 * Controller: mediates between Model and View. It reads/updates the Model
 * in response to actions, and pulls data out of the Model to hand to the
 * View for display - the Model and View never talk to each other directly.
 */
public class StudentController {
    private Student student;
    private final StudentView studentView;

    public StudentController(Student student, StudentView studentView) {
        this.student = student;
        this.studentView = studentView;
    }

    public void setStudentName(String name) {
        student.setName(name);
    }

    public void setStudentId(String id) {
        student.setId(id);
    }

    public void setStudentGrade(String grade) {
        student.setGrade(grade);
    }

    public void updateView() {
        studentView.displayStudentDetails(student.getName(), student.getId(), student.getGrade());
    }
}
