using System;

namespace CodePepper.Tests.Helpers.Extensions
{
    public static class ClassExt
    {        public static T With<T>(this T item, Action<T> config = null) where T : class
        {
            config?.Invoke(item);
            return item;
        }
    }
}
