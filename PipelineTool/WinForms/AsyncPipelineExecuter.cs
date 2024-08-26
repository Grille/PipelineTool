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

    public AsyncPipelineExecuter()
    {
        Runtime = new Runtime();

        _task = Task.CompletedTask;
    }

    public void Execute(Pipeline pipeline, Form form)
    {
        if (Running)
        {
            throw new InvalidOperationException();
        }

        Running = true;
        Runtime.Clear();
        Runtime.UserInterface.ParentForm = form;

        _task = Task.Run(() =>
        {
            try
            {
                Runtime.Call(pipeline);
            }
            catch (Exception ex)
            {
                string stackTrace = Runtime.StackTrace;
                string message = $"{ex.Message}\n\n{stackTrace}";
                Runtime.Logger.Error($"<{ex.GetType().Name}> {message}");
                MessageBox.Show(message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Running = false;

                ExecutionDone?.Invoke(this, EventArgs.Empty);
            }
        });

    }

    public void Cancel()
    {
        Runtime.Cancel();
    }
}
