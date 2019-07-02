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

        // This method is called each time an option value is changed
        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            // Retrieve the command and change its visibility
            OleMenuCommandService commandService = this.GetService(typeof(System.ComponentModel.Design.IMenuCommandService)) as OleMenuCommandService;
            var yellowCommand = commandService.FindCommand(new System.ComponentModel.Design.CommandID(YellowCommand.CommandSet, YellowCommand.CommandId));
            yellowCommand.Visible = IsDisplayingYellowCommand;
        }
    }
}
