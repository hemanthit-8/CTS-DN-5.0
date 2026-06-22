package decoratorpatternexample;

public class Main {
    public static void main(String[] args) {
        System.out.println("=== Exercise 5: Decorator Pattern ===\n");

        // Just email - the base component, no decoration.
        System.out.println("-- Email only --");
        Notifier emailOnly = new EmailNotifier();
        emailOnly.send("Your order has shipped.");

        // Email + SMS - one layer of decoration.
        System.out.println("\n-- Email + SMS --");
        Notifier emailAndSms = new SMSNotifierDecorator(new EmailNotifier());
        emailAndSms.send("Your order has shipped.");

        // Email + SMS + Slack - stacked decorators, each adding a channel.
        System.out.println("\n-- Email + SMS + Slack --");
        Notifier allChannels = new SlackNotifierDecorator(new SMSNotifierDecorator(new EmailNotifier()));
        allChannels.send("Your order has shipped.");

        System.out.println("\nAll three variables above are typed as plain Notifier -");
        System.out.println("the caller doesn't need to know how many channels are layered underneath.");
    }
}
