using CustomORM.DAL;
using CustomORM.Model;
using System;

namespace CustomORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper helper = new SqlHelper();

                StuInfo u = helper.Find<StuInfo>(2);

                var a = helper.Select<StuInfo>(d => d.Id > 0 && d.Id < 3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
