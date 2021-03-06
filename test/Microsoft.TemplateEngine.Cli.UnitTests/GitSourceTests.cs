﻿using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Xunit;

namespace Microsoft.TemplateEngine.Cli.UnitTests
{
    public class GitSourceTests : TestBase
    {
        [Theory(DisplayName = nameof(GitUrlReturnsGitPackageWithUrl))]
        [InlineData("https://github.com/acme/templates.git/sub/folder", "https://github.com/acme/templates.git")]
        [InlineData("https://github.com/acme/templates.git", "https://github.com/acme/templates.git")]
        [InlineData("https://github.com/acme/templates.GIT", "https://github.com/acme/templates.GIT")]
        [InlineData("git@github.com:sayedihashimi/dotnet-new-samples.git", "git@github.com:sayedihashimi/dotnet-new-samples.git")]
        public void GitUrlReturnsGitPackageWithUrl(string request, string gitUrl)
        {
            GitSource source = null;
            bool result = GitSource.TryParseGitSource(request, out source);

            Assert.NotNull(source);
            Assert.Equal(gitUrl, source.GitUrl);
            Assert.Equal(true, result);
        }
        [Theory(DisplayName = nameof(GitUrlReturnsGitPackageWithSubFolder))]
        [InlineData("https://github.com/acme/templates.git/sub/folder", "sub/folder")]
        [InlineData("https://github.com/acme/templates.git", "")]
        public void GitUrlReturnsGitPackageWithSubFolder(string request, string subFolder)
        {
            GitSource source = null;
            bool result = GitSource.TryParseGitSource(request, out source);

            Assert.Equal(subFolder, source.SubFolder);
            Assert.Equal(true, result);
        }
        [Theory(DisplayName = nameof(GitUrlReturnsGitPackageWithRepoName))]
        [InlineData("https://github.com/acme/templates.git/sub/folder", "templates")]
        [InlineData("git@github.com:sayedihashimi/dotnet-new-samples.git", "dotnet-new-samples")]
        public void GitUrlReturnsGitPackageWithRepoName(string request, string repoName)
        {
            GitSource source = null;
            bool result = GitSource.TryParseGitSource(request, out source);

            Assert.NotNull(source);
            Assert.Equal(repoName, source.RepositoryName);
            Assert.Equal(true, result);
        }
        [Theory(DisplayName = nameof(NonGitUrlReturnsFalseNull))]
        [InlineData("C:\\temp\\MyTemplate")]
        [InlineData("Nuget::1")]
        [InlineData(null)]
        [InlineData("not_a_url/templates.GIT")]
        [InlineData("")]
        public void NonGitUrlReturnsFalseNull(string request)
        {
            GitSource source = null;
            bool result = GitSource.TryParseGitSource(request, out source);

            //IReadOnlyList<string> projFilesFound = actionProcessor.FindProjFileAtOrAbovePath(EngineEnvironmentSettings.Host.FileSystem, outputBasePath, new HashSet<string>());
            Assert.Equal(null, source);
            Assert.Equal(false, result);
        }
    }
}
