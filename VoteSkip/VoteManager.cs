using ChatCore.Interfaces;
using System;
using System.Collections.Generic;
using Zenject;

namespace VoteSkip
{
    internal class VoteManager : IInitializable, IDisposable
    {
        private readonly Config _config;
        private readonly ChatManager _chatManager;
        private readonly PauseController _pauseController;
        private readonly HashSet<string> _alreadyVotedUsers;

        private int _votes = 0;
        private bool _insuranceLock = false;

        public VoteManager(Config config, ChatManager chatManager, PauseController pauseController)
        {
            _config = config;
            _chatManager = chatManager;
            _pauseController = pauseController;
            _alreadyVotedUsers = new HashSet<string>();
        }

        public void Initialize()
        {
            _chatManager.VoteReceived += VoteReceived;
        }

        private void VoteReceived(string userID, IChatChannel channel)
        {
            if (!_alreadyVotedUsers.Contains(userID))
            {
                _votes++;
                _alreadyVotedUsers.Add(userID);

                if (_votes >= _config.MinVotesForVoteSkip && !_insuranceLock)
                {
                    _insuranceLock = true;
                    _chatManager.SendMessage(channel, "Too many people did not like this song. Sadge");
                    _pauseController.HandleMenuButtonTriggered();
                }
            }
        }

        public void Dispose()
        {
            _chatManager.VoteReceived -= VoteReceived;
        }
    }
}