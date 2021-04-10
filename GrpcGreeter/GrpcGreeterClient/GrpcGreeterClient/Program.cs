using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;

namespace GrpcGreeterClient
{
    class Program
    {
        // Trade-Nr. abfragen
        static async void unaryId(int id) 
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var boerseClient = new Boerse.BoerseClient(channel);

            var tradeRequested = new TradeSuchenMitNr { Id = id };
            var uTrade = await boerseClient.GetTradeInfoAsync(tradeRequested);

            if (uTrade.Id == 0)
            {
                Console.WriteLine($"Trade mit der Trade-Nr. {id} konnte nicht gefunden werden! \n");
            }
            else
            {
                Console.WriteLine($" ID : {uTrade.Id} \n Name : {uTrade.Name} \n Anzahl : {uTrade.Menge} \n Datum : {uTrade.Datum} \n");
            }
        }

        // Trades der nächsten x Minuten anfragen
        static async void stream(int duration)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var boerseClient = new Boerse.BoerseClient(channel);                      

            var streamingCall = boerseClient.GetNextTrades(new Time { Min = duration });

            await foreach (var trade in streamingCall.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($" ID : {trade.Id} \n Name : {trade.Name} \n Anzahl : {trade.Menge} \n Datum : {trade.Datum} \n");
                Console.Beep(262,500);
            }
        }

        static void Main()
        {
            unaryId(3); // Trade-Nr. 3
            unaryId(1); // Trade-Nr. 1
            unaryId(4); // Trade-Nr. 4
            stream(2); // Trades der nächsten 2 Minuten

            Console.ReadKey();
        }
    }
}
