using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using PaperDownloader.Servers.Versions;

namespace PaperDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Project ProjectType { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ComboBoxProject.Items.Add(Project.Paper);
            ComboBoxProject.Items.Add(Project.Waterfall);
            ComboBoxProject.Items.Add(Project.Travertine);
            ComboBoxProject.SelectedItem = ComboBoxProject.Items[0];
            Build = "latest";
        }

        private void ComboBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxVersions.Items.Clear();

            PopulateVersion();


            if (CheckBoxManual.IsChecked == true)
            {
                PopulateBuilds();
            }
        }
        private void ComboBoxVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckBoxManual.IsChecked == true)
            {
                PopulateBuilds();
            }
        }

        private void CheckBoxIsLatest_OnChecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxManual.IsChecked == true)
            {
                CheckBoxManual.Visibility = Visibility.Hidden;
                PopulateBuilds();
            }
        }
        private void ButtonDownloadJar_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxManual.IsChecked == false)
            {
                Build = "latest";
            }

            Projects.DownloadChangeEvent += UpdateDownload;
            Projects.DownloadCompleted += DownloadCompleted;
            Projects.DownloadJar(ProjectType, Version, Build);
        }


        private void DownloadCompleted()
        {
            Projects.DownloadChangeEvent -= UpdateDownload;
            Projects.DownloadCompleted -= DownloadCompleted;
            MessageBox.Show("Your jar has been downloaded.", "Success!",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
            ProgressBarDownload.Value = 0;

        }

        private void UpdateDownload(int progress)
        {
            ProgressBarDownload.Value = progress;
        }

        private void PopulateVersion()
        {
            ProjectType = (Project)ComboBoxProject.SelectedItem;
            foreach (var stringVersion in Projects.GetVersions(ProjectType))
            {
                ComboBoxVersions.Items.Add(stringVersion);
            }

            Version = ComboBoxVersions.Items[0].ToString();
            ComboBoxVersions.SelectedItem = ComboBoxVersions.Items[0];
        }

        private void PopulateBuilds()
        {
            ComboBoxBuilds.Items.Clear();
            Version = ComboBoxVersions.Text;
            string[] builds = Projects.GetBuild(ProjectType, Version);
            if (builds == null)
            {
                ComboBoxBuilds.Items.Add("No builds found");
                return;
            }

            ComboBoxBuilds.Items.Add("latest");
            foreach (var build in builds)
            {
                ComboBoxBuilds.Items.Add(build);
            }

            Build = ComboBoxBuilds.Items[1].ToString();
            ComboBoxBuilds.SelectedItem = ComboBoxBuilds.Items[1];
        }

        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Projects.DownloadPath = dialog.FileName;
                TextBoxPath.Text = dialog.FileName;
            }
        }

        private void ComboBoxBuilds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxBuilds.SelectedItem is string && ComboBoxBuilds.SelectedItem.Equals("latest"))
            {
                CheckBoxManual.Visibility = Visibility.Visible;
                CheckBoxManual.IsChecked = false;
                Build = ComboBoxBuilds.Items[1].ToString();
                ComboBoxBuilds.SelectedItem = ComboBoxBuilds.Items[1];
            }
        }
    }
}
