using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string message, string property)
        {
            Succedeed = succedeed;
            Message = message;
            Property = property;
        }

        public bool Succedeed { get;}
        public string Message { get;}
        public string Property { get;}
    }
}
