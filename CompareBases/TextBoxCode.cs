using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ICSharpCode.AvalonEdit;
using System.Xml;

namespace CompareBases
{
    public partial class TextBoxCode : UserControl
    {
        private ICSharpCode.AvalonEdit.TextEditor TextEditor;
        private ElementHost TextEditorHost;
        public override string Text { get => TextEditor.Text; set => TextEditor.Text = value; }
        public bool ReadOnly { get => TextEditor.IsReadOnly; set => TextEditor.IsReadOnly = value; }
        public new bool Enabled { get => TextEditorHost.Enabled; set => TextEditorHost.Enabled = value; }

        public TextBoxCode()
        {
            InitializeComponent();

            //  http://avalonedit.net/
            //  https://stackoverflow.com/questions/14170165/how-can-i-add-this-wpf-control-into-my-winform
            //  https://github.com/icsharpcode/AvalonEdit/tree/master/ICSharpCode.AvalonEdit/Highlighting/Resources
            TextEditor = new ICSharpCode.AvalonEdit.TextEditor();
            TextEditor.ShowLineNumbers = true;
            TextEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            TextEditor.FontSize = 12.75f;
            var modeFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "TSQL-Mode.xshd"); // "CSharp-Mode.xshd"
            if (File.Exists(modeFile))
            {
                Stream xshd_stream = File.OpenRead(modeFile);
                XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
                // Apply the new syntax highlighting definition.
                TextEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xshd_reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                xshd_reader.Close();
                xshd_stream.Close();
            }
            TextEditorHost = new ElementHost();
            TextEditorHost.Size = new Size(400, 400);
            TextEditorHost.Location = new Point(0, 0);
            TextEditorHost.Dock = DockStyle.Fill;
            TextEditorHost.Child = TextEditor;
            this.Controls.Add(TextEditorHost);
            
        }
    }
}
