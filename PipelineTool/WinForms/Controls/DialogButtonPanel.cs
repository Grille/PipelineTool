using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grille.PipelineTool.WinForms.Controls;
public class DialogButtonPanel : Control
{
    [Browsable(true)]
    public event EventHandler? Button1Click;

    [Browsable(true)]
    public event EventHandler? Button2Click;

    [Browsable(true)]
    public event EventHandler? Button3Click;

    [Browsable(false)]
    public FlowLayoutPanel Panel { get; }

    [Browsable(false)]
    public Button Button1 { get; }

    [Browsable(false)]
    public Button Button2 { get; }

    [Browsable(false)]
    public Button Button3 { get; }

    [Browsable(true)]
    public string Button1Text { get => Button1.Text; set => Button1.Text = value; }

    [Browsable(true)]
    public string Button2Text { get => Button2.Text; set => Button2.Text = value; }

    [Browsable(true)]
    public string Button3Text { get => Button3.Text; set => Button3.Text = value; }

    [Browsable(true)]
    public bool Button1Visible { get => Button1.Visible; set => Button1.Visible = value; }

    [Browsable(true)]
    public bool Button2Visible { get => Button2.Visible; set => Button2.Visible = value; }

    [Browsable(true)]
    public bool Button3Visible { get => Button3.Visible; set => Button3.Visible = value; }

    public DialogButtonPanel()
    {
        Margin = new Padding(0);

        Panel = new FlowLayoutPanel();

        Panel.Dock = DockStyle.Fill;
        Panel.FlowDirection = FlowDirection.RightToLeft;

        Panel.Margin = new Padding(0);
        Panel.Padding = new Padding(6, 12, 6, 12);

        Button1 = CreateButton(1);
        Button2 = CreateButton(2);
        Button3 = CreateButton(3);

        Button1.Click += (sender, e) => Button1Click?.Invoke(sender, e);
        Button2.Click += (sender, e) => Button2Click?.Invoke(sender, e);
        Button3.Click += (sender, e) => Button3Click?.Invoke(sender, e);

        Panel.Controls.Add(Button1);
        Panel.Controls.Add(Button2);
        Panel.Controls.Add(Button3);

        Controls.Add(Panel);
    }

    private Button CreateButton(int id) => new Button()
    {
        Text = "Button_" + id,
        Margin = new Padding(6, 0, 0, 0),
    };

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (!disposing)
        {
            return;
        }

        Panel.Dispose();
        Button1.Dispose();
        Button2.Dispose();
    }
}
