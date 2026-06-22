package proxypatternexample;

/**
 * Proxy: implements the same Image interface as RealImage, so callers can't
 * tell the difference, but defers creating the expensive RealImage until
 * display() is actually called for the first time (lazy initialization),
 * and then reuses that same RealImage for every subsequent display() call
 * (caching) instead of reloading from the remote server each time.
 */
public class ProxyImage implements Image {
    private final String fileName;
    private RealImage realImage; // null until first use

    public ProxyImage(String fileName) {
        this.fileName = fileName;
        // Note: no loading happens here - just constructing a ProxyImage is cheap.
        System.out.println("  [ProxyImage] Created proxy for \"" + fileName + "\" (no load yet).");
    }

    @Override
    public void display() {
        if (realImage == null) {
            System.out.println("  [ProxyImage] No cached image yet - loading now.");
            realImage = new RealImage(fileName);
        } else {
            System.out.println("  [ProxyImage] Using cached image - no reload needed.");
        }
        realImage.display();
    }
}
