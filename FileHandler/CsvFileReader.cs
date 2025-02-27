using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class CsvFileReader : IFileReader
    {
        public async Task<List<T>> ReadFileAsync<T>(string fileNameWithPath)
        {
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    Comment = '%'
                };

                using (var reader = new StreamReader(fileNameWithPath))
                using (var csv = new CsvReader(reader, configuration))
                {
                    var records = csv.GetRecords<T>();
                    return records.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
