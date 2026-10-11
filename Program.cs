using System;
using System.Text;
using System.Threading;

namespace CarsBridgebase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var bridge = new Bridge();
            Random rdn = new Random();
            Thread[] threads = new Thread[10];

            for (int i = 1; i <= 10; i++)
            {
                int carId = i;
                Direction carDirection =rdn.Next(2) == 0 ? Direction.Left : Direction.Right;
                int carDelay = rdn.Next(100, 801);

                Console.WriteLine($"поток для авто [{carId}]");

                threads[carId - 1] = new Thread(() =>
                {
                    Thread.Sleep(carDelay);

                    Console.WriteLine(
                        $"[{DateTime.Now:mm\\:ss\\.ff}] " +
                        $"Авто [{carId}] " +
                        $"({(carDirection == Direction.Left ? "Слева" : "Справа")}) появилось"
                    );

                    Car car = new Car(carId, carDirection);

                    bridge.Enter(car);
                    Thread.Sleep(500);
                    bridge.Leave(car);
                });
            }

            foreach (var t in threads) t.Start();
            foreach (var t in threads) t.Join();
            Console.WriteLine("\nРабота завершена");

        }
    }
}
