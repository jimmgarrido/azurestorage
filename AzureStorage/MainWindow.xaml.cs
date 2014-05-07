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
using MahApps.Metro.Controls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace AzureStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private string account, key;
        CloudStorageAccount storageAccount;
         

        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                DisableForm();
                OpenAccount();
            } 
            catch
            {
            }
          

        }

        private void login_Click_1(object sender, RoutedEventArgs e)
        {
            account = accountBox.Text;
            key = keyBox.Text;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add("user", account);
            config.AppSettings.Settings.Add("StorageConnectionString", "DefaultEndpointsProtocol=https;AccountName=jimmygarrido;AccountKey=tPdoyMB5YopNibic+IxnzzzkYZlIhAa3ojS1gCFY/SRuq4NYcGFN6oypH5SjvPdtgUrICA4tFsMF9w9pSRjBJQ==");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            try
            {
                storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                DisableForm();
            }
            catch
            {
            }

            OpenAccount();
        }

        private void OpenAccount()
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            List<CloudBlobContainer> containerNames = new List<CloudBlobContainer>();

            foreach(var container in blobClient.ListContainers())
            {
                containerNames.Add(container);
            }

            containersList.ItemsSource = containerNames;

        }

        private void DisableForm()
        {
            accountBox.Text = ConfigurationManager.AppSettings["user"].ToString();
            accountBox.IsEnabled = false;
            keyBox.IsEnabled = false;
        }
    }
}
