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
                package = new GitSource(spec.Substring(0, index), spec.Substring(index));
                return true;
            }
        }
    }
}
