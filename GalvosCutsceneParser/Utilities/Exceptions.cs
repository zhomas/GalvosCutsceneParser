using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class MisformedStepException : Exception
    {
        public MisformedStepException(string message)
        : base(message)
        {
            // This will work, because Exceptions defines a constructor accepting a string.
        }
    }
}
