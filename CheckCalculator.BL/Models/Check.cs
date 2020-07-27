namespace CheckCalculator.BL
{
    public enum CheckType
    {
        Income = 1,
        InconeReturn
    }

    public class Check
    {
        public CheckType CheckType { get; set; }
    }
}
