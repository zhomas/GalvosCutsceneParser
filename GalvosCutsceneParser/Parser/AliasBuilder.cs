using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class AliasBuilder : IEntitySupplier
    {
        public const string START_ALIAS = "#alias";
        public const string END_ALIAS = "#endalias";

        public List<CutsceneEntity> Entities { get; private set; }

        public AliasBuilder(string gplText)
        {
            List<CutsceneEntity> list = new List<CutsceneEntity>();

            string aliasText = string.Empty;

            try
            {
                aliasText = RegexUtilities.GetTextBetween(gplText, START_ALIAS, END_ALIAS);
            }

            catch (Exception e)
            {
                throw new NoAliasException();
            }
            
            using (StringReader reader = new StringReader(aliasText))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        CutsceneEntity entity = BuildEntity(line);
                        list.Add(entity);
                    }
                }
                while (line != null);
            }

            if (list.Count == 0)
            {
                throw new NoAliasException();
            }

            this.Entities = list;
        }

        public static CutsceneEntity BuildEntity(string inputLine)
        {
            try
            {
                int equalsPos = inputLine.IndexOf("=");

                string lhs = inputLine.Substring(0, equalsPos).Trim();
                string rhs = inputLine.Substring(equalsPos + 1).Trim();

                return new CutsceneEntity(lhs, Convert.ToInt16(rhs));
            }
            catch (Exception e)
            {
                throw new BadAliasException();
            }
        }

        public CutsceneEntity GetEntityByAlias(string alias)
        {
            if (this.Entities.Any(e => e.Alias == alias))
            {
                return this.Entities.Where(e => e.Alias == alias).FirstOrDefault();
            }

            throw new Exception("Could not create entity with name :: " + alias);
        }

        public CutsceneEntity GetFirstEntityOnLine(string line)
        {
            var chunks = line.Split(' ');

            if (chunks.Count() > 0)
            {
                return this.GetEntityByAlias(chunks[0]);
            }


            throw new NotImplementedException();
        }
    }

    public class NoAliasException : Exception
    {

    }

    public class BadAliasException : Exception
    {

    }
}
