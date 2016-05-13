using System;

namespace Tsc.Domain
{
    /// <summary>
    /// Declares (enshures) that the implementation should have an Id.
    /// </summary>
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}