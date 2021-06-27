# Feature Toggle

#### PercentageDial

`ToDial(percentage)` returns `true` with a `percentage` probability. It has a dependency on `Random` which needs to be thread-safe. See thread-safe `Random` implementations [here](https://github.com/rmandvikar/random2) ([nuget](https://www.nuget.org/packages/rm.Random2)). `percentage` range is `[0.01-100.00]` (2 decimal places precision).

#### CountDial

`ToDial(count)` returns `true` for first `count` calls, and `false` after. It's useful to sample few calls before dialing with a percentage. It maintains an internal counter with a lock. Dialing `count` to `0` does not incur the lock perf hit.

#### CountThenPercentageDial

`ToDial(count, percentage)` returns `true` for first `count` calls, and then with a `percentage` probability. It uses `CountDial` and `PercentageDial` underneath the hood, and provides flexibility to sample few calls first and then dial with a percentage. Due to the lock perf hit (except when `count` is `0`), it's better to eventually change to `PercentageDial`.

#### Probability

`IsTrue(percentage)` returns `true` with a `percentage` probability. It acts as a helper method, and is used internally by `PercentageDial`.

