using System;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            var sales = new SalesDal("Customers");

            var customerTable = sales.DataSet.Tables[0];

            sales.UpdateDatabase();
        }
    }
}
