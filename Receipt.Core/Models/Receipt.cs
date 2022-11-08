using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptApi.Core.Models
{
    public class Receipt : Entity
    {
        public DateTime CreatedOn { get; set; }
        public List<Item> Items { get; set; }

        public Receipt()
        {
            Items = new List<Item>();
        }

       
    }
}
