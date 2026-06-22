package factorymethodpatternexample;

/**
 * Declares the factory method, createDocument(), without committing to a
 * concrete Document subclass. Each subclass of DocumentFactory decides
 * which concrete Document gets created — that decision is deferred to
 * subclasses, which is the core idea of the Factory Method Pattern.
 */
public abstract class DocumentFactory {

    /** The factory method: subclasses override this to produce a specific Document type. */
    public abstract Document createDocument();

    /**
     * A typical reason to use Factory Method rather than just calling
     * `new WordDocument()` directly: shared workflow logic (here, a simple
     * "create, then open" sequence) can live in one place and work
     * uniformly across every document type, because it only depends on the
     * Document interface, not on any concrete class.
     */
    public Document createAndOpenDocument() {
        Document document = createDocument();
        System.out.println("Factory created document type: " + document.getType());
        document.open();
        return document;
    }
}
