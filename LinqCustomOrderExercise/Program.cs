using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCustomOrderExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Data>
            {
                new Data { OrderId ="A001" , Sequence =0 , Price = 1000 },
                new Data { OrderId ="A002" , Sequence =1 , Price = 10 },
                new Data { OrderId ="A002" , Sequence =2 , Price = 2000 },
                new Data { OrderId ="A003" , Sequence =0 , Price = 1200 },
                new Data { OrderId ="A004" , Sequence =0 , Price = 800 },
                new Data { OrderId ="A005" , Sequence =1 , Price = 8000 },
                new Data { OrderId ="A005" , Sequence =2 , Price = 50 },
                new Data { OrderId ="A006" , Sequence =0 , Price = 300 },
            };

            var result = list.GroupBy(x => x.OrderId).OrderBy(x => x, new DataComparer()).SelectMany(x => x);
            foreach (var item in result)
            {
                Console.WriteLine($"{item.OrderId} -- {item.Sequence} -- {item.Price}");
            }

            Console.ReadLine();
        }
    }

    public class Data
    {
        public string OrderId { get; set; }
        public int Sequence { get; set; }
        public int Price { get; set; }
    }

    public class DataComparer : IComparer<IGrouping<string, Data>>
    {
        public int Compare(IGrouping<string, Data> x, IGrouping<string, Data> y)
        {
            var xPrice = x.OrderBy(d => d.Sequence).First().Price;
            var yPrice = y.OrderBy(d => d.Sequence).First().Price;
            return xPrice - yPrice;
        }
    }
}
