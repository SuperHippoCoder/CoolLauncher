using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Services
{
    public class NaturalVersionComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null || y == null) return 0;

            var xParts = System.Text.RegularExpressions.Regex.Split(x, @"(\d+)");
            var yParts = System.Text.RegularExpressions.Regex.Split(y, @"(\d+)");

            for (int i = 0; i < Math.Min(xParts.Length, yParts.Length); i++)
            {
                if (int.TryParse(xParts[i], out int xNum) && int.TryParse(yParts[i], out int yNum))
                {
                    int cmp = xNum.CompareTo(yNum);
                    if (cmp != 0) return cmp;
                }
                else
                {
                    int cmp = string.Compare(xParts[i], yParts[i], StringComparison.Ordinal);
                    if (cmp != 0) return cmp;
                }
            }

            return xParts.Length.CompareTo(yParts.Length);
        }
    }

    internal class LauncherService
    {
        private readonly MinecraftLauncher _launcher;

        public event Action<int> ProgressChanged;

        public LauncherService()
        {
            _launcher = new MinecraftLauncher();
            _launcher.ByteProgressChanged += (sender, args) =>
            {
                if (args.TotalBytes > 0)
                {
                    int percent = (int)((args.ProgressedBytes * 100) / args.TotalBytes);
                    ProgressChanged?.Invoke(percent);
                }
            };
        }

        public class VersionInfo
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsInstalled { get; set; }
        }

        public async Task<List<VersionInfo>> GetAllVersions()
        {
            var versions = await _launcher.GetAllVersionsAsync();
            var result = new List<VersionInfo>();

            foreach (var version in versions)
            {
                result.Add(new VersionInfo
                {
                    Name = version.Name,
                    Type = version.Type,
                    IsInstalled = IsVersionInstalled(version.Name)
                });
            }

            var comparer = new NaturalVersionComparer();

            result = result
                .OrderByDescending(v => v.IsInstalled)
                .ThenByDescending(v => v.Name, comparer)
                .ToList();

            return result;
        }

        public bool IsVersionInstalled(string versionName)
        {
            string versionJsonPath = Path.Combine(
                _launcher.MinecraftPath.Versions,
                versionName,
                $"{versionName}.json"
            );
            return File.Exists(versionJsonPath);
        }

        public async Task<Process> PlayVersion(string versionName, string username)
        {
            string uuid = Guid.NewGuid().ToString("N");

            var session = new MSession
            {
                Username = username,
                UUID = uuid,
                AccessToken = "offline_token",
                UserType = "Mojang"
            };

            var launchOption = new MLaunchOption
            {
                Session = session,
                MaximumRamMb = 4096
            };

            return await _launcher.InstallAndBuildProcessAsync(versionName, launchOption);
        }
    }
}
