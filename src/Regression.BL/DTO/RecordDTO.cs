using System.Collections.Generic;

namespace Regression.BL.DTO
{
    public class RecordDTO
    {
        public ICollection<double> Inputs { get; set; }
        public double Output { get; set; }
    }
}