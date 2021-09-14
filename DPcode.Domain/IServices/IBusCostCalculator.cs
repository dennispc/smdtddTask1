namespace DPcode.Domain
{
    public interface IBusCostCalculator
    {
        double TotalCost(int noOfPassengers, int kilometer);
    }
}