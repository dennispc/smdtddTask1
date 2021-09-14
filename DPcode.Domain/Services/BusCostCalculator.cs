using System;
using DPcode.Core;
namespace DPcode.Domain
{
    public class BusCostCalculator : IBusCostCalculator
    {
        public double TotalCost(int noOfPassengers, int kilometer)
        {
            double cost = 130;
            if (kilometer < 100)
                cost += kilometer * Bus.FeeBelow100Kms;
            else if (kilometer < 500)
            {
                if (noOfPassengers < 12)
                    cost += kilometer * Bus.FeeOver100KmsAndLessThan500IfLessThan12Passengers;
                else if (noOfPassengers >= 12)
                    cost += kilometer * Bus.FeeOver100KmsAndLessThan500IfMoreThan12Passengers; ;
            }
            else
                cost += kilometer * Bus.FeeMoreThan500kms;
            return cost;
        }
    }
}
