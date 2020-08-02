using Bytha.Models;
using System;
using System.Collections.Generic;
namespace Bytha.Models
{
    public class Stuff
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
