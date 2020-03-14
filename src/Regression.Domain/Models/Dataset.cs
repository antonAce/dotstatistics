using System;
using System.Collections.Generic;

namespace Regression.Domain.Models
{
    public class Dataset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}