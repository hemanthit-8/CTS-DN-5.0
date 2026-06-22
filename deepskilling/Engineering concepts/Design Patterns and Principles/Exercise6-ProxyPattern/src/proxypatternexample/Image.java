package proxypatternexample;

/**
 * Subject interface: anything that can be displayed as an image, whether
 * it's the real, expensive-to-load image or a lightweight stand-in for it.
 */
public interface Image {
    void display();
}
