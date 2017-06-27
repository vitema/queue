using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    public class CustomQueue
    {
        private System.Collections.Queue queue;
        public int Count;
        AutoResetEvent waitEvent = new AutoResetEvent(false);

        public CustomQueue()
        {
            queue = new System.Collections.Queue();

        }

        public void Push(object obj)
        {
            lock (((ICollection)queue).SyncRoot) // предохраняемся от повреждения очереди другим потоком
            {

                queue.Enqueue(obj);
                Console.WriteLine("push value {0}", obj);
                Count++;
                waitEvent.Set(); // оповещаем pop, что добавлен новый элемент
               
            }
        }



        public void Pop()
        {

            waitEvent.WaitOne();

            lock (((ICollection)queue).SyncRoot)
            {
                var item = queue.Dequeue();
                Count--;
                Console.WriteLine("pop value {0}", item);
            }
        }



    }
}
