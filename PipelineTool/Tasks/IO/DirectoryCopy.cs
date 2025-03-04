﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.Tasks.IO;

[PipelineTask("IO/Directory/Copy")]
internal class DirectoryCopy : PipelineTask
{
    protected override void OnInit()
    {
        Parameters.Def(ParameterTypes.Directory, "Src", "", "SrcDir");
        Parameters.Def(ParameterTypes.Directory, "Dst", "", "DstDir");
    }

    protected override void OnExecute()
    {
        string srcPath = EvalParameter("Src");
        string dstPath = EvalParameter("Dst");

        if (!Directory.Exists(dstPath))
            Directory.CreateDirectory(dstPath);

        foreach (string dirPath in Directory.GetDirectories(srcPath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(srcPath, dstPath));
        }

        foreach (string newPath in Directory.GetFiles(srcPath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(srcPath, dstPath), true);
        }
    }

    public override Token[] ToTokens() => new Token[]
    {
        new(TokenType.Text, "Copy Directory "),
        new(TokenType.Expression, Parameters["Src"]),
        new(TokenType.Text, " to "),
        new(TokenType.Expression, Parameters["Dst"]),
    };
}
