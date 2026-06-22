package factorymethodpatternexample;

public class PdfDocument implements Document {
    @Override
    public void open() {
        System.out.println("Opening a PDF document (.pdf) in the PDF reader view.");
    }

    @Override
    public String getType() {
        return "PDF Document";
    }
}
