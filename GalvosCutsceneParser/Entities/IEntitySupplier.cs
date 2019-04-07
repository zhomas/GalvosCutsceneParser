using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser.Entities
{
    public interface IEntitySupplier
    {
        List<IEntity> Entities { get; }
        IEntity GetEntityByAlias(string alias);
    }
}
