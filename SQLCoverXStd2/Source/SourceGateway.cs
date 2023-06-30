using System.Collections.Generic;
using SQLCoverXStd2.Objects;

namespace SQLCoverXStd2.Source
{
    public interface SourceGateway
    {
        SqlServerVersion GetVersion();
        IEnumerable<Batch> GetBatches(List<string> objectFilter);
        string GetWarnings();
    }
}