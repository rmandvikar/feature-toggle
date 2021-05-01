# Feature Toggle

#### PercentageDial

`ToDial(double)` returns `true` with a percentage probability. `PercentageDial` has a dependency on `Random` which needs to be thread-safe. See thread-safe `Random` implementations [here](https://github.com/rmandvikar/random2) ([nuget](https://www.nuget.org/packages/rm.Random2)). `percentage` range is `[0.01-100.00]` (2 decimal places precision).

