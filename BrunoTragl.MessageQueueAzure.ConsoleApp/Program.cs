using System;

namespace BrunoTragl.MessageQueueAzure.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QueueService queueService = new QueueService();
            //queueService.InsertQueue();
            //queueService.InsertQueue();
            //queueService.InsertQueue();
            //queueService.InsertQueue();
            //queueService.InsertQueue();
            //queueService.InsertQueue();
            foreach (ObjetoDeTeste obj in queueService.ReadQueue())
            {
                Console.WriteLine($"Cor {obj.Cor}");
                Console.WriteLine($"Valor R$ {obj.Valor}");
                Console.WriteLine($"Quantidade {obj.Quantidade}");
                Console.WriteLine();
                Console.WriteLine("############");
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Aguardando uma tecla");
            Console.ReadKey();
        }
    }
}
