using System;
using System.Threading;

namespace CarsBridgebase
{
    public enum Direction { Left, Right }
    public class Car
    {
        public int Id;
        public Direction Dir;

        public Car(int id, Direction dir) { 
            Id = id;
            Dir = dir;
        }
    }
    public class Bridge
    {
        private static int carsOnBridge = 0;        
        private readonly object locker = new object();
        private bool occupied = false;

        public void Enter(Car car)
        {
            lock (locker)
            {
                while (occupied)
                {
                    Monitor.Wait(locker);
                }

                occupied = true;
                Console.WriteLine($"[{GetTime()}] Авто [{car.Id}] ({(car.Dir == Direction.Left ? " <- " : " -> ")}) на мосту");
            }
        }
        public void Leave(Car car)
        {
            lock (locker)
            {
                occupied = false;
                Console.WriteLine($"[{GetTime()}] " +
                              $"Авто [{car.Id}] " +
                              $"({(car.Dir == Direction.Left ? " <- " : " -> ")}) съехал с моста");
                Monitor.PulseAll(locker);
            }
        }
        private string GetTime()
        {
            return DateTime.Now.ToString("mm\\:ss\\.ff");
        }
    }
}
