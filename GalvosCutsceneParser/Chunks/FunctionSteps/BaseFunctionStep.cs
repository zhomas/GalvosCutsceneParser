using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GalvosCutsceneParser
{
    public abstract class BaseFunctionStep : BaseStep
    {
        protected abstract string ComponentName { get; }
        protected abstract string FunctionName { get; }
        protected CutsceneEntity owner;

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("isStatic", false.ToString()),
                new XAttribute("waitBetween", true.ToString()),
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override List<XElement> GetStringElements()
        {
            return new List<XElement>()
            {
                new XElement("name", new XCData(this.ComponentName)),
            }.Concat(base.GetStringElements()).ToList();
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

        protected override string GetNodeType()
        {
            return "CallFunctionStep";
        }

        protected override List<XAttribute> GetFloatAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("timeBetween", 0f)
            };
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            return new List<XElement>()
            {
                new XElement("onObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.owner.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", this.GetEmptyCData("childName")),
                    new XElement("objectKey",
                            new XAttribute("type", 0),
                        new XElement("_string", this.GetEmptyCData("value")))
                ),
                new XElement("method",
                    new XElement("_string", 
                        new XElement("functionName", new XCData(this.FunctionName))),
                    new XElement("_subarrays", 
                        new XElement("parameter", ""))
                )
            };
        }
    }
}
