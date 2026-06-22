# Exercise 10: MVC Pattern

## Scenario
A simple web application for managing student records needs a clean separation between the data, how it's displayed, and the logic that ties the two together.

## What problem the MVC Pattern solves
Without a clear separation, it's easy to end up with classes that mix data storage, business logic, and display formatting all in one place - a `Student` class with a `print()` method baked in, for instance, can't easily be displayed a different way (HTML page vs. console vs. mobile app) without modifying the `Student` class itself, and can't be unit tested without also exercising the display logic. MVC (Model-View-Controller) separates these concerns into three collaborating roles, each with a single responsibility, so each can change independently.

## Implementation
- `Student.java` (**Model**) - holds the data only (`name`, `id`, `grade`) with plain getters/setters. It has no display logic and no idea a `StudentView` or `StudentController` exists.
- `StudentView.java` (**View**) - `displayStudentDetails(name, id, grade)` only knows how to format and print data it's handed; it never reads from the `Student` object directly.
- `StudentController.java` (**Controller**) - holds references to both the `Student` (model) and `StudentView` (view); exposes methods to update the model (`setStudentGrade`, etc.) and to push current model data to the view (`updateView`). The Model and View never reference each other directly - the Controller is the only class that touches both.
- `Main.java` - creates a `Student`, displays it via the controller, updates the grade through the controller, and displays it again, showing the update flowing from Controller to Model and the refreshed data flowing from Controller to View.

Compile and run:
```bash
cd Exercise10-MVCPattern
javac -d out src/mvcpatternexample/*.java
java -cp out mvcpatternexample.Main
```

## How responsibility is divided, and why it matters
- **Model**: owns the data and the rules about what valid data looks like. It's reusable across completely different interfaces - the exact same `Student` class could back a console app, a web app, or a mobile app.
- **View**: owns presentation only. A second `StudentHtmlView` or `StudentJsonView` could be added without touching `Student` or `StudentController` at all, since the View's job is narrowly "given this data, display it."
- **Controller**: owns the coordination logic - interpreting requests (here, simple method calls; in a real web app, HTTP requests), updating the Model accordingly, and choosing what the View should display. Keeping this thin (the Controller above does no formatting and no data validation) makes it easy to reason about.

This separation makes each piece independently testable: the Model's logic can be unit tested with no View involved at all, and a View can be tested by handing it sample data directly, with no real Model needed.

## MVC in the broader ecosystem
This exercise implements MVC by hand to show the underlying mechanics, but in practice, most web frameworks (Spring MVC for Java, ASP.NET MVC, Ruby on Rails, Django) provide MVC scaffolding out of the box - routing HTTP requests to controller methods, rendering templates as views, and often pairing the model with an ORM for database persistence. Understanding the plain version here is what makes those frameworks' conventions make sense rather than feel like arbitrary structure.
