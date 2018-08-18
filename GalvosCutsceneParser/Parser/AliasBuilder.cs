using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GalvosCutsceneParser
{
    public class AliasBuilder
    {
        public const string START_ALIAS = "#alias";
        public const string END_ALIAS = "#endalias";

        public List<BaseEntity> GetEntitiesFromAliasText(string aliasText)
        {
            List<BaseEntity> list = new List<BaseEntity>();

            using (StringReader reader = new StringReader(aliasText))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        BaseEntity entity = this.BuildEntity(line);
                        list.Add(entity);
                    }
                }
                while (line != null);
            }

            return list;
        }

        public string GetAliasSectionOfInputText(string input)
        {
            int startIndex = input.IndexOf(START_ALIAS) + START_ALIAS.Length;
            int endIndex = input.IndexOf(END_ALIAS);
            return input.Substring(startIndex, endIndex - startIndex).Trim();
        }

        public BaseEntity BuildEntity(string inputLine)
        {
            int equalsPos = inputLine.IndexOf("=");

            if (equalsPos == -1)
            {
                throw new Exception("Failed to build entity - no equals sign detected");
            }



            string lhs = inputLine.Substring(0, equalsPos).Trim();
            string rhs = inputLine.Substring(equalsPos + 1).Trim();

            return new Humanoid();

            return new Humanoid();
        }
    }
}
