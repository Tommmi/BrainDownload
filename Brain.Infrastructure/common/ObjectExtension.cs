using System.Collections.Generic;
using System.Linq;

namespace Brain.Infrastructure.common
{
	public static class ObjectExtension
	{
		public static bool In<T>(this T @this, IEnumerable<T> list)
		{
			return list.Any(x => x.Equals(@this));
		}
		public static bool In<T>(this T @this, params T[] list)
		{
			return list.Any(x => x.Equals(@this));
		}
	}
}
