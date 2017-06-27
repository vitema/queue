using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace Queue
{
    class Program
    {

        static void Main(string[] args)
        {
            var q = new CustomQueue();

            // проверка в обычном режиме, в  одном потоке
            q.Push(3);
            q.Pop();


            // проверяем работу очереди при запуске операций в разных потоках

            // запускаем потоки операции pop, ожидающие события push
            for (int i = 0; i < 10; i++)
            {
                Thread popThread = new Thread(new ThreadStart(q.Pop));
                popThread.Start();
            }


            // запускаем потоки push, при вставке будет срабатывать ожидающая операция pop
            for (int i = 0; i < 10; i++)
            {
                Thread pushThread = new Thread(new ParameterizedThreadStart(q.Push));
                Thread.Sleep(1000);
                pushThread.Start(i);
                


            }

            Console.ReadLine();
        }

       



    }  
}
