(super rough draft)

After spending the last year solidifying Vale's foundations, the Vale team is joining in the [repl.it](https://repl.it/) hackathon, and using the opportunity to finally taking a stab at Vale's true vision: **universal speed, safety, and simplicity!**

Universal speed, sa

Vale offers true single ownership, which enables some massive leaps forward, such as native cross-compilation and hybrid-generational memory. These open up entire new possibilities all over the programming world, but the one we're most excited about is in **app development.**

App development is one of the fastest-growing areas of software engineering today. There are [3.5 billion smartphone users](https://www.statista.com/statistics/330695/number-of-smartphone-users-worldwide/) in the world, using [2.2 million](https://buildfire.com/app-statistics/) App Store apps and [2.8 million](https://buildfire.com/app-statistics/) Play Store apps, and many of those have counterparts on the web too.

Every app developer wishes there was a way to easily write code once, and use it on all three platforms. Unfortunately, existing methods often suffer:

 * Heavy slowdowns, as we run GC'd languages on iOS without their trusty JIT-compiler, such as Java or Kotlin.
 * Difficulty communicating between JVM and a native language, like C++ or Rust, as well as the difficulties of learning how to code safely with those languages.

There's a missing piece of the puzzle, a holy grail of cross-platform development, waiting to be built.


We recently realized that Vale's [true single ownership](https://vale.dev/blog/raii-next-steps) paradigm could be the missing piece of this puzzle:

 * Vale can do **native cross-compilation**; it can compile to other languages without losing performance or safety, because its single ownership paradigm can easily work in a ref-counted or garbage-collected environment. It's high-level--it doesn't need to give the programmer direct memory access to be fast--so it can easily compile to JVM code.
 * Vale's single ownership inspired our **hybrid-generational** memory management system that's a new, much faster alternative to reference-counting and garbage collection.
 * Vale has **regions**, which can allow one to easily have references across the JVM/native, or JS/wasm boundary. Read more about this in [Vision for the Cross-Platform Core](https://vale.dev/blog/cross-platform-core-vision)!

In this hackathon, we decided to make working proof-of-concepts for the most interesting parts:

 * We tried taking Vale's high-level AST ((abstract syntax tree)) and cross-compiling it to the most challenging target: Javascript.
 * We implemented the core logic behind hybrid-generational memory.

After three weeks of excited building, we emerge victorious! **Vale can now cross-compile to Javascript, and now has the first-ever implementation of generational memory, which out-performs reference-counting and garbage collection.**

Grab an ale and sit by our fire, and we will tell you the tale of our three-weeks quest.


## Native Cross-Compilation

Cross-compilation is a challenge normally fraught with peril, but Vale was able to effortlessly sail past those problems.

Vale is the only high-level single ownership language ever made. Because of [true single ownership](https://vale.dev/blog/raii-next-steps), all owning references and non-owning references can just compile to regular Java references or Swift references, and the program will still work exactly as expected.

Other languages face problems here:

 * Garbage collected languages (Python, Java, Kotlin, Javascript) can't cross-compile to Swift/ObjC because they need to detect and free reference cycles, so they bring in their garbage collectors, which struggle on native platforms like iOS because they can't run their Just-in-Time compilers.
 * Reference-counted languages (like Swift, ObjC) need to run their deinitializers ((destructors)) deterministically, so need to keep a ref-count inside the object. This means they're paying the costs for both reference counting *and* garbage collection.
 * Native languages (like C++, Rust) expose raw memory access to the user, which we can't do in the JVM.

Single ownership solves the first two problems, and Vale's high-level nature solves the third problem.

To see if our theory was correct, we built a second "backend" for our compiler, which transforms our intermediate AST into Javascript, instead of native assembly.

As expected, it worked flawlessly!

You can try it out (link here).

Vale has become the first language to losslessly compile to a native and a garbage-collected environment, without incurring any extra costs in either!

**With this, Vale has done the impossible, and made cross-compilation a zero-cost abstraction.**


## Hybrid-Generational Memory

Vale's mission is universal speed, safety, and simplicity. It's simple language with zero unsafety, and our cross-compilation makes it truly universal, and it was already fast but **we want to make it faster.**

So, we gathered all of our knowledge of memory safety techniques from across the world, and discovered a gem, brimming with potential, hidden among the tomes of ancient knowledge. When combined with other techniques, we made something fresh, new, and *fast.*

### Generational Indices

The gem we found was a tried-and-true technique from C++ game engines, called **generational indices.** 

The basic idea is that our objects live in a bunch of "slots", usually in an array. Each slot has an object, and a **actual generation** number which means "the object here is the Nth inhabitant of this slot."

For example, slot 1234 has a Person in it, with actual generation 1. Later, when we want to remove the Person, we will zero out its memory, and change the actual generation to 2.

Let's say the Person is still here. When we give a reference to this Person to someone, we give them the index and also a copy the actual generation. This copy is called the "target generation". This reference has \[1234, 1\].

To ensure safety, anyone who dereferences the index (1234) will first check if the Person is still there, by comparing the target generation (1) with the actual generation (1). If they match, the access is valid!

It's as if the reference is saying:

   "Hello! I'm looking for the 11th inhabitant of this house, are they still around?"

and the person who opens the door says:

   "No, sorry, I am the 12th inhabitant of this house, the 11th inhabitant is no more."

or instead:

   "Yes! That is me. Which of my fields would you like to access?"

### Hybrid-Generational Memory

Generational indices have only ever been used in arrays. Our idea was to somehow use it for **everything.**

 * We had to 



Whenever we allocate anything, 


In the last three weeks, we experimented with a new idea for how Vale handles memory, called **hybrid-generational memory.**






unsafe-fast: 0.66
assist: 0.78 (18% more than unsafe-fast)
naive-rc: 0.88 (33% more than unsafe-fast)
resilient-v0: 1.08 (64% more than unsafe-fast)
resilient-v1: 0.82 (24% more than unsafe-fast)

                     non-intrusive RC

            -48%, to +33%       -62.5%, to +24%

       intrusive                       generations
             
                      -81%, to 12%


afterward, we can blend RC, generations, and escape analysis into something amazing, as low as 0.6%, which will form something called the Automatic Borrow Checker. Safer than Rust, and without the borrow checker.


we can use a borrow-checker-like substance to reduce it to 95% of that, probably.