using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using Grille.PipelineTool.IO;

namespace Grille.PipelineTool.WinForms;

public class AsyncPipelineExecuter
{
    public Runtime Runtime { get; }

    Task _task;

    public bool Running { get; private set; }

    public event EventHandler? ExecutionDone;

    public AsyncPipelineExecuter(ILogger logger)
    {
        Runtime = new Runtime(logger);

        _task = Task.CompletedTask;
    }

    public void Execute(Pipeline pipeline)
    {
        if (Running)
        {
            throw new InvalidOperationException();
        }

        Running = true;
        Runtime.Clear();

        _task = Task.Run(() =>
        {
            try
            {
                Runtime.Call(pipeline);
            }
            catch (Exception ex)
            {
                Running = false;

                string stackTrace = Runtime.StackTrace;
                string message = $"{ex.Message}\n\n{stackTrace}";
                Runtime.Logger.Error($"<{ex.GetType().Name}> {message}");
                MessageBox.Show(message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Running = false;

            ExecutionDone?.Invoke(this, EventArgs.Empty);
        });

    }

    public void Cancel()
    {
        Runtime.Cancel();
    }
}
