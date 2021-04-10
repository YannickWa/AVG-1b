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
                output.id = "1";
                output.name = "Rast&Ruh";
                output.menge = 1000;
                output.datum = "2021, 01, 01, 07, 35, 00";
            }
            else if (request.Id == "2")
            {
                output.id = "2";
                output.name = "BenzaAG";
                output.menge = 420;
                output.datum = "2021, 01, 01, 07, 40, 00";
            }
            else if (request.Id == "3")
            {
                output.id = "3";
                output.name = "GmbH&KoKAG";
                output.menge = 300;
                output.datum = "2021, 01, 01, 08, 01, 00";
            }

            return Task.FromResult(output);
        }
    }
}
