using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterGoods.AllGoods
{
    public class Goods20PercentPDV : Goods
    {
        private decimal pdv20 = 0.2m;

        public decimal CalculateSellingPricePerUnit()
        {
            SellingPricePerUnit = NetPrice * (1 + pdv20);

            return SellingPricePerUnit;
        }
    }
}
