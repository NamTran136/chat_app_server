using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Server_proj1
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, HashSet<string>> conversationSockets = new();

        public async Task JoinConversation(string conversationId)
        {
            if (!conversationSockets.ContainsKey(conversationId))
            {
                conversationSockets[conversationId] = new HashSet<string>();
            }
            conversationSockets[conversationId].Add(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task LeaveConversation(string conversationId)
        {
            if (conversationSockets.ContainsKey(conversationId))
            {
                conversationSockets[conversationId].Remove(Context.ConnectionId);
                if (conversationSockets[conversationId].Count == 0)
                {
                    conversationSockets.Remove(conversationId);
                }
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var conversation in conversationSockets)
            {
                if (conversation.Value.Contains(Context.ConnectionId))
                {
                    conversation.Value.Remove(Context.ConnectionId);
                    if (conversation.Value.Count == 0)
                    {
                        conversationSockets.Remove(conversation.Key);
                    }
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        public Task<int> GetNumberOfMembersInConversation(string conversationId)
        {
            if (conversationSockets.ContainsKey(conversationId))
            {
                return Task.FromResult(conversationSockets[conversationId].Count);
            }
            return Task.FromResult(0);
        }
    }
}
