using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingCommon
{
        public class VendingItem
        {
         private string _pin = "2005";
        public string PIN
        {
            get { return _pin; }
            set
            {
                if (value.Length == 4 || value.Length == 6)
                {
                    _pin = value;
                }
            }
        }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        }
}

