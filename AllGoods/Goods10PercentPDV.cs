using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterGoods.AllGoods
{
    public class Goods10PercentPDV : Goods
    {
        private decimal pdv10 = 0.1m;

        public decimal CalculateSellingPricePerUnit()
        {
            SellingPricePerUnit = NetPrice * (1 + pdv10);

            return SellingPricePerUnit;
        }
    }
}
