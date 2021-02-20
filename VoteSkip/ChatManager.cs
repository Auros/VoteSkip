using ChatCore;
using ChatCore.Interfaces;
using ChatCore.Services.Twitch;
using System;
using Zenject;

namespace VoteSkip
{
    internal class ChatManager : IInitializable, IDisposable
    {
        private readonly Config _config;
        private readonly TwitchService _twitchService;
        private readonly ChatCoreInstance _chatCoreInstance;
        public event Action<string, IChatChannel> VoteReceived;

        public ChatManager(Config config)
        {
            _config = config;
            _chatCoreInstance = ChatCoreInstance.Create();
            _twitchService = _chatCoreInstance.RunTwitchServices();
        }

        public void Initialize()
        {
            _twitchService.OnTextMessageReceived += ChatMessageReceived;
        }

        private void ChatMessageReceived(IChatService chatService, IChatMessage chatMessage)
        {
            if (chatMessage.Message.ToLower().StartsWith(_config.VoteSkipPrefix))
            {
                VoteReceived?.Invoke(chatMessage.Sender.Id, chatMessage.Channel);
            }
        }

        public void Dispose()
        {
            _twitchService.OnTextMessageReceived -= ChatMessageReceived;
            _chatCoreInstance.StopTwitchServices();
        }

        public void SendMessage(IChatChannel channel, string text)
        {
            _twitchService.SendTextMessage(text, channel);
        }
    }
}
