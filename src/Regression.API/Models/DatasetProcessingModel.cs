using System.Collections.Generic;

namespace Regression.API.Models
{
    public class DatasetProcessingModel
    {
        public ICollection<RecordModel> Records { get; set; }
    }
}