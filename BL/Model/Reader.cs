using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace BL.Model
{
    public class Reader
    {
        public string FilePath { get; set; }
        public Reader(string filePath)
        {
            this.FilePath = filePath;
        }

        public IEnumerable<Record> Read()
        {
            using (var streamReader = new StreamReader(FilePath, Encoding.Default))
            {
                return new CsvReader(streamReader).GetRecords<Record>().ToList();
            }
        }
    }
}
