using System;
using System.Threading.Tasks;
using CheckCalculator.BL;

namespace CheckCalculator.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fnsClient = new FnsClient();
            try
            {
                var fiscalNumber = "9289440300662133";
                var fiscalDoc = "7272";
                var fiscalSign = "0847514783";
                var checkType = FnsClient.CheckType.Income;
                var date = DateTime.Parse("24.06.2020 10:36");
                var sum = 55039;

                var resp = await fnsClient.LoginAsync("+79046174591", "514841");
                resp = await fnsClient.VerifyCheck(fiscalNumber, fiscalDoc, fiscalSign, checkType, date, sum);
                resp = await fnsClient.GetCheckInfo(fiscalNumber, fiscalDoc, fiscalSign);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}
