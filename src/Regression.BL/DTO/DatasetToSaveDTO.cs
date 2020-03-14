using System.Collections.Generic;

namespace Regression.BL.DTO
{
    public class DatasetToSaveDTO
    {
        public string Name { get; set; }
        public ICollection<RecordDTO> Records { get; set; }
    }
}