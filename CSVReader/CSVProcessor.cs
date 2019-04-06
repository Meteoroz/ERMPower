using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using ERM_Models;
using ERM_Models.Models;
using Extensions;

namespace CSVUtilities
{
    public class CSVProcessor : ICsvProcessor
    {
        public IEnumerable<string> ProcessFiles(IEnumerable<string> fileNames)
        {
            List<string> records = new List<string>();
            var line = string.Empty;

            foreach (var fileName in fileNames)
            {
                var LPRecords = new List<LP>();
                var TOURecords = new List<TOU>();
                decimal median = 0m;

                TextReader reader = new StreamReader(fileName);
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.PrepareHeaderForMatch = (string header, int unknown) => Regex.Replace(header, "[\\s/]", string.Empty);

                if (Path.GetFileNameWithoutExtension(fileName).StartsWith("lp_", StringComparison.CurrentCultureIgnoreCase))
                {
                    LPRecords.AddRange(csvReader.GetRecords<LP>());
                    median = GetMedian(LPRecords);
                    foreach (var record in LPRecords)
                    {
                        if (record.DataValue.IsOutOfRangeOfMedian(median, .2m))
                        {
                            line = $"{fileName} {record.DateTime} {record.DataValue} {median}";
                            records.Add(line);
                        }
                    }
                }
                else
                {
                    TOURecords.AddRange(csvReader.GetRecords<TOU>());
                    median = GetMedian(TOURecords);
                    foreach (var record in TOURecords)
                    {
                        if (record.Energy.IsOutOfRangeOfMedian(median, .2m))
                        {
                            line = $"{fileName} {record.DateTime} {record.Energy} {median}";
                            records.Add(line);
                        }
                    }
                }
            }

            return records;
        }

        private static decimal GetMedian(IEnumerable<LP> records)
        {
            var values = records.Select(r => r.DataValue);
            return values.GetMedian();
        }

        private static decimal GetMedian(IEnumerable<TOU> records)
        {
            var values = records.Select(r => r.Energy);
            return values.GetMedian();
        }
    }
}
