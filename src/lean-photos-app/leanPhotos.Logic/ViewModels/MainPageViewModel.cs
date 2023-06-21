using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using leanPhotos.Logic.Helpers;
using leanPhotos.Logic.Interfaces;
using leanPhotos.Logic.Models;
using leanPhotos.Logic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace leanPhotos.Logic.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        private IPhotoService _photoService;

        public MainPageViewModel(IPhotoService photoService)
        {
            _photoService = photoService;

            InitPhotosAsyncCommand = new AsyncRelayCommand(InitPhotosAsync);
            GetAlbumPhotosAsyncCommand = new AsyncRelayCommand(GetAlbumPhotosAsync);
            DismissErrorAlertCommand = new RelayCommand(DismissErrorAlert);
        }

        #region Commands and Command Methods

        public AsyncRelayCommand InitPhotosAsyncCommand { get; }
        public AsyncRelayCommand GetAlbumPhotosAsyncCommand { get; }
        public RelayCommand DismissErrorAlertCommand { get; }

        /// <summary>
        /// Make a call for all the photo data and add it to the observable
        ///     VisiblePhotos object
        /// </summary>
        private async Task InitPhotosAsync()
        {
            IsLoading = true;
            IsAlbumView = false;
            AlbumQuery = AlbumTitleText = "";

            List<Photo> rawPhotoData = await _photoService.GetAllPhotosAsync();
            VisiblePhotos = new ObservableCollection<Photo>(rawPhotoData);

            IsLoading = false;
        }

        /// <summary>
        /// Using the AlbumQuery property, retrieve only the photos that
        ///     are in a given album and display that album information
        /// </summary>
        private async Task GetAlbumPhotosAsync()
        {
            IsLoading = true;

            if (String.IsNullOrEmpty(AlbumQuery))
            {
                ErrorText = "To sort by album, you must input a number between 1 and 100";
                IsErrorAlertVisible = true;
            }
            else
            {
                if (InputValidation.IsAlbumQueryInputValid(AlbumQuery))
                {
                    List<Photo> rawPhotoData = await _photoService.GetPhotosWithQueryAsync(AlbumQuery);

                    VisiblePhotos = new ObservableCollection<Photo>(rawPhotoData);
                    AlbumTitleText = $"Album {AlbumQuery}";
                    IsAlbumView = true;
                }
                else
                {
                    ErrorText = $"There is no album with the value '{AlbumQuery}'. Please enter a number between 1 and 100 and try again.";
                    IsErrorAlertVisible = true;
                }
            }

            IsLoading = false;
        }

        /// <summary>
        /// Dismiss an error alert
        /// </summary>
        private void DismissErrorAlert()
        {
            IsErrorAlertVisible = false;
            ErrorText = "";
        }

        #endregion

        #region Properties 
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            private set => SetProperty(ref isLoading, value);
        }

        private bool isAlbumView;
        public bool IsAlbumView
        {
            get => isAlbumView;
            private set => SetProperty(ref isAlbumView, value);
        }

        private bool isItemSelected;
        public bool IsItemSelected
        {
            get => isItemSelected;
            set => SetProperty(ref isItemSelected, value);
        }

        private string albumQuery;
        public string AlbumQuery
        {
            get => albumQuery;
            set => SetProperty(ref albumQuery, value);
        }

        private ObservableCollection<Photo> visiblePhotos = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> VisiblePhotos
        {
            get => visiblePhotos;
            private set => SetProperty(ref visiblePhotos, value);
        }

        private string albumTitleText;
        public string AlbumTitleText
        {
            get => albumTitleText;
            private set => SetProperty(ref albumTitleText, value);
        }

        private Photo selectedItem;
        public Photo SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private bool isErrorAlertVisible = false;
        public bool IsErrorAlertVisible
        {
            get => isErrorAlertVisible;
            private set => SetProperty(ref isErrorAlertVisible, value);
        }

        private string errorText;
        public string ErrorText
        {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }
        #endregion
    }
}