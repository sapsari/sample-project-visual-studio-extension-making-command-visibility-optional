using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Settings;
using Task = System.Threading.Tasks.Task;

namespace YellowNamespace
{
    [DesignerCategory("code")] // to hide designer
    class YellowOptionsPage : DialogPage
    {
        [Category("UI")]
        [DisplayName("Display command")]
        [Description("Display or hide extension shortcut in Tools menu")]
        public bool IsDisplayingYellowCommand { get; set; } = true;
    }
}
