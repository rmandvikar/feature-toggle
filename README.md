# Feature Toggle

#### PercentageDial

`ToDial(percentage)` returns `true` with a `percentage` probability. It has a dependency on `Random` which needs to be thread-safe. See thread-safe `Random` implementations [here](https://github.com/rmandvikar/random2) ([nuget](https://www.nuget.org/packages/rm.Random2)). `percentage` range is `[0.01-100.00]` (2 decimal places precision).

#### CountDial

`ToDial(count)` returns `true` for first `count` calls, and `false` after. It's useful to sample few calls before dialing with a percentage. It maintains an internal counter with a lock. Dialing `count` to `0` does not incur the lock perf hit.

#### CountThenPercentageDial

`ToDial(count, percentage)` returns `true` for first `count` calls, and then with a `percentage` probability. It uses `CountDial` and `PercentageDial` underneath the hood, and provides flexibility to sample few calls first and then dial with a percentage. Due to the lock perf hit (except when `count` is `0`), it's better to eventually change to `PercentageDial`.

#### IdDial

`ToDial(id, percentage)` returns `true` if the calculated value of a string `id` (`[0.01-100.00]` and deterministic) is less than or equal to `percentage`. This dial makes the dialing a function of something (`id`) versus randomness. It's deterministic so `IsDial(id, percentage)` will keep returning `true` as long as the `percentage` is same or increasing. It will behave uniformly if the `id` values have high cardinality, and with no traffic clustering.

#### Probability

`IsTrue(percentage)` returns `true` with a `percentage` probability. It acts as a helper method, and is used internally by `PercentageDial`.
