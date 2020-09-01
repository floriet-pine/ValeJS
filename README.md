# Vale: Universal Speed, Safety and Simplicity

[See this post on vale.dev, with pretty colors, tables, and side-comments!](https://vale.dev/blog/replit-prototype)

**August 31st, 2020**  —  Evan Ovadia and Eske Hansen

Welcome to Vale!

Vale is a very new language (announced just last month, in fact!), with modern features and a focus on easy, readable syntax, and a very nice, gradual learning curve.

```rust
fn main() {
  a = 3;
  b = 5;
  c = a + b;
  println("Behold the ancient " + c);
}
```

```rust
fn main() {
  x str = "world!";
  println("Hello " + x);
}
```

```rust
struct Spaceship {
  name Str;
  numWings Int;
}

fn main() {
  ship = Spaceship("Serenity", 2);
  println(ship.name);
}
```

Vale's goal is to show the world that speed and safety can be easy, that we don't have to make the choice between fast languages and easy languages, we can have both!

Vale has some fascinating syntactical designs ([shortcalling](https://vale.dev/ref/structs#shortcalling), [interface constructors](https://vale.dev/ref/interfaces#sealedconstructors), [the inl keyword](https://vale.dev/ref/references#inline), and more!) that will make life easier, but Vale's true power is underneath the surface, in the most important part of any programming language: **how it handles memory.**

Every other language uses a form of reference counting, garbage collection, or borrow checking. As part of the [repl.it hackathon](https://repl.it/jam), we created something completely new for Vale, something we call **Hybrid-Generational Memory,** which is over twice as fast as reference-counting!

Our efforts didn't stop there, though. Also during the hackathon, we took it even further and showed that an incredibly fast language like Vale can be seamlessly cross-compiled to other platforms, like Javascript.

With this, we've taken our first step towards Vale's true vision: universal speed, safety, and simplicity!

## Speed and Safety: Hybrid-Generational Memory

If you know what you're doing, coming back to C from Java feels amazing. C's raw speed and simplicity is truly wonderful. Java is like riding a freight train, but C is like driving a Ferrari.

Trains have their benefits though; they're much safer than cars. Even though it's slow, Java code is safer because you can't trigger undefined behavior or seg-faults, and you generally make more secure code.

If only there was a way to **easily have both speed and safety!** Previous attempts (Cyclone, ATS, Rust, Ada) have all resulted in languages with a very difficult learning curve. But is there a way to have speed, safety, and simplicity?

Yes there is! With its single ownership and high-level nature, Vale can have something we call **Hybrid-Generational Memory,** a memory model based on generation numbers. It's a revolutionary new memory model, which completely avoids reference-counting's aliasing costs, garbage collection's pauses, and borrow checkers' difficulty.

Over the last three weeks, we implemented the basic idea. We designed seven stages of optimization to avoid branching and increase cache-friendliness to get maximum speed. If you're interested in the details, you can read about the full design at [Hybrid-Generational Memory](http://localhost:8080/blog/hybrid-generational-memory-part-1).

We were shocked to see that it was way faster than reference-counting, even without optimizations!

| Mode | Speed (seconds) | Overhead Compared to Unsafe (seconds) | Overhead Compared to Unsafe (%) |
| -- | -- | -- | -- |
| Naive&nbsp;RC&nbsp;[1] | 54.90&nbsp;seconds | +11.08&nbsp;seconds | +25.29% |
| Naive&nbsp;HGM&nbsp;[2] | 48.57&nbsp;seconds | +4.75&nbsp;seconds | +10.84% |
| Unsafe&nbsp;[3] | 43.82&nbsp;seconds | n/a | n/a |

[1] Naive RC: A basic reference counting mode, to keep an object alive while there are still references alive. Basic reference counting adds 25.29% to the run time of a program, compared to Unsafe mode!

[2] Naive HGM: a basic hybrid-generational memory implementation, where before each dereference, we compare generations to see if the object is still alive. It only adds 10.84% to the run time of a program!

[3] Unsafe: A mode that has no memory safety, which compiles to roughly the same assembly code that would come from C.

Hybrid-Generational Memory only adds 10.84% to the run time of a program, **less than half the cost of reference counting!**

When we started, we thought Hybrid-Generational Memory would be better than reference counting, but we were blown away by how much. And even better, this is before the optimizations!

If you'd like to run this for yourself, you can find this [experimental version of the Vale compiler here!](https://github.com/Verdagon/Vale/raw/replitfinal/bundle/ValeCompiler0.0.8.zip) It includes the benchmark program, and instructions on running it, in the benchmark folder. 

After we add our optimizations, we expect speed on par with Rust, and almost as fast as C++! We'll be submitting benchmarks and a full publication to [OOPSLA (Object-Oriented Programming, Systems, Languages & Applications)](https://en.wikipedia.org/wiki/OOPSLA), so stay tuned!

## Clean Cross-Compilation

Vale's new memory model is incredibly fast, which makes it an amazing choice for games and servers. But why stop there? Vale could bring its speed to *even higher* realms, such as app development, because it is the first native-speed language that can do **clean cross-compilation.**

After writing an app for one platform (Web, iOS, Android), we often look longingly at the other platforms, and wish we could just re-use our code on those. Unfortunately, existing methods for sharing code between platforms often suffer:

 * Heavy slowdowns, as we run GC'd languages on iOS without their trusty JIT-compiler, such as Java or Kotlin.
 * Difficulty communicating between JVM and a native language like C++ or Rust, as well as the difficulties of learning how to code safely with those languages.

**If only there was a fast and easy language that didn't suffer these problems!** If only there was a language that could cleanly operate across the VM/native boundary. It's the missing piece of the puzzle, a holy grail of cross-platform development.

We recently realized that Vale's [true single ownership](https://vale.dev/blog/raii-next-steps) paradigm could be the missing puzzle piece!

To put it concisely, Vale's single ownership and [regions](https://vale.dev/blog/zero-cost-refs-regions) can work together to make it very easy to cross-compile to another language (JS, Java, Swift) and, with one keyword, compile to native assembly to take advantage of Vale's true speed. You can read more about this at [Vision for the Cross-Platform Core](http://localhost:8080/blog/cross-platform-core-vision).

To make that vision work, we needed to cross-compile Vale to a VM language, like Javascript. This is usually **very difficult,** as most native-speed languages (like C++) give you raw access to the memory, and VM languages like Javascript do not.

However, Vale is a native-speed language that's designed to be simple and high-level. For example, it has references instead of pointers, and, like Python, it doesn't expose raw memory.

We decided to start with compiling Vale to Javascript, and after three weeks of effort, it worked! [You can try it out at the Forked Lightning repl.it](https://repl.it/@ForkedLightning/ForkedLightningWeb).

**This is a big step forward,** because every other language incurs a big cost when cross-compiling:

 * Garbage-collected languages like Java or Kotlin bring their costly garbage collector to iOS, and they also have to run without their Just-In-Time Compilers, causing them to be even slower.
 * Reference counted languages like Swift, if compiled to JVM or JS, would suffer the cost of garbage collection and reference counting, because weak references and destructors rely on reference-counting precision.
 * Low-level languages like C++ or Rust, if compiled to JVM or JS, would need to have "simulated RAM", like what powers asm.js, causing big slowdowns.

Because single ownership fits so cleanly into native, reference-counting, and garbage-collected environments, Vale suffers **zero** extra slowdowns when cross-compiling.

With this, Vale has done the impossible, **clean cross-compilation,** and has cleared the way towards a future with **fast cross-platform code.**

## Next Steps

Now that we have prototypes of Vale's two core innovations, we'll be spending the next few months building on these foundations:

 * Add the next seven stages of optimization for Hybrid-Generational Memory, to see it reach its true speed potential!
 * Add Swift and Java cross-compilation!
 * Implement basic ID-based regions, so we can have automatic references across the JVM/native and JS/WASM boundaries!

We'll also be:

 * Adding IDE support!
 * Making the foundations solid, doing some refactors to prepare for the big features ahead!
 * Releasing Vale v0.1 with a big splash!
 * Completely replacing C++ over the next decade. Easy!

If you want to see this happen sooner, or just want to contribute to something cool, we invite you to [come join us!](http://localhost:8080/contribute)

Stay tuned for coming articles, where we talk about Vale's optimizations, pentagonal tiling, and more. If you want to learn more before then, come by the [r/Vale subreddit](http://reddit.com/r/vale) or the [Vale discord server](https://discord.gg/SNB8yGH)!

## Afterword: Hackathon and Scope

These past three weeks in the repl.it hackathon have been a wild ride, full of late-night design discussions on discord, insanity-driven optimization brainstorming, and caffeine-fueled all-nighters!

Vale's vision is vast, and too big to fit in three weeks, so we knew at the very beginning that we needed to keep scope down, and really nail the core concepts that show Vale's potential.

Here's what we did specifically:

 * Implemented a basic to-JS cross-compiler, **ValeJS**, starting with printing a simple string.
 * Modified the compiler's intermediate AST to include enough information for ValeJS to successfully construct structs.
 * Created a way for ValeJS to mimic Vale's edge-based vtables in Javascript.
 * Made a way for ValeJS to fill arrays without exposing null/undefined in the Vale's intermediate AST.
 * Finished ValeJS, and made a little roguelike (a room and an @ sign) to show it off.
 * Implemented "unsafe mode" in the compiler's native backend ("Midas"), so we could get an accurate measurement for how much overhead the other modes have.
 * Made a web-service for repl.it to talk to, which outputs Vale's intermediate AST, so ValeJS could turn it into Javascript.
 * Implemented "naive RC mode" in Midas, so we could measure our new mode against the native memory management standard.
 * Made a way to simplify the very long and complicated names from the Vale's intermediate AST to something simple that JS could handle.
 * Implemented "Resilient V0 mode" in Midas, which turned all constraint references into weak references, using a central table for all weak ref counts.
 * Implemented "Resilient V1 mode" in Midas, which had a similar central table, but for generations.
 * Implemented "Resilient V2 mode" in Midas, with the "generational heap" which embeds generations directly into the memory allocations themselves.

Here's what wasn't part of the three weeks:

 * The basic compiler, compiling to LLVM, existed before the hackathon.
 * The seven stages of optimization are planned for the next few months. That's right, we beat RC without any optimizations!
 * Unfinished features, which we paused when we started the hackathon:
   * Regions, as described in [Zero-Cost References with Regions](https://vale.dev/blog/zero-cost-refs-regions).
   * [shortcalling struct](https://vale.dev/ref/structs) and [interface constructors](https://vale.dev/ref/interfaces) is unfinished.
   * The [inl keyword](https://vale.dev/ref/references) is mostly implemented but not hooked up to all types yet.

## Appendix: Benchmark Numbers

We ran the raw numbers for each mode 7 times, and got the following numbers:

 * Unsafe mode: **43.82s,** 43.89s, 43.90s, 44.06s, 44.28s, 44.65s, 44.83s
 * Naive RC: **54.90s,** 55.17s, 55.27s, 55.32s, 55.34s, 55.37s, 55.48s
 * Resilient V2: **48.57s,** 48.91s, 49.18s, 49.19s, 49.46s, 49.59s, 51.87s

We then used the best number for each run (bolded above).
To avoid CPU caching effects, we ran this on a very large map (200 x 200), specified in main.vale.
