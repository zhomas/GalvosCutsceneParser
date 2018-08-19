using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public interface IEntitySupplier
    {
        List<CutsceneEntity> Entities { get; }
        CutsceneEntity GetEntityByAlias(string alias);
    }
}
