using System.Collections.Generic;

namespace Regression.API.Models
{
    public class RecordModel
    {
        public ICollection<double> Inputs { get; set; }
        public double Output { get; set; }
    }
}