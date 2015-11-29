using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace BL.Model
{
    public class Reader
    {
        public string FilePath { get; set; }
        public Reader(string filePath)
        {
            this.FilePath = filePath;
        }

        public List<Record> Read()
        {
            if (!File.Exists(FilePath))
                return null;

            using (var streamReader = new StreamReader(FilePath, Encoding.Default))
            {
                var csv = new CsvReader(streamReader);
                csv.Configuration.RegisterClassMap<MyClassMap>();
                csv.Configuration.IgnoreReadingExceptions = true;

                return csv.GetRecords<Record>().ToList();
            }
        }
    }

    public sealed class MyClassMap : CsvClassMap<Record>
    {
        public MyClassMap()
        {
            Map(m => m.Date).ConvertUsing(row => DateTime.ParseExact(row.CurrentRecord[0], "dd.MM.yyyy", null));
            Map(m => m.Client);
            Map(m => m.Product);
            Map(m => m.Cost);
        }
    }
}
