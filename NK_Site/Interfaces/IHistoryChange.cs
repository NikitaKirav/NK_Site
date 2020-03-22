using NK_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NK_Site.Interfaces
{
    public interface IHistoryChange
    {
        IEnumerable<HistoryChange> AllHistoryChanges { get; }
        HistoryChange GetHistoryChange(int historyChangeId);
        void AddHistoryChange(HistoryChange historyChange);
        DateTime GetLastDateTimeUsingIpAddress(string remoteIpAddress);
    }
}
