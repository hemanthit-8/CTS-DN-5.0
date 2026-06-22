package builderpatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 3: Builder Pattern ===\n");

        // Minimal configuration: only the required CPU is set, everything else uses defaults.
        Computer budgetPc = new Computer.Builder("Intel Core i3").build();
        System.out.println("Budget PC (defaults for everything else):");
        System.out.println("  " + budgetPc);

        // Fully customized configuration, built fluently.
        Computer gamingPc = new Computer.Builder("Intel Core i9")
                .ramGb(32)
                .storageGb(2000)
                .hasGraphicsCard(true)
                .hasWifi(true)
                .build();
        System.out.println("\nGaming PC (every option customized):");
        System.out.println("  " + gamingPc);

        // Partial customization: only the options that matter are overridden.
        Computer officePc = new Computer.Builder("AMD Ryzen 5")
                .ramGb(16)
                .hasWifi(false) // wired connection only
                .build();
        System.out.println("\nOffice PC (only RAM and WiFi customized, rest default):");
        System.out.println("  " + officePc);
    }
}
