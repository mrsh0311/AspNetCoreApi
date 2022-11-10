using System;
using System.Collections.Generic;
using System.Text;

namespace Devsharp.Core
{
    public interface IDateEntity
    {
         DateTime CreateOn { get; set; }
         DateTime UpdateOn { get; set; }
    }
}
