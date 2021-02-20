using IPA.Config.Stores;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace VoteSkip
{
    internal class Config
    {
        public bool Enabled { get; set; } = false;
        public int MinVotesForVoteSkip { get; set; } = 10;
        public string VoteSkipPrefix { get; set; } = "!voteskip";
    }
}