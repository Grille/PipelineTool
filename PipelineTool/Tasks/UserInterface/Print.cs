﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.Tasks.UserInterface;

[PipelineTask("UI/Print")]
internal class Print : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.String, "Text", "", "Message");
    }

    protected override void OnExecute()
    {
        var text = EvalParameter("Text");
        Runtime.Logger.Info(text);
    }
}
