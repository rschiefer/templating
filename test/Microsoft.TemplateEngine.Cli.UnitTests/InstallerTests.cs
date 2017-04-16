using Microsoft.TemplateEngine.Core.UnitTests;
using Microsoft.TemplateEngine.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Microsoft.TemplateEngine.Cli.UnitTests
{
    public class InstallerTests : TestBase
    {
        [Fact(DisplayName = nameof(GitTemplatePathCallsGitClone))]
        public void GitTemplatePathCallsGitClone()
        {
            //string targetBasePath = FileSystemHelpers.GetNewVirtualizedPath(EngineEnvironmentSettings);
            //string projFileFullPath = Path.Combine(targetBasePath, "MyApp.proj");
            //EngineEnvironmentSettings.Host.FileSystem.WriteAllText(projFileFullPath, TestCsprojFile);
            
            IInstaller installer = new Installer(this.EnvironmentSettings);

            var installationRequests = new [] { "https://github.com/sayedihashimi/dotnet-new-samples.git/" };
            installer.InstallPackages(installationRequests);

            //IReadOnlyList<string> projFilesFound = actionProcessor.FindProjFileAtOrAbovePath(EngineEnvironmentSettings.Host.FileSystem, outputBasePath, new HashSet<string>());
            //Assert.Equal(1, projFilesFound.Count);
        }
    }
}
