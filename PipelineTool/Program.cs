using Grille.PipelineTool.WinForms.Tree;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Grille.PipelineTool;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        AssemblyTaskTypeTree.Initialize();
        ApplicationConfiguration.Initialize();
        Application.Run(new PipelineToolForm());
    }
}