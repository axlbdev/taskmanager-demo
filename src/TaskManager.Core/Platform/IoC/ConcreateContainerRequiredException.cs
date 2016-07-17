using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDemo.Core.Platform.IoC
{
    public class ConcreateContainerRequiredException
        : InvalidOperationException
    {
        public ConcreateContainerRequiredException()
            : base("Lightinject Container implementation expected.") { }
    }
}
