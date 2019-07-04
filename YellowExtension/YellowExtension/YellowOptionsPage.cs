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
        /// <summary>
        /// Where extension options are stored in the registry, can be get from base property SharedSettingsStorePath
        /// </summary>
        const string registryCollectionPath = @"ApplicationPrivateSettings\YellowNamespace\YellowOptionsPage";
        const string propertyName = nameof(IsDisplayingYellowCommand) + "Raw";

        /// <summary>
        /// Full path into registry for boolean value of IsDisplayingYellowCommand, to be consumed by UI context rule
        /// </summary>
        public const string RegistryFullPathToIsDisplayingYellowCommandAsBoolean =
            registryCollectionPath + @"\" + propertyName;
        /// <summary>
        /// Full path into registry for string value of IsDisplayingYellowCommand, to be consumed by UI context rule
        /// </summary>
        public const string RegistryFullPathToIsDisplayingYellowCommandAsString =
            registryCollectionPath + @"\" + nameof(IsDisplayingYellowCommand);

        [Category("UI")]
        [DisplayName("Display command")]
        [Description("Display or hide extension shortcut in Tools menu")]
        public bool IsDisplayingYellowCommand { get; set; } = true;

        // This method is called each time an option value is changed
        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            // overridden base method is not async, so run our own async method with ThreadHelper
            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await SaveSettingsToStorageAuxAsync();
            });
        }

        async Task SaveSettingsToStorageAuxAsync()
        {
            // Retrieve the command and change its visibility
            OleMenuCommandService commandService = this.GetService(typeof(System.ComponentModel.Design.IMenuCommandService)) as OleMenuCommandService;
            var yellowCommand = commandService.FindCommand(new System.ComponentModel.Design.CommandID(YellowCommand.CommandSet, YellowCommand.CommandId));
            yellowCommand.Visible = IsDisplayingYellowCommand;
            
            // Write custom property to the registry
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            userSettingsStore.SetBoolean(registryCollectionPath, propertyName, IsDisplayingYellowCommand);
        }
    }
}
