using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.APIs
{
    [DataContract]
    public class PaisAPI
    {
        [DataMember]
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
