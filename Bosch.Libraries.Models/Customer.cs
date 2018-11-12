using System;

namespace Bosch.Libraries.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int CreditLimit { get; set; }
        public bool ActiveStatus { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}, {5}",
                this.CustomerId, this.CustomerName, this.Address,
                this.CreditLimit, this.ActiveStatus, this.Remarks);
        }
    }
}
