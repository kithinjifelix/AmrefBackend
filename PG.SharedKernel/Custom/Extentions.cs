using System;
using System.Collections.Generic;

namespace PG.SharedKernel.Custom
{
   public static class Extentions
    {
        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        /// <summary>
        /// Determines if Guid is Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// Determines if a nullable DateTime (DateTime?) is null or Default Empty date (01/01/0001)
        /// </summary>
        public static bool IsNullOrEmpty(this DateTime? date)
        {
            if (null == date)
                return true;

            if (date.Value==DateTime.MinValue)
                return true;

            return false;
        }

        /// <summary>
        /// Determines if a nullable DateTime (DateTime) is Default Empty date (01/01/0001)
        /// </summary>
        public static bool IsNullOrEmpty(this DateTime date)
        {
            if (date==DateTime.MinValue)
                return true;

            return false;
        }

        /// <summary>
        /// formatted date
        /// </summary>
        public static string ToDateCustomFormat(this DateTime? date,string format="dd-MMM-yyyy")
        {
            return date.IsNullOrEmpty() ? string.Empty : date.Value.ToDateCustomFormat();
        }

        /// <summary>
        /// formatted date with Time
        /// </summary>
        public static string ToDateTimeCustomFormat(this DateTime? date,string format="dd-MMM-yyyy HH:mm:ss")
        {
            return date.IsNullOrEmpty() ? string.Empty : date.Value.ToDateTimeCustomFormat();
        }

        /// <summary>
        /// formatted date
        /// </summary>
        public static string ToDateCustomFormat(this DateTime date,string format="dd-MMM-yyyy")
        {
            return $"{date.ToString(format)}";
        }
        /// <summary>
        /// formatted date with Time
        /// </summary>
        public static string ToDateTimeCustomFormat(this DateTime date,string format="dd-MMM-yyyy HH:mm:ss")
        {
            return $"{date.ToString(format)}";
        }
        /// <summary>
        /// compare strings insenitive
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsSameAs(this string value, string other)
        {
            if (value == null)
                return false;
            return value.Trim().ToLower() == other.Trim().ToLower();
        }

        public static string HasToEndWith(this string value, string end)
        {
            if (value == null)
                return string.Empty;

            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string HasToStartWith(this string value, string start)
        {
            if (value == null)
                return string.Empty;

            return value.StartsWith(start) ? value : $"{start}{value}";
        }

        public static string ToUnixStyle(this string value)
        {
            if (value == null)
                return string.Empty;

            return value.Replace(@"\", @"/");
        }

        public static string ToOsStyle(this string value)
        {
            if (value == null)
                return string.Empty;

            if(Environment.OSVersion.Platform==PlatformID.Unix||Environment.OSVersion.Platform==PlatformID.MacOSX)
                return value.Replace(@"\", @"/");

            return value.Replace(@"/", @"\");
        }

        public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> enumerable,
            int chunkSize)
        {
            if (chunkSize < 1) throw new ArgumentException("chunkSize must be positive");

            using (var e = enumerable.GetEnumerator())
                while (e.MoveNext())
                {
                    var remaining = chunkSize;    // elements remaining in the current chunk
                    var innerMoveNext = new Func<bool>(() => --remaining > 0 && e.MoveNext());

                    yield return e.GetChunk(innerMoveNext);
                    while (innerMoveNext()) {/* discard elements skipped by inner iterator */}
                }
        }

        private static IEnumerable<T> GetChunk<T>(this IEnumerator<T> e,
            Func<bool> innerMoveNext)
        {
            do yield return e.Current;
            while (innerMoveNext());
        }
    }
}
