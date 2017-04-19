using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Core.UnitTests;
using Microsoft.TemplateEngine.Utils;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Xunit;

namespace Microsoft.TemplateEngine.Cli.UnitTests
{
    public class InstallerTests : TestBase
    {
        [Theory(DisplayName = nameof(GitTemplatePathCallsGitClone))]
        [InlineData("https://github.com/acme/templates.git/sub/folder", "https://github.com/acme/templates.git")]
        [InlineData("https://github.com/acme/templates.git", "https://github.com/acme/templates.git")]
        public void GitTemplatePathCallsGitClone(string request, string gitUrl)
        {
            //string targetBasePath = FileSystemHelpers.GetNewVirtualizedPath(EngineEnvironmentSettings);
            //string projFileFullPath = Path.Combine(targetBasePath, "MyApp.proj");
            //EngineEnvironmentSettings.Host.FileSystem.WriteAllText(projFileFullPath, TestCsprojFile);

            InstallerTestWrapper installer = new InstallerTestWrapper(this.EnvironmentSettings);

            var installationRequests = new [] { request };
            installer.InstallPackages(installationRequests);

            //IReadOnlyList<string> projFilesFound = actionProcessor.FindProjFileAtOrAbovePath(EngineEnvironmentSettings.Host.FileSystem, outputBasePath, new HashSet<string>());
            Assert.Equal("git", installer.ExecuteProcessCommands[0][0]);
            Assert.Equal("clone", installer.ExecuteProcessCommands[0][1]);
            Assert.Equal(gitUrl, installer.ExecuteProcessCommands[0][2]);
            Assert.Contains("scratch", installer.ExecuteProcessCommands[0][3]);
        }
    }

    internal class InstallerTestWrapper : Installer
    {
        public InstallerTestWrapper(IEngineEnvironmentSettings environmentSettings) : base(environmentSettings)
        {
            ExecuteProcessCommands = new List<string[]>();
        }

        public List<string[]> ExecuteProcessCommands { get; set; }
        public bool ExecuteProcessReturn { get; set; }

        internal override bool ExecuteProcess(string command, params string[] args)
        {
            ExecuteProcessCommands.Add(new string[] { command }.Concat(args).ToArray());
            //base.ExecuteProcess(command, args);
            return ExecuteProcessReturn;
        }
    }
}
