package proxypatternexample;

/**
 * Real subject: represents an actual image loaded from a (simulated) remote
 * server. Loading happens once, in the constructor, which models a genuinely
 * expensive operation (network I/O, decoding, etc.) - exactly the kind of
 * cost the Proxy in this exercise exists to defer and avoid repeating.
 */
public class RealImage implements Image {
    private final String fileName;

    public RealImage(String fileName) {
        this.fileName = fileName;
        loadFromRemoteServer();
    }

    private void loadFromRemoteServer() {
        System.out.println("  [RealImage] Loading \"" + fileName + "\" from remote server (slow)...");
        try {
            Thread.sleep(150); // simulate network/decoding latency
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
        System.out.println("  [RealImage] \"" + fileName + "\" loaded.");
    }

    @Override
    public void display() {
        System.out.println("  [RealImage] Displaying \"" + fileName + "\".");
    }
}
