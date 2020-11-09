// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Luis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace AddLuis
{
    public class LuisSample : ActivityHandler
    {
        private LuisTaskRecognizer _luisHelper;
        public LuisSample(LuisTaskRecognizer luisHelper)
        {
            _luisHelper = luisHelper;
        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to Bot!"), cancellationToken);
                }
            }
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var result = await _luisHelper.RecognizeAsync<IncidentLuis>(turnContext, cancellationToken);
            var topIntent = result.TopIntent().intent;
            switch (topIntent)
            {
                case IncidentLuis.Intent.Create_Incident:
                    break;
                case IncidentLuis.Intent.None:
                    break;
                   
            }
        }
    }
}
