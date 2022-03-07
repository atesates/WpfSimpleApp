using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCustomerModel;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace MyCustomerViewModel
{
    public class CustViewModel :INotifyPropertyChanged
    {
        private Customer obj = new Customer();

        private BtnClick ocammand;

        public CustViewModel()
        {
            ocammand = new BtnClick(CalculateTax, IsValid);//this btnClick class can be used by every ViewModel
        }
        public bool IsValid()
        {
            if(obj.Amount < 0)
            {
                return false;
            }
            else return true;
        }
        public ICommand BtnClick
        {
            get
            {
                return ocammand;    
            }
        }
        public string txtName { 
            get 
            { 
                return obj.CustomerName; 
            } 
            set 
            { 
                obj.CustomerName = value; 
            } 
        }
        public string txtAmount { 
            get 
            { 
                return obj.Amount.ToString(); 
            } 
            set
            { 
                obj.Amount = Convert.ToInt16(value);
                Refresh("lblAmountColor");
                ocammand.Refresh();
            } 
        }    
        public string lblAmountColor//use least denometer datatype like string. Do not use ui specific datatype in order to be independet of ui
        {
            get
            {
                if (obj.Amount > 2000)
                {
                    return "Red";
                }
                else if (obj.Amount > 1000)
                { 
                    return "Orange"; 
                }
                return "Yellow";
            }
         
        }
        public bool IsMarried
        {
            get
            {
                if (obj.Married == "Married")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                obj.Married = "Married";
            }
        }
        private void Refresh(string properyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properyname));
        }

        public string txtTax
        {
            get { return obj.Tax.ToString(); }
        }

        public void CalculateTax() 
        {
            obj.CalculateTax();  
            Refresh("txtTax");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class BtnClick : ICommand
    {
        //private CustViewModel obj;//actually we dont need whole object, for reusability we will use delegates//decouple vıewmodel from command
        private Action WhatToExecute;//Delegate
        private Func<bool> WhetherToExecute;//Delegate
        public BtnClick(Action What, Func<bool> When)
        {
            WhatToExecute = What;
            WhetherToExecute = When;
        }

        public void Refresh()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);   
            }

        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return WhetherToExecute();
        }

        public void Execute(object parameter)
        {
            WhatToExecute();
        }
    }
}
