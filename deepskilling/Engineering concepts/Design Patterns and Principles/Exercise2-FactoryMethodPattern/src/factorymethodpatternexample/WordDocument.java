package factorymethodpatternexample;

public class WordDocument implements Document {
    @Override
    public void open() {
        System.out.println("Opening a Word document (.docx) in the word processor view.");
    }

    @Override
    public String getType() {
        return "Word Document";
    }
}
