package commandpatternexample;

/**
 * Receiver: the object that actually knows how to perform the requested
 * action. It has no knowledge of Command, RemoteControl, or any other part
 * of the pattern - it's a plain, focused class.
 */
public class Light {
    private final String location;
    private boolean isOn = false;

    public Light(String location) {
        this.location = location;
    }

    public void turnOn() {
        isOn = true;
        System.out.println(location + " light is now ON.");
    }

    public void turnOff() {
        isOn = false;
        System.out.println(location + " light is now OFF.");
    }

    public boolean isOn() {
        return isOn;
    }
}
