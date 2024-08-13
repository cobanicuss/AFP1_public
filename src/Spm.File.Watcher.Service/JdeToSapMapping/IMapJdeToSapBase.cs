using System.Collections.Generic;
using Spm.Shared;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapJdeToSapBase<T, out TK> where T : IMarkAsDto
    {
        TK MapPayload(IList<T> input);
    }

    public interface IMapJdeToSapBaseSingle<in T, out TK> where T : IMarkAsDto
    {
        TK MapPayload(T input);
    }
}