using Grille.PipelineTool.IO;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms;

public partial class PipelineToolControl : UserControl
{
    public event EventHandler? ItemsChanged;

    public event EventHandler? UnsavedChangesChanged;

    public const string DefaultPath = "pipelines.txt";

    public bool UnsavedChanges { get; private set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FilePath { get; set; } = DefaultPath;

    public PipelineList Pipelines => PipelinesControl.Pipelines;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ILogger Logger {get => PipelinesControl.Logger; set => PipelinesControl.Logger = value; }

    public PipelineToolControl()
    {
        InitializeComponent();

        var executer = PipelinesControl.Executer;
        var piplines = PipelinesControl.Pipelines;

        PipelinesControl.ListBox.Executer = executer;
        TasksControl.ListBox.Executer = executer;

        PipelinesControl.TasksControl = TasksControl;

        PipelinesControl.ItemsChanged += ListBox_ItemsChanged;
        TasksControl.ItemsChanged += ListBox_ItemsChanged;
    }

    private void ListBox_ItemsChanged(object? sender, EventArgs e)
    {
        OnItemsChanged();
    }

    protected void OnItemsChanged()
    {
        ItemsChanged?.Invoke(this, EventArgs.Empty);
        SetUnsavedChanges(true);
    }

    protected void OnUnsavedChangesChanged()
    {
        UnsavedChangesChanged?.Invoke(this, EventArgs.Empty);
    }

    void SetUnsavedChanges(bool value)
    {
        if (UnsavedChanges != value)
        {
            UnsavedChanges = value;
            OnUnsavedChangesChanged();
        }
    }

    public void SaveFile()
    {
        SaveFile(FilePath);
    }

    public void LoadFile()
    {
        LoadFile(FilePath);
    }

    public void SaveFile(string path)
    {
        try
        {
            using var stream = File.Create(path);
            TextSerializer.Serialize(stream, Pipelines);

            SetUnsavedChanges(false);
        }
        catch (Exception ex)
        {
            ExceptionBox.Show(this, ex);
        }
    }

    public void LoadFile(string path)
    {
        try
        {
            using var stream = File.OpenRead(path);
            Pipelines.Clear();
            TextSerializer.Deserialize(stream, Pipelines);

            SetUnsavedChanges(false);
        }
        catch (Exception ex)
        {
            ExceptionBox.Show(this, ex);
        }
    }

    public DialogResult ShowSaveFileDialog()
    {
        using var dialog = new SaveFileDialog();
        var result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            FilePath = dialog.FileName;
            SaveFile();
        }
        return result;
    }

    public DialogResult ShowOpenFileDialog()
    {
        using var dialog = new OpenFileDialog();
        var result = dialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            FilePath = dialog.FileName;
            LoadFile();
            PipelinesControl.InvalidateItems();
        }
        return result;
    }

    public DialogResult ShowSaveExitDialog(bool allowCancel = true)
    {
        if (!UnsavedChanges)
        {
            return DialogResult.OK;
        }
        const string title = "You have unsaved changes";
        const string message = "Do you want to save changes before closing?";
        var buttons = allowCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
        var result = MessageBox.Show(this, message, title, buttons, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SaveFile();
        }
        return result;
    }

    public void ShowHelp()
    {
    }
}
