using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VaultMigration.Config;

namespace VaultMigration.ViewModels
{
    public class MigrationViewModel : ObservableObject
    {
        public List<VaultApplication> Applications { get { return Settings.Applications.ToList(); } }

        private string _tempDownload;
        public string TempDownloadPath
        {
            get
            {
                return _tempDownload ?? Settings.DownloadPath;
            }
            set
            {
                _tempDownload = value;
                OnPropertyChanged("TempDownloadPath");
            }
        }

        private int? _totalCount;
        public int? TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                OnPropertyChanged("TotalCount");
            }
        }

        private int? _downloadProgress;
        public int? DownloadProgress
        {
            get { return _downloadProgress; }
            set { _downloadProgress = value; OnPropertyChanged("DownloadProgress"); }
        }


        private string _currentDownload;
        public string CurrentDownload
        {
            get
            {
                return _currentDownload;
            }
            set
            {
                _currentDownload = value;
                OnPropertyChanged("CurrentDownload");
            }
        }

        private string _currentUpload;
        public string CurrentUpload
        {
            get
            {
                return _currentUpload;
            }
            set
            {
                _currentUpload = value;
                OnPropertyChanged("CurrentUpload");
            }
        }

        private VaultApplication _selectedApplication;


        public VaultApplication SelectedApplication
        {
            get { return _selectedApplication; }
            set
            {
                _selectedApplication = value;
                OnPropertyChanged("SelectedApplication");
            }
        }

        public ICommand MigrateCommand
        {
            get
            {
                return new DelegateCommand(Migrate);
            }
        }

        private void Migrate(Object data)
        {
            if (SelectedApplication == null)
            {
                MessageBox.Show("Please select an application to migrate.");
                return;
            }

            if (string.IsNullOrEmpty(TempDownloadPath))
            {
                MessageBox.Show("Please enter a temperory download path.");
                return;
            }

            BlockingCollection<VaultObject> queue = new BlockingCollection<VaultObject>();


            var producer = Task.Factory.StartNew(() =>
                {

                    var downloadService = new DownloadService(SelectedApplication.DataID,
                        Path.Combine(TempDownloadPath, SelectedApplication.Name))
                    {
                        Queue = queue
                    };
                    downloadService.NodesFetched += (s, eventArgs) =>
                    {
                        TotalCount = eventArgs.NodeCount;
                    };
                    downloadService.CurrentDownloadChanged +=
                        (s, eventArgs) =>
                        {
                            CurrentDownload = eventArgs.CurrentDownload;
                            DownloadProgress = eventArgs.DownloadProgress;
                            OnPropertyChanged("TotalDownloadProgress");

                        };
                    downloadService.Download();
                });

            var consumer = Task.Factory.StartNew(() =>
            {
                UploadService uploadService = new UploadService { Queue = queue };
                uploadService.CurrentUploadChanged += (sende, eventArgs) =>
                {
                    CurrentUpload = eventArgs.CurrentUpload;
                    QueueLength = eventArgs.QueueLength;
                    OnPropertyChanged("QueueLength");                    
                };                
                uploadService.Upload();
            });

            Task.WhenAll(producer, consumer).ContinueWith(t =>
            {
                queue.Dispose();
                SelectedApplication.IsMigrated = true;
                Settings.UpdateApplication(SelectedApplication);
            });

        }

        public int QueueLength { get; private set; }
    }

}
