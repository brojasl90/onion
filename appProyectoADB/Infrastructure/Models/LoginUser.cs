using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    [MetadataType(typeof(LoginUserMetadata))]
    public partial class LoginUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public LoginUser()
        {

        }

        public string Identificacion { get; set; }
        public string Contrasenia { get; set; }
    }
}
