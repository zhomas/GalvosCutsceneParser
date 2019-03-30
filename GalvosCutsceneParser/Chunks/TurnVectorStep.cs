﻿using GalvosCutsceneParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace GalvosCutsceneParser
{
    public class TurnVectorStep : BaseStep
    {
        public enum Direction
        {
            North = 0, South = 1, East = 2, West = 3
        }
        
        private Direction dir;

        public static TurnVectorStep CreateFromInputString(IEntity entity, string inputString)
        {
            string[] chunks = inputString.Split(' ');

            if (chunks.Last().ToLower().Contains("north"))
                return new TurnVectorStep(entity, Direction.North);
            if (chunks.Last().ToLower().Contains("south"))
                return new TurnVectorStep(entity, Direction.South);
            if (chunks.Last().ToLower().Contains("east"))
                return new TurnVectorStep(entity, Direction.East);
            if (chunks.Last().ToLower().Contains("west"))
                return new TurnVectorStep(entity, Direction.West);

            throw new MisformedStepException(inputString);
        }

        public Direction LookDirection
        {
            get { return dir; }
        }

        public TurnVectorStep(IEntity entity, Direction dir) : base()
        {
            this.dir = dir;
            this.entity = entity;
        }

        protected override List<XAttribute> GetRootNodeAttributes(int index, bool isFinal)
        {
            return new List<XAttribute>()
            {
                new XAttribute("direction", (int)this.dir)
            }.Concat(base.GetRootNodeAttributes(index, isFinal)).ToList();
        }

        protected override List<XAttribute> GetBooleanAttributes()
        {
            return new List<XAttribute>()
            {
                new XAttribute("active", true.ToString()),
                new XAttribute("overrideNodeName", false.ToString())
            };
        }

        protected override string GetNodeType()
        {
            return "GalvosTurnVector";
        }

        protected override List<XElement> GetStringArrayElements()
        {
            return new List<XElement>();
        }

        protected override List<XElement> GetNodeSpecialElements()
        {
            var emptyChildName = new XElement("childName");
            emptyChildName.Add(new XCData(""));

            var emptyValue = new XElement("value");
            emptyValue.Add(new XCData(""));

            return new List<XElement>()
            {
                new XElement("usedObject",
                        new XAttribute("type", 0),
                        new XAttribute("aID", this.entity.ID),
                        new XAttribute("wID", 0),
                        new XAttribute("pID", 0),
                        new XAttribute("pID2", -1),
                    new XElement("_string", emptyChildName),
                    new XElement("objectKey",
                            new XAttribute("type", 0),
                        new XElement("_string", emptyValue))),
            };
        }
    }
}
