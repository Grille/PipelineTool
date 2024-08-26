using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.Text;

[PipelineTask("Text/Section")]
internal class StringSection : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Value", "", "value");
        Parameters.Def(ParameterTypes.Integer, "Offset", "", "0");
        Parameters.Def(ParameterTypes.Integer, "Length", "", "10");
        Parameters.DefResult("Result");
    }

    protected override void OnExecute()
    {
        string var = EvalParameter("Result");
        string value = EvalParameter("Value");
        int offset = int.Parse( EvalParameter("Offset"));
        int length = int.Parse( EvalParameter("Length"));

        var result = value.Substring(offset, length);

        Runtime.Variables[var] = result;
    }
}
