package factorymethodpatternexample;

public class ExcelDocument implements Document {
    @Override
    public void open() {
        System.out.println("Opening an Excel document (.xlsx) in the spreadsheet view.");
    }

    @Override
    public String getType() {
        return "Excel Document";
    }
}
