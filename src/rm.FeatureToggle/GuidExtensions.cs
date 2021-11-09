using System;

namespace rm.FeatureToggle
{
	/// <summary>
	/// Guid extensions.
	/// </summary>
	public static class GuidExtensions
	{
		/// <note>
		/// see https://stackoverflow.com/questions/9195551/why-does-guid-tobytearray-order-the-bytes-the-way-it-does
		/// </note>
		/// <remarks>
		/// Note: The byte[] returned by <see cref="ToByteArrayMatchingStringRepresentation(Guid)"/> will not yield
		/// the original Guid with <see cref="Guid(byte[])"/> ctor.
		/// </remarks>
		public static byte[] ToByteArrayMatchingStringRepresentation(this Guid guid)
		{
			var bytes = guid.ToByteArray();
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes, 0, 4);
				Array.Reverse(bytes, 4, 2);
				Array.Reverse(bytes, 6, 2);
			}
			return bytes;
		}
	}
}
