using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            long n;
            Input.Next(out n);
            var shoes = new List<Shoes>();
            for (var i = 0; i < n; i++)
            {
                var shoe = new Shoes{Index = i};
                shoes.Add(shoe);
                Input.Next(out shoe.Cost, out shoe.Size);
            }
            shoes = shoes.OrderBy(c => c.Size).ToList();

            long m;
            Input.Next(out m);
            var customers = new List<Customer>();
            for (var i = 0; i < m; i++)
            {
                var customer = new Customer{Index = i};
                Input.Next(out customer.Money, out customer.Size);
                customers.Add(customer);
            }
            var groups = customers.OrderBy(c => c.Money).GroupBy(c => c.Size).OrderBy(c => c.Key).ToList();

            var g = 0;
            Shoes previousShoe = null;
            foreach (var shoe in shoes)
            {
                while (g < groups.Count && groups[g].Key < shoe.Size) g++;
                if (g < groups.Count && groups[g].Key == shoe.Size)
                {
                    shoe.Customers = groups[g].Where(c => c.Money >= shoe.Cost).ToList();
                }

                var previousG = g - 1;
                if (previousG >= 0 && previousG < groups.Count && groups[previousG].Key == shoe.Size - 1)
                {
                    shoe.PreviousCustomers = groups[previousG].Where(c => c.Money >= shoe.Cost).ToList();
                }
                if (previousShoe != null && previousShoe.Size + 1 == shoe.Size)
                {
                    previousShoe.Next = shoe;
                    shoe.Previous = previousShoe;
                }
                previousShoe = shoe;
            }

            for (var i=0;i<shoes.Count;i++)
            {
                shoes[i].Buy();
                while (i < shoes.Count && shoes[i].Next != null) i++;
            }

            var sum = shoes.Where(c => c.Bought).Sum(c => c.Cost);
            Console.WriteLine(sum);
            Console.WriteLine(shoes.Count(c => c.Bought));
            foreach (var shoe in shoes.Where(c => c.Bought).OrderByDescending(c=>c.Index))
            {
                Console.WriteLine("{0} {1}", shoe.BoughtBy.Index + 1, shoe.Index + 1);
            }
        }
    }

    class Customer
    {
        public long Size;
        public long Money;
        public int Index;
    }

    class Shoes
    {
        public long Size;
        public long Cost;
        public int Index;
        public bool Bought;
        public bool InQuestion;
        public Customer BoughtBy;
        public Customer Candidate;

        public long BuyThisSum;
        public long BuyNextSum;

        public Shoes Next, Previous;
        public List<Customer> Customers = new List<Customer>();
        public List<Customer> PreviousCustomers = new List<Customer>();

        public override string ToString()
        {
            return
                string.Format("Size:{0} Cost:{1} Index:{2} Bought:{3} InQuestion:{4} BoughtBy:{5} Candidate:{6} BuyThisSum:{7} BuyNextSum:{8}",
                    Size, Cost, Index, Bought, InQuestion, BoughtBy, Candidate, BuyThisSum, BuyNextSum);
        }

        public void Buy()
        {
            if (PreviousCustomers.Any())
            {
                Bought = true;
                BoughtBy = PreviousCustomers.First();
            }
            else if (Customers.Count>1)
            {
                Bought = true;
                BoughtBy = Customers.First();
                if (Next != null) Next.PreviousCustomers.Remove(BoughtBy);
            }
            else if (Customers.Count==1)
            {
                if (Next == null || !Next.PreviousCustomers.Contains(Customers[0]) || Next.PreviousCustomers.Count>1
                    || ((Previous == null || !Previous.InQuestion) && Next.Cost <= Cost))
                {
                    Bought = true;
                    BoughtBy = Customers[0];
                    if (Next != null) Next.PreviousCustomers.Remove(BoughtBy);
                }
                else
                {
                    Candidate = Customers[0];
                    InQuestion = true;
                    Next.PreviousCustomers.Remove(Candidate);
                    BuyThisSum = Cost;
                    BuyNextSum = Next.Cost;
                    if (Previous != null && Previous.InQuestion)
                    {
                        BuyThisSum += Previous.BuyThisSum;
                        BuyNextSum += Math.Max(Previous.BuyThisSum, Previous.BuyNextSum);
                    }
                }
            }
            if (!InQuestion && Previous != null && Previous.InQuestion) Previous.Resolve();
            if (Next != null) Next.Buy();
        }

        private void Resolve()
        {
            if (Next.Bought)
            {
                Bought = true;
                BoughtBy = Candidate;
            }
            else
            {
                if (BuyThisSum >= BuyNextSum)
                {
                    Bought = true;
                    BoughtBy = Candidate;
                }
                else
                {
                    Next.Bought = true;
                    Next.BoughtBy = Candidate;
                }
            }
            if (Previous != null && Previous.InQuestion) Previous.Resolve();
        }
    }
}
