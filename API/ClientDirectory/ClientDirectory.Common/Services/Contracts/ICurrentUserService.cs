using System;
using System.Collections.Generic;
using System.Text;

namespace ClientDirectory.Common.Services.Contracts
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        bool IsAuthenticated { get; }
    }
}
