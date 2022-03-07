using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomerModel
{
    public class Customer
    {
        private string _customerName;
        public string CustomerName { get { return _customerName; } set { _customerName = value; } }

        private double _amount;

        public double Amount { get { return _amount; } set { _amount = value; } }

        private string _married;

        public string Married { get { return _married; } set { _married = value; } }

        private double _tax;

        public double Tax { get { return _tax; } }
        public void CalculateTax()
        {
            _tax = (_amount * 0.1);
        }
    }
}
