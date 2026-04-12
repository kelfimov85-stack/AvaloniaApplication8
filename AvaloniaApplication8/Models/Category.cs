using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication8.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Products> Products { get; set; }


    }
}
