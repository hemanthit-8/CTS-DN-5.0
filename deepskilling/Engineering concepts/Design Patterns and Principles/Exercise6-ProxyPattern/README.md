# Exercise 6: Proxy Pattern

## Scenario
An image viewer loads images from a remote server, which is slow. The viewer needs to avoid that cost until an image is actually displayed, and avoid paying it again if the same image is displayed more than once.

## What problem the Proxy Pattern solves
Loading every image as soon as it's referenced (e.g. when a gallery page is built) wastes time and bandwidth on images the user may never actually scroll to. The Proxy Pattern introduces a stand-in object that implements the exact same interface as the real object, so callers can't tell them apart, but controls access to the real object - here, by deferring its creation until first use (lazy initialization) and reusing it afterward (caching) instead of creating it fresh on every call.

## Implementation
- `Image.java` - the subject interface shared by both the real image and its proxy: `display()`.
- `RealImage.java` - the real subject. Loading happens in its constructor, simulating an expensive remote fetch (with a short `Thread.sleep` standing in for network latency) - exactly the cost we want to defer and avoid repeating.
- `ProxyImage.java` - implements `Image`, holds a `null` `RealImage` reference until `display()` is called for the first time, at which point it constructs (and caches) the `RealImage`; subsequent `display()` calls reuse the cached instance with no reload.
- `Main.java` - creates two `ProxyImage`s (instant, no loading), then calls `display()` on each twice, showing the first call triggers a load and the second is served from cache.

Compile and run:
```bash
cd Exercise6-ProxyPattern
javac -d out src/proxypatternexample/*.java
java -cp out proxypatternexample.Main
```

In the sample output, constructing `photo1` and `photo2` produces no `[RealImage] Loading...` line at all - that only appears the first time `display()` is called on each, and never again afterward for the same image.

## Types of proxies, and how this one fits
The Gang of Four catalog describes several proxy variants, often combined in one implementation as it is here: a **virtual proxy** defers creation of an expensive object until it's needed (the lazy-initialization behavior shown above); a **caching proxy** stores results of expensive operations to avoid repeating them (the caching behavior shown above); a **remote proxy** represents an object that actually lives in a different address space (a real version of this scenario would have `RealImage`'s constructor make an actual network call); and a **protection proxy** controls access based on permissions (not used in this exercise, but a natural extension - e.g. only allowing `display()` for authenticated users).

## Proxy vs. Decorator: a common point of confusion
Both patterns wrap an object behind the same interface, which makes them easy to mix up. The difference is *intent*: Decorator (Exercise 5) is about adding new behavior/responsibilities to an object that otherwise functions normally on its own. Proxy is about controlling *access* to an object - deciding whether, when, and how the real object gets used at all (deferring its creation, restricting who can reach it, or short-circuiting repeated calls). In this exercise, `ProxyImage` actively prevents `RealImage` from being constructed until necessary; a Decorator would assume the wrapped object already exists and simply add behavior around it.
