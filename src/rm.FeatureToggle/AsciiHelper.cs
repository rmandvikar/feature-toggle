using System;

namespace rm.FeatureToggle;

public static class AsciiHelper
{
	public static bool IsAscii(this string s)
	{
		_ = s
			?? throw new ArgumentNullException(nameof(s));

		foreach (var ch in s)
		{
			if (!ch.IsAscii())
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsAscii(this char ch)
	{
		return 0 <= ch && ch <= 127;
	}
}
