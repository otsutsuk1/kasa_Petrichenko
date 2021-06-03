using System;
using System.Collections.Generic;
using System.Threading;

namespace kasa
{
    class Buyer
    {
        private const bool V = false;

        internal bool IsAppeared { get; set; }
        internal bool IsBought { get; set; }
        internal bool IsServiced { get; set; }

        internal Buyer()
        {
            IsAppeared = false;
            IsBought = V;
            IsServiced = false;
        }

    }

    class Cash
    {
        private System.Collections.Generic.Queue<Thread> buyersQueue;

        internal System.Collections.Generic.Queue<Thread> GetBuyersQueue()
        {
            return buyersQueue;
        }

        internal void SetBuyersQueue(System.Collections.Generic.Queue<Thread> value)
        {
            buyersQueue = value;
        }

        internal AutoResetEvent Lock { get; set; }

        internal Cash()
        {
            SetBuyersQueue(new Queue<Thread>());
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            Lock = autoResetEvent;
        }
    }

    public class Shop1
    {
        private const int count = 50;
        private const bool V = true;
        private const int V1 = 6;

        internal List<Thread> BuyersList { get; set; }
        internal Cash[] Cashes { get; set; }
        public Shop1(int m)
        {
            BuyersList = new List<Thread>();
            Cashes = new Cash[m];
            for (int i = 0; i < Cashes.Length; i++)
                Cashes[i] = new Cash();
        }

        public void Start()
        {
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Buyer buyer = new Buyer();
                    Thread.Sleep(GaussMethod(7.0, 1.0));
                    buyer.IsAppeared = V;
                    // do something - but what?
                    Thread.Sleep(GaussMethod(12.0, 1.5));
                    buyer.IsBought = true;
                    // draw something
                    // lock something
                });
                BuyersList.Add(thread);
            }

            foreach (Thread temp in BuyersList)
                temp.Start();

            foreach (Thread temp in BuyersList)
                object p = temp.Join();
        }

        private static int GaussMethod(
            double mu,
            double sigma)
        {
            const int n = V1;
            double dSumm = 0;
            Random ran = new Random();
            dSumm += ran.NextDouble();
            return (int)Math.Abs(Math.Round((mu + sigma * (dSumm - n / 2))));
        }
    }
}
