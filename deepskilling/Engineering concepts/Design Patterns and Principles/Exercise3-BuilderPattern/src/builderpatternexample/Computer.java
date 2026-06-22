package builderpatternexample;

/**
 * A Computer can be configured from many optional parts (CPU is required;
 * everything else is optional with sensible defaults). Building it through
 * a multi-argument constructor would force every caller to pass every
 * parameter, in a fixed order, even for parts they don't care about -
 * exactly the problem the Builder Pattern avoids.
 */
public class Computer {

    // Required
    private final String cpu;

    // Optional, with defaults
    private final int ramGb;
    private final int storageGb;
    private final boolean hasGraphicsCard;
    private final boolean hasWifi;

    // Private constructor: a Computer can only be created via Computer.Builder.
    private Computer(Builder builder) {
        this.cpu = builder.cpu;
        this.ramGb = builder.ramGb;
        this.storageGb = builder.storageGb;
        this.hasGraphicsCard = builder.hasGraphicsCard;
        this.hasWifi = builder.hasWifi;
    }

    @Override
    public String toString() {
        return "Computer{cpu='" + cpu + "', ramGb=" + ramGb + ", storageGb=" + storageGb
                + ", hasGraphicsCard=" + hasGraphicsCard + ", hasWifi=" + hasWifi + "}";
    }

    /**
     * Static nested Builder class: accumulates configuration via chained
     * setter-style methods, then produces an immutable Computer via build().
     */
    public static class Builder {
        private final String cpu; // required, so it's passed to the Builder's constructor
        private int ramGb = 8;             // sensible default
        private int storageGb = 256;       // sensible default
        private boolean hasGraphicsCard = false;
        private boolean hasWifi = true;

        public Builder(String cpu) {
            this.cpu = cpu;
        }

        public Builder ramGb(int ramGb) {
            this.ramGb = ramGb;
            return this;
        }

        public Builder storageGb(int storageGb) {
            this.storageGb = storageGb;
            return this;
        }

        public Builder hasGraphicsCard(boolean hasGraphicsCard) {
            this.hasGraphicsCard = hasGraphicsCard;
            return this;
        }

        public Builder hasWifi(boolean hasWifi) {
            this.hasWifi = hasWifi;
            return this;
        }

        public Computer build() {
            return new Computer(this);
        }
    }
}
