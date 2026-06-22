package commandpatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 9: Command Pattern ===\n");

        Light livingRoomLight = new Light("Living Room");
        Light bedroomLight = new Light("Bedroom");

        Command livingRoomOn = new LightOnCommand(livingRoomLight);
        Command livingRoomOff = new LightOffCommand(livingRoomLight);
        Command bedroomOn = new LightOnCommand(bedroomLight);
        Command bedroomOff = new LightOffCommand(bedroomLight);

        RemoteControl remote = new RemoteControl();

        System.out.println("-- Turning on the living room light --");
        remote.setCommand(livingRoomOn);
        remote.pressButton();

        System.out.println("\n-- Turning on the bedroom light --");
        remote.setCommand(bedroomOn);
        remote.pressButton();

        System.out.println("\n-- Turning off the living room light --");
        remote.setCommand(livingRoomOff);
        remote.pressButton();

        System.out.println("\n-- Turning off the bedroom light --");
        remote.setCommand(bedroomOff);
        remote.pressButton();

        System.out.println("\nTotal commands executed via this one remote: " + remote.commandsExecutedCount());
        System.out.println("The RemoteControl never referenced Light directly - only Command.");
    }
}
