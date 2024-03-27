using Microsoft.Win32;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Threading;

namespace Randomizer50
{
    public partial class MainWindow : Window
    {
        private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            btnStart.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Collapsed;
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT files (*.txt)|*.txt";
            openFileDialog.Title = "Please select a file to convert";

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                txtFilepath.Text = filePath;
            }
        }

        private void ShowError(Exception e)
        {
            MessageBox.Show(e.Message, "Randomizer50", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        private async void BtnStart_ClickAsync(object sender, RoutedEventArgs e)
        {
            var slowDown = 50;
            var path = txtFilepath.Text;
            bool isSlowdown = ckbSlowdown.IsChecked ?? false;

            try
            {
                if (System.IO.File.Exists(path))
                {
                    var lines = await System.IO.File.ReadAllLinesAsync(path);
                    var rand = new Random();
                    var lengthOfArray = lines.Length;

                    txtStatus.Text = "Generating..";
                    btnStart.Visibility = Visibility.Collapsed;
                    btnStop.Visibility = Visibility.Visible;

                    isRunning = true;
                    while (isRunning)
                    {
                        var index = rand.Next(0, lengthOfArray);
                        txtEditor.Text = lines[index];
                        await Task.Delay(slowDown);
                    }

                    // Add slowdown effect
                    if (isSlowdown)
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            slowDown += 10;
                            var index = rand.Next(0, lengthOfArray);
                            txtEditor.Text = lines[index];
                            await Task.Delay(slowDown);
                        }
                    }

                    txtStatus.Text = $"Done";
                    btnStart.Visibility = Visibility.Visible;
                    btnStop.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txtStatus.Text = $"File {path} was not found";
                    btnStart.Visibility = Visibility.Visible;
                    btnStop.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception er)
            {
                ShowError(er);
                btnStart.Visibility = Visibility.Visible;
                btnStop.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }
    }
}
