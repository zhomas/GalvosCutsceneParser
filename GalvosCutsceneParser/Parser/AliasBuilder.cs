using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalvosCutsceneParser.Entities
{
    public class AliasBuilder : IEntitySupplier
    {
        public const string START_ALIAS = "#alias";
        public const string END_ALIAS = "#endalias";
        public List<IEntity> Entities { get; private set; }

        private Func<int, GameObject> goGetter;

        public AliasBuilder(string gplText, Func<int, GameObject> goGetter)
        {
            this.goGetter = goGetter;
            List<IEntity> list = new List<IEntity>();

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
                        CutsceneEntity entity = BuildEntity(line, this.goGetter);
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

        public static CutsceneEntity BuildEntity(string inputLine, Func<int, GameObject> goGetter)
        {
            try
            {
                int equalsPos = inputLine.IndexOf("=");

                string lhs = inputLine.Substring(0, equalsPos).Trim();
                string rhs = inputLine.Substring(equalsPos + 1).Trim();

                Debug.LogError("Getting Cutscene Entity :: " + rhs);
                Debug.LogError("Go Getter : " + goGetter);

                return new CutsceneEntity(lhs, Convert.ToInt16(rhs), goGetter);
            }
            catch (Exception e)
            {
                throw new BadAliasException();
            }
        }

        public IEntity GetEntityByAlias(string alias)
        {
            return this.Entities.Where(e => e.Alias == alias).FirstOrDefault();
        }
    }

    public class NoAliasException : Exception
    {

    }

    public class BadAliasException : Exception
    {

    }
}
