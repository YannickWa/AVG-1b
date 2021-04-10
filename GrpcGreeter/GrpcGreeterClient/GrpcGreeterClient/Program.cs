﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeter;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main()
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var boerseClient = new Boerse.BoerseClient(channel);

            var tradeRequested = new TradeSuchenMitNr { Id = "3" };

            var trade = await boerseClient.GetTradeInfoAsync(tradeRequested);

            Console.WriteLine($" ID : {trade.Id} \n Name : {trade.Name} \n Anzahl : {trade.Menge} \n Verfügbar ab : {trade.Verfuegbar}");

            Console.ReadKey();
        }
    }
}
