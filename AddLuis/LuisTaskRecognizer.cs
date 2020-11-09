using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AddLuis
{
    public class LuisTaskRecognizer : IRecognizer
    {
        private readonly Microsoft.Bot.Builder.AI.Luis.LuisRecognizer _luisRecognizer;
        public LuisTaskRecognizer()
        {
            var service = new LuisService()
            {
                AppId = "5e7068ca-eaa8-42f8-b6b0-f603b070c5ee",
                SubscriptionKey = "2760aa3227d8414f9e948ac5eb1849fb",
                Region = "westus",
                Version = "2.0",
              

            };
            var app = new LuisApplication(service);
            var regOptions = new LuisRecognizerOptionsV2(app)
            {
                IncludeAPIResults = true,
                PredictionOptions = new LuisPredictionOptions()
                {
                    IncludeAllIntents = true,
                    IncludeInstanceData = true
                }
            };
            _luisRecognizer = new Microsoft.Bot.Builder.AI.Luis.LuisRecognizer(regOptions);
        }
        public async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            return await _luisRecognizer.RecognizeAsync(turnContext, cancellationToken);
        }

        public async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken) where T : IRecognizerConvert, new()
        {
            return await _luisRecognizer.RecognizeAsync<T>(turnContext, cancellationToken);
        }
    }
}
