# Feature Toggle

#### PercentageDial

`ToDial(double)` returns `true` with a percentage probability. `PercentageDial` has a dependency on `Random` which needs to be thread-safe. See thread-safe `Random` implementations [here](https://github.com/rmandvikar/random2) ([nuget](https://www.nuget.org/packages/rm.Random2)). `percentage` range is `[0.01-100.00]` (2 decimal places precision).

#### CountDial

`ToDial(count)` returns `true` for first `count` calls, and `false` after. It's useful to sample few calls before dialing with a percentage.

#### CountThenPercentageDial

`ToDial(count, percentage)` returns `true` for first `count` calls, and then with a `percentage` probability. It uses `CountDial` and `PercentageDial` underneath the hood, and provides flexibility to sample few calls first and then dial with a percentage. Due to the lock perf hit, it's better to eventually change to `PercentageDial`.

#### GuidDial

`ToDial(guid, percentage)` returns `true` if the calculated value of `guid` (`[0.01-100.00]` and deterministic) is less than or equal to `percentage`. This dial makes the dialing a function of something (guid) versus randomness. It's deterministic so `IsDial(guid)` will keep returning `true` as long as the `percentage` is same or increasing.

