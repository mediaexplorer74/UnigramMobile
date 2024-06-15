using System.Collections.Generic;

namespace Unigram.Converters
{
    public class ChatFilter
    {
        internal string IconName;
        internal string Title;
        internal List<long> PinnedChatIds;
        internal List<long> IncludedChatIds;
        internal List<long> ExcludedChatIds;
    }
}