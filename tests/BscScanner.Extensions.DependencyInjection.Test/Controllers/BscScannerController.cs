using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BscScanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BscScanner.Extensions.DependencyInjection.Test.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class BscScannerController : ControllerBase {
        
        private readonly ILogger<BscScannerController> _logger;
        private readonly IBscScanClient _bscScanClient;

        public BscScannerController(ILogger<BscScannerController> logger, IBscScanClient bscScanClient) {
            _logger = logger;
            _bscScanClient = bscScanClient;
        }

        [HttpGet]
        public async Task<float> GetBalanceFor(string address) {
            var result = await _bscScanClient.GetBnbBalanceSingleAsync(address);

            return result;
        }
    }
}