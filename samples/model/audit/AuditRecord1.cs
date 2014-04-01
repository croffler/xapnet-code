using System;
using GigaSpaces.Core.Metadata;

[SpaceClass]
public class AuditRecord1
{
    [SpaceID]
    [SpaceRouting]
    public long? Id;

    public long? TimeStamp { set; get; }
    public String Application { set; get; }
    public String AuditContent { set; get; }
    public String UserName { set; get; }

}
