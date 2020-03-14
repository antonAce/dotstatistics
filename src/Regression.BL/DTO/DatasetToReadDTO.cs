using System;
using System.Collections.Generic;

namespace Regression.BL.DTO
{
    public class DatasetToReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<RecordDTO> Records { get; set; }
    }
}