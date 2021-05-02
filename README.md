# Feature Toggle

#### PercentageDial

`ToDial(double)` returns `true` with a percentage probability. `PercentageDial` has a dependency on `Random` which needs to be thread-safe. See thread-safe `Random` implementations [here](https://github.com/rmandvikar/random2) ([nuget](https://www.nuget.org/packages/rm.Random2)). `percentage` range is `[0.01-100.00]` (2 decimal places precision).

#### CountDial

`ToDial(count)` returns `true` for first `count` calls, and `false` after. It's useful to sample few calls before dialing with a percentage. It maintains an internal counter with a lock. Dialing `count` to `0` does not incur the lock perf hit.

