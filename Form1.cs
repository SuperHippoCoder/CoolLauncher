using Launcher.Services;
using static Launcher.Services.LauncherService;

namespace Launcher
{
    public partial class Form1 : Form
    {
        private LauncherService _launcherService = null;
        private List<VersionInfo> _allVersions = new List<VersionInfo>();

        public Form1()
        {
            InitializeComponent();
            _launcherService = new LauncherService();
            _launcherService.ProgressChanged += (percent) =>
            {
                this.Invoke(new Action(() =>
                {
                    DownloadBar.Value = percent;
                    DownloadText.Text = $"{percent}%";
                }));
            };
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            VersionCheck.SetItemChecked(0, true);

            _allVersions = await _launcherService.GetAllVersions();
            ApplyFilter();

            ActiveControl = null;
        }

        private void VersionCheck_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() => ApplyFilter()));
        }

        private void ApplyFilter()
        {
            VersionsBox.Items.Clear();

            var allowedTypes = new List<string>();
            if (VersionCheck.CheckedItems.Contains("Release")) allowedTypes.Add("release");
            if (VersionCheck.CheckedItems.Contains("Snapshot")) allowedTypes.Add("snapshot");
            if (VersionCheck.CheckedItems.Contains("Beta")) allowedTypes.Add("old_beta");
            if (VersionCheck.CheckedItems.Contains("Alpha")) allowedTypes.Add("old_alpha");

            if (allowedTypes.Count == 0)
            {
                VersionsBox.Items.Add("Nothing");
                return;
            }

            var filtered = _allVersions
                .Where(v => allowedTypes.Contains(v.Type))
                .OrderByDescending(v => v.IsInstalled);

            foreach (var v in filtered)
            {
                string display = v.IsInstalled ? $"{v.Name} ✅" : v.Name;
                VersionsBox.Items.Add(display);
            }

            if (VersionsBox.Items.Count > 0)
                VersionsBox.SelectedIndex = 0;
        }
        private async void PlayButton_Click(object sender, EventArgs e)
        {
            string cleanName = ((string)VersionsBox.SelectedItem).Replace(" ✅", "");

            if (string.IsNullOrEmpty(cleanName))
            {
                MessageBox.Show("Select Version!");
                return;
            }

            var process = await _launcherService.PlayVersion(cleanName, Nickname.Text);
            process.Start();
        }

        private void VersionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedVersion = VersionsBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedVersion)) return;

            if (_launcherService.IsVersionInstalled(selectedVersion.Replace(" ✅", "")))
            {
                PlayButton.Text = "Play";
            }
            else
            {
                PlayButton.Text = "Download";
            }
        }
    }
}
