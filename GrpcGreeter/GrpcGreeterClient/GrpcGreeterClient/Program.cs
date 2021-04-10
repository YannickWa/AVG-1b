using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main()
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var boerseClient = new Boerse.BoerseClient(channel);

            var tradeRequested = new TadeSuchenMitNr { id = "1" };

            var trade = await BoerseClient.GetTadeInfoAsync(tradeRequested);

            Console.WriteLine($" ID : {trade.id} \n Name : {trade.name} \n Anzahl : {trade.menge} \n Datum : {artikel.datum}");

            Console.ReadKey();
        }
    }
}
