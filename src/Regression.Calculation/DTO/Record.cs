using System.Collections.Generic;

namespace Regression.Calculation.DTO
{
    public class Record
    {
        public ICollection<double> Inputs { get; set; }
        public double Output { get; set; }
    }
}