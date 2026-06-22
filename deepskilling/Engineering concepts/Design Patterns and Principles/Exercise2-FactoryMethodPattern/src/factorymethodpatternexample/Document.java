package factorymethodpatternexample;

/**
 * Common interface implemented by every document type the system can create.
 */
public interface Document {
    void open();
    String getType();
}
