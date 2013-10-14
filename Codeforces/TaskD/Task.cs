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
            task.Solve();
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
            }
            var groups = customers.OrderBy(c => c.Money).GroupBy(c => c.Size).OrderBy(c => c.Key).ToList();

            var g = 0;
            Shoes previousShoe = null;
            foreach (var shoe in shoes)
            {
                if (previousShoe != null && shoe.Size == previousShoe.Size + 1 && !shoe.Bought) shoe.Previous = previousShoe;

                while (g < groups.Count && groups[g].Key < shoe.Size) g++;
                if (g >= groups.Count)
                {
                    if (shoe.Previous != null)
                    {
                        var p = shoe.Previous;
                        while (p != null && p.InQuestion)
                        {
                            p.Bought = true;
                            p.BoughtBy = p.Candidate;
                            var a = p.Previous;
                            p.Previous = null;
                            p = a;
                        }
                    }
                    shoe.Previous = null;
                    break;
                }
                if (groups[g].Key == shoe.Size) //found customers with the same size
                {
                    var group = groups[g];
                    Customer candidate = null;
                    foreach (var customer in group)
                    {
                        if (customer.Money < shoe.Cost) continue;
                        if (candidate == null)
                        {
                            candidate = customer;
                            if (shoe.Previous != null)
                            {
                                shoe.Bought = true;
                                shoe.BoughtBy = customer;
                                candidate = null;
                                break;
                            }
                        }
                        else //this is second candidate with >= money
                        {
                            shoe.Bought = true;
                            shoe.BoughtBy = customer;
                            candidate = null;
                            break;
                        }
                    }

                    var canBuyPreviousShoe = false;

                    if (shoe.Previous != null)
                    {
                        var cost = shoe.Previous.Cost;
                        foreach (var customer in group)
                        {
                            if (customer == shoe.BoughtBy) continue;
                            if (customer.Money < cost) continue;
                            if (customer == candidate)
                            {
                                canBuyPreviousShoe = true;
                                continue;
                            }

                            shoe.Previous.BoughtBy = customer;
                            shoe.Previous.Bought = true;
                            var p = shoe.Previous;
                            while (p.Previous != null)
                            {
                                p.Previous.Bought = true;
                                p.Previous.BoughtBy = p.Candidate;
                                var a = p.Previous;
                                p.Previous = null;
                                p = a;
                            }
                            shoe.Previous = null;
                            canBuyPreviousShoe = false;
                            break;
                        }

                        if (shoe.Previous != null && canBuyPreviousShoe)
                        {
                            if (shoe.Previous.Cost >= shoe.Cost)
                            {
                                shoe.Previous.BoughtBy = candidate;
                                shoe.Previous.Bought = true;
                                shoe.Previous = null;
                            }
                            else
                            {
                                shoe.InQuestion = true;
                                shoe.Candidate = candidate;
                            }
                        }

                        if (!shoe.InQuestion) shoe.Previous = null;
                    }
                }
                else //no customers with such size
                {
                    if (shoe.Previous != null)
                    {
                        var p = shoe.Previous;
                        while (p != null && p.InQuestion)
                        {
                            p.Bought = true;
                            p.BoughtBy = p.Candidate;
                            var a = p.Previous;
                            p.Previous = null;
                            p = a;
                        }
                    }
                    shoe.Previous = null;
                }
                previousShoe = shoe;
            }

            var sum = shoes.Where(c => c.Bought).Sum(c => c.Cost);
            Console.WriteLine(sum);
            foreach (var shoe in shoes.Where(c => c.Bought))
            {
                Console.WriteLine("{0} {1}", shoe.Index, shoe.BoughtBy.Index);
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
        public Shoes Previous;
    }
}
