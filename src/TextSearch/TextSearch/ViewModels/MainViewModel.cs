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
using Windows.Storage.Streams;

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

        private string _searchString = "zira";
        public string SearchString
        {
            get { return _searchString; }
            set { Set(ref _searchString, value); }
        }
        private string _searchString2 = "404,906,6275";
        public string SearchString2
        {
            get { return _searchString2; }
            set { Set(ref _searchString2, value); }
        }
        private string _searchString3 = "770,313,1997";
        public string SearchString3
        {
            get { return _searchString3; }
            set { Set(ref _searchString3, value); }
        }

        private string _searchString4 = "770,928,7132";
        public string SearchString4
        {
            get { return _searchString4; }
            set { Set(ref _searchString4, value); }
        }
        private string _searchString5 = "770,988,6927";
        public string SearchString5
        {
            get { return _searchString5; }
            set { Set(ref _searchString5, value); }
        }
        private string _searchString6 = "404,434,2260";
        public string SearchString6
        {
            get { return _searchString6; }
            set { Set(ref _searchString6, value); }
        }

        private string _searchString7 = "404,409,9466";
        public string SearchString7
        {
            get { return _searchString7; }
            set { Set(ref _searchString7, value); }
        }
        private string _searchString8 = "404,929,9466";
        public string SearchString8
        {
            get { return _searchString8; }
            set { Set(ref _searchString8, value); }
        }
        private string _searchString9 = "404,754,3124";
        public string SearchString9
        {
            get { return _searchString9; }
            set { Set(ref _searchString9, value); }
        }

        private string _searchString10 = "770,457,2340";
        public string SearchString10
        {
            get { return _searchString10; }
            set { Set(ref _searchString10, value); }
        }
        private string _searchString11 = "404,986,9478";
        public string SearchString11
        {
            get { return _searchString11; }
            set { Set(ref _searchString11, value); }
        }
        private string _searchString12 = "770,807,6576";
        public string SearchString12
        {
            get { return _searchString12; }
            set { Set(ref _searchString12, value); }
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
                        //FoundFiles = await GetFilesInDirectory(TargetFolder);

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
                            List<string> searchTerms = new List<string>();
                            if(SearchString.Length > 1 ) { searchTerms.Add(SearchString); }
                            if (SearchString2.Length > 1) { searchTerms.Add(SearchString2); }
                            if (SearchString3.Length > 1) { searchTerms.Add(SearchString3); }
                            //if (SearchString4.Length > 1) { searchTerms.Add(SearchString4); }
                            //if (SearchString5.Length > 1) { searchTerms.Add(SearchString5); }
                            //if (SearchString6.Length > 1) { searchTerms.Add(SearchString6); }
                            //if (SearchString7.Length > 1) { searchTerms.Add(SearchString7); }
                            //if (SearchString8.Length > 1) { searchTerms.Add(SearchString8); }
                            //if (SearchString9.Length > 1) { searchTerms.Add(SearchString9); }
                            //if (SearchString10.Length > 1) { searchTerms.Add(SearchString10); }
                            //if (SearchString11.Length > 1) { searchTerms.Add(SearchString11); }
                            //if (SearchString12.Length > 1) { searchTerms.Add(SearchString12); }

                            foreach (var filePath in FoundFiles)
                            {
                                iteration2++;

                                if (iteration2 % 500 == 0)
                                {
                                    StatusText = $"Searched {iteration2} files of {FoundFiles.Count} with {positiveFiles.Count} results";
                                    Debug.WriteLine(StatusText);
                                }
                            // Optional: Check the file extension if you want to filter specific types
                            // if (new[] { ".txt", ".cs" }.Contains(Path.GetExtension(filePath).ToLowerInvariant()))
                            // {
                                if (SearchString.Length < 3)
                                {
                                    //(404)604 - 8612
                                    SearchString = "4908";
                                }
                                var searchResults = await FileContainsText(filePath, searchTerms);
                                if (searchResults != "none")
                                {
                                    positiveFiles.Add(searchResults);
                                    Debug.WriteLine(StatusText);
                                }
                                // }
                            }

                        } catch (Exception ex)
                        {
                            Debug.WriteLine($"Something went wrong");
                            Debug.WriteLine($"Files with something: {positiveFiles.Count}");
                        }

                        if(positiveFiles.Count == 0 )
                        {
                            Debug.WriteLine($"Search result not found");
                        } else
                        {
                            StatusText = "";
                            foreach(string pos in positiveFiles)
                            {
                                StatusText += pos + "\n";
                            }
                            Debug.WriteLine(StatusText);
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

        static async Task<string> FileContainsText(string filePath, List<string> searchStrings)
        {
            try
            {
                StorageFile sf = await StorageFile.GetFileFromPathAsync(filePath);
                // Read the content of the file
                //var content = await FileIO.ReadTextAsync(sf);

                if (sf != null)
                {
                    IBuffer buffer = await FileIO.ReadBufferAsync(sf);
                    DataReader reader = DataReader.FromBuffer(buffer);
                    byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                    reader.ReadBytes(fileContent);
                    string text = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
                    // Search the file content for the specified text
                    bool isFound = true;
                    string searchTerm = "";
                    foreach (string line in searchStrings)
                    {
                        isFound = true;
                        string tempSearchTerm = "";

                        // Concat
                        List<string> multiSearchString = line.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach(string term in multiSearchString)
                        {
                            if (!text.ToLower().Contains(term.ToLower()))
                            {
                                isFound = false;
                            }
                            else
                            {
                                tempSearchTerm += term+",";
                            }
                        }

                        if (isFound)
                        {
                            searchTerm += tempSearchTerm + " ";
                        }
                    }

                    if (searchTerm != "")
                    {
                        Debug.WriteLine($"Found: {searchTerm} in {filePath} ");
                        return searchTerm + filePath;
                    }
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
                StatusText = $"File Count = {fileCount} \n Folder Count = {folderCount}";
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
