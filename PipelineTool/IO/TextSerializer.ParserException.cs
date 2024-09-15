using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grille.PipelineTool.IO;

public static partial class TextSerializer
{
    public class ParserException : Exception
    {
        public ParserException(Exception exception, Result result) : base($"Error at line {result.LineNr}", exception) { }
    }
}
