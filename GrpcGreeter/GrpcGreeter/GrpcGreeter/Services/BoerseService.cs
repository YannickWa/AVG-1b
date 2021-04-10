﻿using System;
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

        public override async Task<TradeInfo> GetTradeInfo(TradeSuchenMitNr request, ServerCallContext context)
        {
            TradeInfo output = new TradeInfo();
            if (request.Id == 1)
            {
                output.Id = 1;
                output.Name = "Rast&Ruh";
                output.Menge = 1000;
                output.Datum = "2021, 01, 01, 07, 35, 00";
            }
            else if (request.Id == 2)
            {
                output.Id = 2;
                output.Name = "BenzaAG";
                output.Menge = 420;
                output.Datum = "2021, 01, 01, 07, 40, 00";
            }
            else if (request.Id == 3)
            {
                output.Id = 3;
                output.Name = "GmbH&KoKAG";
                output.Menge = 300;
                output.Datum = "2021, 01, 01, 08, 01, 00";
            }

            _logger.LogInformation("Send unary Trade");

            return await Task.FromResult(output);
        }

        public override async Task GetPossibleTrades(Time request, IServerStreamWriter<TradeInfo> responseStream, ServerCallContext context)
        {
            var rng = new Random();
            var now = DateTime.Now;

            var end = DateTime.Now.AddMinutes(Convert.ToInt32(request));
            
            while (DateTime.Now < end)
            {
                await Task.Delay(10000);

                var output = new TradeInfo
                {
                    Id = 4,
                    Name = "ABC",
                    Menge = 5,
                    Datum = DateTime.Now.ToString()
                };

                _logger.LogInformation("Send Trade");

                await responseStream.WriteAsync(output);
            }           
        }
    }
}
