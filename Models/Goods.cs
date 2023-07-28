using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterGoods.Models
{
    public class Goods
    {
        private int _PLU;
        private string _name;
        private decimal? _quantity;
        private decimal _netPrice;
        private string _pdv;
        private string _margin;
        private decimal _sellingPricePerUnit;
        private long _barcode;

        [Key]
        public int PLU
        {
            get { return _PLU; }
            set { _PLU = value; }
        }

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Required(ErrorMessage = "Quantity is required.")]
        public decimal? Quantity
        { 
            get { return _quantity; }
            set { _quantity = value; }
        }

        [Required(ErrorMessage ="Net price is required")]
        public decimal NetPrice
        {
            get { return _netPrice; }
            set { _netPrice = value; }
        }

        [Required(ErrorMessage = "PDV is required")]
        public string PDV
        {
            get { return _pdv; }
            set { _pdv = value; }
        }

        [Required(ErrorMessage = "Margin is required.")]
        public string Margin
        {
            get { return _margin; }
             set { _margin = value; }
        }

        [Required(ErrorMessage = "Selling Price Per Unit is required.")]
        public decimal SellingPricePerUnit
        {
            get { return _sellingPricePerUnit; }
            set { _sellingPricePerUnit = value; }
        }

        [Required(ErrorMessage = "Barcode is required.")]
        public long Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }


        /* public decimal CalculateSellingPricePerUnit()
         {
             SellingPricePerUnit = NetPrice * (1 + ParsePDVPercentage(PDV));
             return SellingPricePerUnit;
         }

         private decimal ParsePDVPercentage(string pdv)
         {
             // Remove the percentage sign and parse the value as a decimal
             return decimal.Parse(pdv.TrimEnd('%')) / 100;
         }*/
    }
}
