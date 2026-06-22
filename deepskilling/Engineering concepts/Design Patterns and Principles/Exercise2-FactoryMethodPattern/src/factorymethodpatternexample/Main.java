package factorymethodpatternexample;

import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 2: Factory Method Pattern ===\n");

        List<DocumentFactory> factories = List.of(
                new WordDocumentFactory(),
                new PdfDocumentFactory(),
                new ExcelDocumentFactory()
        );

        for (DocumentFactory factory : factories) {
            factory.createAndOpenDocument();
            System.out.println();
        }

        System.out.println("Client code never called `new WordDocument()` etc. directly -");
        System.out.println("it only ever depended on the DocumentFactory and Document abstractions.");
    }
}
