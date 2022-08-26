using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class BookSettings: IBookSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }

    public interface IBookSettings
    {
        string Server { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
}
