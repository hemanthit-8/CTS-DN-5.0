package singletonpatternexample;

import java.time.LocalTime;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * A logging utility with exactly one instance for the entire application
 * lifetime, so every part of the app writes to the same shared log.
 *
 * Thread-safety: this uses the "initialization-on-demand holder" idiom.
 * The JVM guarantees a class is loaded and initialized lazily — only the
 * first time it is actively referenced — and that this initialization is
 * synchronized internally by the classloader. That gives lazy, thread-safe
 * creation of the single instance with no explicit locking or volatile
 * keyword needed, unlike the classic "check-then-create" lazy singleton,
 * which requires synchronized or double-checked locking to be safe under
 * concurrent access.
 */
public final class Logger {

    private final List<String> logEntries = new ArrayList<>();

    // Private constructor: nothing outside this class can call `new Logger()`.
    private Logger() {
        logEntries.add("Logger instance created.");
    }

    // Holder is not loaded until getInstance() is called for the first
    // time, so the Logger itself isn't constructed until it's actually needed.
    private static class Holder {
        private static final Logger INSTANCE = new Logger();
    }

    /** The single point of access to the Logger instance. */
    public static Logger getInstance() {
        return Holder.INSTANCE;
    }

    public void log(String message) {
        String entry = "[" + LocalTime.now().withNano(0) + "] " + message;
        logEntries.add(entry);
        System.out.println(entry);
    }

    public List<String> getLogHistory() {
        return Collections.unmodifiableList(logEntries);
    }
}
