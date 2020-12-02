using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using PG.SharedKernel.Custom;

namespace PG.SharedKernel.Utility
{
    public static class SeedDataReader
    {
        public static List<T> ReadCsv<T>(Assembly assembly, string @namespace = "Seed", string delimiter = ",", string fileName = "")
            where T : class
        {
            var name = string.IsNullOrWhiteSpace(fileName) ? typeof(T).Name : fileName;
            var resourceName = $"{assembly.GetName().Name}.{@namespace}.{name.HasToEndWith(".csv")}";
            var stream = assembly.GetManifestResourceStream(resourceName);
            List<T> records;
            using (StreamReader reader = new StreamReader(stream))
            {
                var csv = new CsvReader(reader,CultureInfo.InvariantCulture);
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.TrimOptions = TrimOptions.Trim;
                csv.Configuration.IgnoreBlankLines = true;
                csv.Configuration.BadDataFound = null;
                records = csv.GetRecords<T>().ToList();
            }

            return records;
        }
    }
}
