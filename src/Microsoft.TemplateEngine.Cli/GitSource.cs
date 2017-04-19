using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.TemplateEngine.Cli
{
    internal class GitSource
    {
        public string GitUrl { get; set; }
        public string SubFolder { get; set; }

        public GitSource(string gitUrl, string subFolder)
        {
            GitUrl = gitUrl;
            SubFolder = subFolder;
        }

        public static bool TryParseGitSource(string spec, out GitSource package)
        {
            package = null;
            int gitIndex = -1;

            if (string.IsNullOrEmpty(spec) || (gitIndex = spec.IndexOf(".git", StringComparison.OrdinalIgnoreCase)) < 0)
            {
                return false;
            }
            else
            {
                var index = gitIndex + 4;
                var indexOfLastSlashBeforeGit = -1;
                var indexOfSlash = -1;
                while ((indexOfSlash = spec.IndexOf('/', indexOfLastSlashBeforeGit + 1)) < index && indexOfSlash != -1)
                {
                    indexOfLastSlashBeforeGit = indexOfSlash;
                }
                var subFolder = spec.Substring(indexOfLastSlashBeforeGit + 1).Replace(".git", string.Empty);
                package = new GitSource(spec.Substring(0, index), subFolder);
                return true;
            }
        }
    }
}
