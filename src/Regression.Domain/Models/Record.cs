using System.Collections.Generic;

namespace Regression.Domain.Models
{
    public class Record
    {
        public ICollection<double> Inputs { get; set; }
        public double Output { get; set; }
    }
}