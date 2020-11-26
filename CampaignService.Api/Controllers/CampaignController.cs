using CampaignService.Common.Models;
using CampaignService.Logging;
using CampaignService.Logging.CampaignService.Logging;
using CampaignService.Services.FilterServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : Controller
    {
        //private readonly ILogger<CampaignController> logger;
        private readonly IFilterService filterService;
        private readonly ILoggerManager loggerManager;

        public CampaignController(/*ILogger<CampaignController> logger,*/
            IFilterService filterService, ILoggerManager loggerManager)
        {
            //this.logger = logger;
            this.filterService = filterService;
            this.loggerManager = loggerManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            CampaignRequest campaignRequest = new CampaignRequest();
            LogRequestModel logRequestModel  = new LogRequestModel()
            {
                Message = "generic",
                EntityId = 1,
                EntityType = "campaign",
                ProcessBy = 15
            };
            loggerManager.LogInfo(logRequestModel);
            //var loggerdeneme = LogManager.GetCurrentClassLogger();
            //LogEventInfo logEvent = new LogEventInfo(NLog.LogLevel.Info, "", "hadi bakalım");
            //logEvent.Properties = new List<KeyValuePair<string, object>>();
            //logEvent.Properties.Add(new KeyValuePair<object, object>("EntityType", "value"));
            //logger.LogInformation("hadi bakalım {EntityType}", "güven sıfır");
            ////logEvent.Context.Add("EntityType", "value");           
            ////logger.LogInformation(logEvent);
            //logger.Log(Microsoft.Extensions.Logging.LogLevel.Information,
            // default(EventId),
            // new MyLogEvent2("hadi bakalım").WithProperty("EntityType", "value"),
            // (Exception)null,
            // MyLogEvent2.Formatter);

            var filteredCampaigns = await filterService.FilteredCampaigns(campaignRequest);

            return Ok(filteredCampaigns);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class MyLogEvent2 : Microsoft.Extensions.Logging.ILogger, IReadOnlyList<KeyValuePair<string, object>>
        {
            readonly string _format;
            readonly object[] _parameters;
            IReadOnlyList<KeyValuePair<string, object>> _logValues;
            List<KeyValuePair<string, object>> _extraProperties;

            public MyLogEvent2(string format, params object[] values)
            {
                _format = format;
                _parameters = values;
            }

            public MyLogEvent2 WithProperty(string name, object value)
            {
                var properties = _extraProperties ?? (_extraProperties = new List<KeyValuePair<string, object>>());
                properties.Add(new KeyValuePair<string, object>(name, value));
                return this;
            }

            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                if (MessagePropertyCount == 0)
                {
                    if (ExtraPropertyCount > 0)
                        return _extraProperties.GetEnumerator();
                    else
                        return System.Linq.Enumerable.Empty<KeyValuePair<string, object>>().GetEnumerator();
                }
                else
                {
                    if (ExtraPropertyCount > 0)
                        return System.Linq.Enumerable.Concat(_extraProperties, LogValues).GetEnumerator();
                    else
                        return LogValues.GetEnumerator();
                }
            }

            public KeyValuePair<string, object> this[int index]
            {
                get
                {
                    int extraCount = ExtraPropertyCount;
                    if (index < extraCount)
                    {
                        return _extraProperties[index];
                    }
                    else
                    {
                        return LogValues[index - extraCount];
                    }
                }
            }

            public int Count => MessagePropertyCount + ExtraPropertyCount;

            public override string ToString() => LogValues.ToString();

            private IReadOnlyList<KeyValuePair<string, object>> LogValues
            {
                get
                {
                    if (_logValues == null)
                        Microsoft.Extensions.Logging.LoggerExtensions.LogDebug(this, _format, _parameters);
                    return _logValues;
                }
            }

            private int ExtraPropertyCount => _extraProperties?.Count ?? 0;

            private int MessagePropertyCount
            {
                get
                {
                    if (LogValues.Count > 1 && !string.IsNullOrEmpty(LogValues[0].Key) && !char.IsDigit(LogValues[0].Key[0]))
                        return LogValues.Count;
                    else
                        return 0;
                }
            }

            void Microsoft.Extensions.Logging.ILogger.Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                _logValues = state as IReadOnlyList<KeyValuePair<string, object>> ?? Array.Empty<KeyValuePair<string, object>>();
            }

            bool Microsoft.Extensions.Logging.ILogger.IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
            {
                return true;
            }

            IDisposable Microsoft.Extensions.Logging.ILogger.BeginScope<TState>(TState state)
            {
                throw new NotSupportedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public static Func<MyLogEvent2, Exception, string> Formatter { get; } = (l, e) => l.LogValues.ToString();
        }
    }
}

