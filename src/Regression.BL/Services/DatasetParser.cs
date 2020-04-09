using System.Collections.Generic;
using System.Linq;
using Regression.BL.DTO;
using Regression.BL.Interfaces;

namespace Regression.BL.Services
{
    public class DatasetParser : IDatasetParser
    {
        public DatasetToSaveDTO FromString(string name, string source)
        {
            var target = new DatasetToSaveDTO();
            string[] rows = source.Split("\n");
            target.Name = name;
            target.Records = new List<RecordDTO>();

            foreach (var row in rows)
            {
                var samples = row.Split(",");
                target.Records.Add(new RecordDTO
                {
                    Inputs = samples.SkipLast(1).Select(double.Parse).ToArray(),
                    Output = samples.TakeLast(1).Select(double.Parse).First()
                });
            }

            return target;
        }
    }
}