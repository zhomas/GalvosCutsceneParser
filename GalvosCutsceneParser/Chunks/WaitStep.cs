using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public class WaitStep : BaseStep
    {
        private int milliseconds;

        public WaitStep(int milliseconds)
        {
            this.milliseconds = milliseconds;
        }

        public float Seconds
        {
            get
            {
                return ((float)this.milliseconds / 1000);
            }
        }

        public static int ParseMillisecondsFromInputLine(string inputLine)
        {
            if (inputLine.Contains("wait"))
            {
                return RegexUtilities.GetDigitsFromString(inputLine);
            }

            return 0;
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("random", false.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override string GetNodeType()
        {
            return "WaitStep";
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var objectID = new XElement("objectID");
            objectID.Add(new XCData(""));

            var name = new XElement("name");
            name.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("time", 
                        new XAttribute("type", 0), 
                        new XAttribute("origin", 1), 
                        new XAttribute("multiVariableUseType", 0),
                        new XAttribute("formulaID", 0),
                        new XAttribute("rounding", 0),
                    new XElement("_float", 
                        new XAttribute("initialValue", 0),
                        new XAttribute("offset", 0),
                        new XAttribute("value", this.Seconds),
                        new XAttribute("value2", 0)),
                    new XElement("_bool", 
                        new XAttribute("useObject", true.ToString()),
                        new XAttribute("isInt", false.ToString()),
                        new XAttribute("isUTC", false.ToString())),
                    new XElement("_string", 
                        objectID,
                        name
                    )
                )
            };
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }
    }
}
