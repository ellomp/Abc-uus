using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Data.Common
{
    public class NamedEntityData : UniqueEntityData
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
