using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BrunoTragl.MessageQueueAzure.ConsoleApp
{
    public class QueueService
    {
        private string _connectionString;
        private string _queueName;
        private QueueClient _queueClient;
        public QueueService()
        {
            _connectionString = ConfigurationManager.AppSettings["connectionStringQueue"];
            _queueName = ConfigurationManager.AppSettings["queueName"];
            _queueClient = new QueueClient(_connectionString, _queueName);

            _queueClient.CreateIfNotExists();

            if (!_queueClient.Exists())
                throw new Exception("Opss, fila não existe..");
        }

        public void InsertQueue()
        {
            try
            {
                _queueClient.SendMessage(JsonConvert.SerializeObject(new ObjetoDeTeste
                {
                    Cor = GetCor(),
                    Quantidade = new Random().Next(1, 100),
                    Valor = GetDecimal()
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ObjetoDeTeste> ReadQueue()
        {
            try
            {
                IList<ObjetoDeTeste> list = new List<ObjetoDeTeste>();
                PeekedMessage[] peekedMessages = _queueClient.PeekMessages(30);
                foreach (PeekedMessage message in peekedMessages)
                {
                    if (message.MessageText.Contains("{") && message.MessageText.Contains("}") && message.MessageText.Contains("\""))
                    {
                        object obj = JsonConvert.DeserializeObject<ObjetoDeTeste>(message.MessageText);
                        list.Add((ObjetoDeTeste)obj);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCor()
        {
            string[] cores = new string[] { "Vermelho", "Verde", "Azul", "Preto", "Cinza", "Branco", "Marrom", "Amarelo", "Violeta", "Vinho", "Verde Água", "Marrom Tijolo", "Prata", "Azul Fluorecente", "Rosa Choque", "Lilás" };
            return cores[new Random().Next(0, 15)];
        }

        public decimal GetDecimal()
        {
            return decimal.Parse($"{new Random().Next(100, 999).ToString()}.{new Random().Next(10, 99).ToString()}");
        }
    }
}
