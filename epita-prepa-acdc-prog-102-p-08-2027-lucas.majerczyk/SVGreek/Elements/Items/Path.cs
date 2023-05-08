using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SVGreek
{
    public class Path : Element
    {
        public List<(char, double[])> Instructions { get; }

        public Path(List<(char, double[])> instructions, Color? strokeColor = null, Color? fillColor = null, double strokeWidth = 1d)
        {
            Instructions = instructions;
            Stroke = strokeColor ?? new Color("none");
            Fill = fillColor ?? new Color("none");
            StrokeWidth = strokeWidth;
        }

        public Path(XElement element)
        {
            Instructions = ParseInstructions(element.Attribute("d")?.Value ?? throw new InvalidAttributeException());
            Stroke = new Color(element.Attribute("stroke")?.Value ?? "none");
            Fill = new Color(element.Attribute("fill")?.Value ?? "none");
            StrokeWidth = double.Parse(element.Attribute("stroke-width")?.Value ?? "1");
        }

        public override XElement ToXElement()
        {
            var element = new XElement("path");
            element.SetAttributeValue("d", InstructionsToString());
            element.SetAttributeValue("stroke", Stroke.ToString());
            element.SetAttributeValue("fill", Fill.ToString());
            element.SetAttributeValue("stroke-width", StrokeWidth);
            if (TransformToString() != "")
            {
                element.SetAttributeValue("transform", TransformToString());
            }
            return element;
        }

        private List<(char, double[])> ParseInstructions(string instructionString)
        {
            var instructions = new List<(char, double[])>();
            var instructionChunks = instructionString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var chunk in instructionChunks)
            {
                var instruction = chunk[0];
                var parameters = chunk[1..].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

                if (parameters.Length == 0 && (instruction != 'Z' && instruction != 'z'))
                {
                    throw new InvalidAttributeException();
                }

                instructions.Add((instruction, parameters));
            }

            return instructions;
        }

        private string InstructionsToString()
        {
            var chunks = Instructions.Select(instruction =>
            {
                var parameters = instruction.Item2.Select(p => p.ToString()).ToArray();
                var parameterString = parameters.Length > 0 ? " " + string.Join(" ", parameters) : "";
                return instruction.Item1 + parameterString;
            });
            return string.Join("", chunks);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Path other))
            {
                return false;
            }

            return Instructions.SequenceEqual(other.Instructions) &&
                   Stroke.Equals(other.Stroke) &&
                   Fill.Equals(other.Fill) &&
                   Math.Abs(StrokeWidth - other.StrokeWidth) < double.Epsilon;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Instructions, Stroke, Fill, StrokeWidth);
        }
    }
}
