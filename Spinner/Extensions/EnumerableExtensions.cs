﻿using System;
using System.Collections.Generic;

namespace Spinner.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (T item in enumeration)
			{
				action(item);
			}
		}
	}
}
