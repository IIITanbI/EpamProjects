using System;
using System.Collections.Generic;
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

        public IEnumerable<Record> Read()
        {
            
            ;
            if (!File.Exists(FilePath))
                return null;
            using (var streamReader = new StreamReader(FilePath, Encoding.Default))
            {
                var csv = new CsvReader(streamReader);
                //csv.Configuration.PropertyBindingFlags = BindingFlags.Public | BindingFlags.Instance;
                //csv.Configuration.RegisterClassMap<MyClassMap>();
                csv.Configuration.IgnoreReadingExceptions = true;

                //while (csv.Read())
                //{
                //    var record = csv.GetRecord<Record>();
                //}
                List<Record> rr = csv.GetRecords<Record>().ToList();
                
                return rr;
                //return new CsvReader(streamReader).GetRecords<Record>().ToList();
            }
        }
    }

    public sealed class MyClassMap : CsvClassMap<Record>
    {
        public MyClassMap()
        {
            Map(m => m.Date);
            Map(m => m.Client);
            Map(m => m.Product);
            Map(m => m.Cost);
        }
    }
}
