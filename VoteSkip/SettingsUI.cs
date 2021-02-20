using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using System;
using Zenject;

namespace VoteSkip
{
    internal class SettingsUI : IInitializable, IDisposable
    {
        private readonly Config _config;

        [UIValue("enabled")]
        protected bool Enabled
        {
            get => _config.Enabled;
            set => _config.Enabled = value;
        }

        [UIValue("min-votes")]
        protected int MinVotes
        {
            get => _config.MinVotesForVoteSkip;
            set => _config.MinVotesForVoteSkip = value;
        }

        public SettingsUI(Config config)
        {
            _config = config;
        }

        public void Initialize()
        {
            BSMLSettings.instance.AddSettingsMenu("VoteSkip", "VoteSkip.settings.bsml", this);
        }

        public void Dispose()
        {
            if (BSMLSettings.instance != null)
                BSMLSettings.instance.RemoveSettingsMenu(this);
        }
    }
}