using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace TextSearch.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly string _targetFolderKeyValue = "targetFolderToken";

        private List<string> FoundFiles { get; set; }  = new List<string>();

        public MainViewModel()
        {
            Initialize();
        }

        private string _searchString = "";
        public string SearchString
        {
            get { return _searchString; }
            set { Set(ref _searchString, value); }
        }

        private bool _isReadyToSearch = false;
        public bool IsReadyToSearch
        {
            get { return _isReadyToSearch; }
            set { Set(ref _isReadyToSearch, value); }
        }

        private string _targetFolderPath = "";
        public string TargetFolderPath
        {
            get { return _targetFolderPath; }
            set { Set(ref _targetFolderPath, value); }
        }

        private string _statusText= "";
        public string StatusText
        {
            get { return _statusText; }
            set { Set(ref _statusText, value); }
        }

        private StorageFolder _targetFolder;
        public StorageFolder TargetFolder
        {
            get { return _targetFolder; }
            set { Set(ref _targetFolder, value); }
        }

        private async Task Initialize()
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }


        #region PerformTextSearch
        private RelayCommand _startTextSearch;

        /// <summary>
        /// Gets the SetApiKeyCommand.
        /// </summary>
        public RelayCommand StartTextSearch
        {
            get
            {
                return _startTextSearch
                    ?? (_startTextSearch = new RelayCommand(
                    async () =>
                    {
                        fileCount = folderCount = iteration = 0;
                        FoundFiles = await GetFilesInDirectory(TargetFolder);

                        string rootPath = @"D:\utter faves\oldbadthings\websites\atlanta.backpage.com";
                        string searchText = @"zira";

                        Debug.WriteLine($"Total files found: {FoundFiles.Count}");
                        List<string> positiveFiles = new List<string>();
                        //if (string.IsNullOrWhiteSpace(SearchString))
                        //    return;
                        int iteration2 = 0;
                        try
                        {
                            Debug.WriteLine($"Starting Search");
                            foreach (var filePath in FoundFiles)
                            {
                                iteration2++;

                                if (iteration2 % 200 == 0)
                                {
                                    Debug.WriteLine($"Searched {iteration2} files with {positiveFiles.Count} results");
                                }
                                // Optional: Check the file extension if you want to filter specific types
                                // if (new[] { ".txt", ".cs" }.Contains(Path.GetExtension(filePath).ToLowerInvariant()))
                                // {
                                if (await FileContainsText(filePath, searchText) != "none")
                                {
                                    positiveFiles.Add(filePath);
                                }
                                // }
                            }

                        } catch (Exception ex)
                        {
                            Debug.WriteLine($"Something went wrong");
                            Debug.WriteLine($"Files with something: {positiveFiles.Count}");
                        }

                        if(positiveFiles.Count > 0 )
                        {
                            Debug.WriteLine($"Search result not found");
                        }

                        //foreach (string filePath in allFiles)
                        //{
                        //    string[] lines = File.ReadAllLines(file);
                        //    string firstOccurrence = lines.FirstOrDefault(l => l.Contains(SearchString));
                        //    if (firstOccurrence != null)
                        //    {
                        //        lblOutput.Text = firstOccurrence;
                        //        break;
                        //    }
                        //}

                        // TODO: test the api key to make sure it is valid
                    }));
            }
        }
        #endregion

        static async Task<string> FileContainsText(string filePath, string searchText)
        {
            try
            {
                StorageFile sf = await StorageFile.GetFileFromPathAsync(filePath);
                // Read the content of the file
                var content = await FileIO.ReadTextAsync(sf);

                // Search the file content for the specified text
                if (content.ToLower().Contains(searchText))
                {
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file access errors)
                Debug.WriteLine($"Error reading file {filePath}: {ex.Message}");
                return "none";
            }

            return "none";
        }
        #region SelectTargetFolderCommand
        private RelayCommand _selectTargetFolder;

        /// <summary>
        /// Gets the SelectTargetFolder.
        /// </summary>
        public RelayCommand SelectTargetFolderCommand
        {
            get
            {
                return _selectTargetFolder
                    ?? (_selectTargetFolder = new RelayCommand(
                    async () =>
                    {
                        IsReadyToSearch = false;
                        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                        var picker = new Windows.Storage.Pickers.FolderPicker();
                        picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                        picker.FileTypeFilter.Add("*");

                        var folder = await picker.PickSingleFolderAsync();
                        //  https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-using-file-and-folder-pickers
                        if (folder != null)
                        {
                            TargetFolder = folder;
                            SaveSettings();
                        }

                        StatusText = $"Getting files in directory {TargetFolder.Path}";
                        fileCount = folderCount = iteration = 0;
                        FoundFiles = await GetFilesInDirectory(TargetFolder);
                        StatusText = $"Ready to search";

                        IsReadyToSearch = true;

                    }));
            }
        }
        #endregion

        private int fileCount = 0;
        private int folderCount = 0;
        private int iteration = 0;
        private async Task<List<string>> GetFilesInDirectory(StorageFolder folder)
        {
            iteration++;

            if(iteration % 1000 == 0)
            {
                StatusText = $"File Count = {fileCount} \n Folder Count = {folderCount}"
                Debug.WriteLine($"File Count = {fileCount} \n Folder Count = {folderCount}");
            }
            folderCount++;
            List<string> allFiles = new List<string>();

            IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();
            foreach (StorageFile file in fileList)
            {
                fileCount++;
                allFiles.Add(file.Path);
            }

            var subfolders = await folder.GetFoldersAsync();
            foreach (var f in subfolders)
            {
                var subFiles = await GetFilesInDirectory(f);
                allFiles.AddRange(subFiles);
            }

            return allFiles;
        }

        private void SaveSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (_targetFolder != null)
            {
                localSettings.Values[_targetFolderKeyValue] = StorageApplicationPermissions.FutureAccessList.Add(_targetFolder);
            }
        }

        private async Task LoadSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


            if (localSettings.Values.ContainsKey(_targetFolderKeyValue))
            {
                string token = (string)localSettings.Values[_targetFolderKeyValue];
                TargetFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
            }
        }
    }
}
