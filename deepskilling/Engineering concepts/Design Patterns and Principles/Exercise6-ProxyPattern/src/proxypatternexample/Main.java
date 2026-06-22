package proxypatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 6: Proxy Pattern ===\n");

        System.out.println("Creating proxies for two images (should be instant, no loading):");
        Image photo1 = new ProxyImage("sunset.jpg");
        Image photo2 = new ProxyImage("mountains.jpg");

        System.out.println("\nDisplaying photo1 for the first time (expect a load):");
        photo1.display();

        System.out.println("\nDisplaying photo1 again (expect a cache hit, no reload):");
        photo1.display();

        System.out.println("\nDisplaying photo2 for the first time (expect a load):");
        photo2.display();

        System.out.println("\nDisplaying photo2 again (expect a cache hit, no reload):");
        photo2.display();

        System.out.println("\nNote: photo1 and photo2 were never touched again from when they were");
        System.out.println("first declared as Image until display() was called - the remote load");
        System.out.println("only happened on first real use, and only once per image.");
    }
}
