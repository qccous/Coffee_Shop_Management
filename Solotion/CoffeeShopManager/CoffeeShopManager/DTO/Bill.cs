using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class Bill
    {
        private DateTime? dateCheckout;

        private int iD;

        private DateTime? timeCheckin;

        private int status;
        private int discount;

        public Bill(int iD, DateTime? timeCheckin, DateTime? dateCheckout, int status, int discount = 0)
        {
            this.ID = iD;
            this.TimeCheckin = timeCheckin;
            this.DateCheckout = dateCheckout;
            this.Status = status;
            this.discount = discount;
          
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["idBill"];
            this.TimeCheckin = (DateTime?)row["timeCheckin"];
            var dateCheckoutTemp = row["dateCheckout"];
            if (dateCheckoutTemp.ToString() != "")
            {
                this.DateCheckout = (DateTime?)row["dateCheckout"];
            }

            this.Status = (int)row["status"];

            if(row["discount"].ToString()!="")
            this.Discount = (int)row["discount"];
        }
        public int ID { get => iD; set => iD = value; }
        public DateTime? TimeCheckin { get => timeCheckin; set => timeCheckin = value; }
        public DateTime? DateCheckout { get => dateCheckout; set => dateCheckout = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
