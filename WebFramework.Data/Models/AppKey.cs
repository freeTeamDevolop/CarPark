using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Data.Models
{
    [Table("WfAppKey")]
    public partial class WfAppKey
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }
    }
}
