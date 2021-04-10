using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter.Services
{
    public class BoerseService : Boerse.BoerseBase
    {
        private readonly ILogger<BoerseService> _logger;
        public BoerseService(ILogger<BoerseService> logger)
        {
            _logger = logger;
        }

        public override Task<TradeInfo> GetTradeNr(TradeSuchenMitNr request, ServerCallContext context)
        {
            TradeInfo output = new TradeInfo();
            if (request.Id == "1")
            {
                output.Id = "1";
                output.Name = "Rast&Ruh";
                output.Menge = 1000;
                output.Datum = "2021, 01, 01, 07, 35, 00";
            }
            else if (request.Id == "2")
            {
                output.Id = "2";
                output.Name = "BenzaAG";
                output.Menge = 420;
                output.Datum = "2021, 01, 01, 07, 40, 00";
            }
            else if (request.Id == "3")
            {
                output.Id = "3";
                output.Name = "GmbH&KoKAG";
                output.Menge = 300;
                output.Datum = "2021, 01, 01, 08, 01, 00";
            }

            return Task.FromResult(output);
        }
    }
}
