using DPcode.Core;
using DPcode.Domain;
using NUnit.Framework;


namespace DPcode.Tests
{
    public class BusCostCalculatorTest
    {
        /*
Exercise 1
A bus company has some weird rules for price calculation: 
The price depends on the distance and the number of passengers. 
The rules are as follows: 
There is an initial fee of 130 kr. 
Anything below 100 km costing 3.20 kr. per kilometer, irrespective of passenger numbers. 
For the kilometers that are above 100 and less than 500 paid 2.75 kr. for each kilometer if there are less than 12 passengers. 
If not below 12 passengers it costs 3.00 kr per kilometer. 
For the kilometers above 500, each kilometer costs 2.25 kr. 
        */

        private IBusCostCalculator _busCalc;
        int passengerNumber1;
        int passengerNumber2;
        int lessThan100Kms;
        int moreThan100KmsButLessThan500;
        int moreThan500Kms;
        [SetUp]
        public void Setup()
        {
            _busCalc = new BusCostCalculator();
            passengerNumber1 = 13;
            passengerNumber2 = 11;
            lessThan100Kms = 44;
            moreThan100KmsButLessThan500 = 250;
            moreThan500Kms = 560;
        }

        [Test]
        public void InitialFeeTest()
        {
            //The inital fee must be 130 kr.
            Assert.AreEqual(Bus.initialFee, _busCalc.TotalCost(0, 0));

        }

        [Test]
        public void LowKmTest()
        {
            //Anything velow 100 kms costs 3.2kr per kilometer irrespective of passenger numbers

            int AmountOfKms = lessThan100Kms;
            //Checks that it gives the same result regardless of amount of passengers
            Assert.AreEqual(_busCalc.TotalCost(passengerNumber1, AmountOfKms), _busCalc.TotalCost(passengerNumber2, AmountOfKms));
            double expectedCostAtKms = AmountOfKms * Bus.FeeBelow100Kms + Bus.initialFee;
            //Check that it calculates the correct cost when below 100kms
            Assert.AreEqual(expectedCostAtKms, _busCalc.TotalCost(passengerNumber1, AmountOfKms));
        }

        [Test]
        public void MediumKmTestFewPassengersTest()
        {
            //For the kilometers that are above 100 and less than 500 paid 2.75 kr. for each kilometer if there are less than 12 passengers. 
            int amountOfKms = moreThan100KmsButLessThan500;
            double expectedCostAtKms = Bus.FeeOver100KmsAndLessThan500IfLessThan12Passengers * amountOfKms + Bus.initialFee;

            Assert.AreEqual(expectedCostAtKms, _busCalc.TotalCost(passengerNumber2, amountOfKms),
            "Calculator gives the expected result at less than 12 customers");


            Assert.AreNotEqual(_busCalc.TotalCost(passengerNumber1, amountOfKms), _busCalc.TotalCost(passengerNumber2, amountOfKms),
            "Calculator gives a different cost at more passengers");

        }

        [Test]
        public void MediumKmTestManyPassengersTest()
        {

            //If not below 12 passengers it costs 3.00 kr per kilometer.
            int amountOfKms = moreThan100KmsButLessThan500;

            double expectedCostAtKms = Bus.FeeOver100KmsAndLessThan500IfMoreThan12Passengers * amountOfKms + Bus.initialFee;

            Assert.AreEqual(expectedCostAtKms, _busCalc.TotalCost(passengerNumber1, amountOfKms),
            "Calculator gives the expected result at less than 12 customers");

        }

        [Test]
        public void HighKmTest()
        {
            //For the kilometers above 500, each kilometer costs 2.25 kr. 
            int amountOfKms = moreThan500Kms;

            double expectedCostAtKms = amountOfKms * Bus.FeeMoreThan500kms + Bus.initialFee;

            
            Assert.AreEqual(expectedCostAtKms, _busCalc.TotalCost(passengerNumber1, amountOfKms));
            
            Assert.AreEqual(expectedCostAtKms, _busCalc.TotalCost(passengerNumber2, amountOfKms));
        }
    }
}