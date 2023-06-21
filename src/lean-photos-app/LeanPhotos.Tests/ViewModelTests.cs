using leanPhotos.Logic.Services;
using leanPhotos.Logic.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LeanPhotos.Tests
{
    
    [TestClass]
    public class ViewModelTests
    {
        [TestClass]
        public class MainPageViewModelTests
        {
            [TestMethod]
            public async Task InitPhotosAsyncTests()
            {
                var mainPageVM = new MainPageViewModel(new PhotoService());

                await mainPageVM.InitPhotosAsyncCommand.ExecuteAsync(null);

                // verify we haven't lost any photos between the photo service and
                //      the view model
                var photoService = new PhotoService();
                var rawPhotoData = await photoService.GetAllPhotosAsync();
                Assert.AreEqual(rawPhotoData.Count, mainPageVM.VisiblePhotos.Count);
            }

            [TestMethod]
            public async Task GetAlbumPhotosAsyncValidTests()
            {
                var mainPageVM = new MainPageViewModel(new PhotoService());
                string validAlbumQuery = "50";
                mainPageVM.AlbumQuery = validAlbumQuery;
                await mainPageVM.GetAlbumPhotosAsyncCommand.ExecuteAsync(null);

                // verify the view model states
                Assert.IsTrue(mainPageVM.IsAlbumView);
                Assert.IsTrue(mainPageVM.AlbumTitleText.Contains(validAlbumQuery));

                // verify we haven't lost any photos between the photo service and
                //      the view model
                var photoService = new PhotoService();
                var rawPhotoData = await photoService.GetPhotosWithQueryAsync(validAlbumQuery);
                Assert.AreEqual(rawPhotoData.Count, mainPageVM.VisiblePhotos.Count);

                // switch back to all photos & verify the album info has been scrubbed
                await mainPageVM.InitPhotosAsyncCommand.ExecuteAsync(null);

                // verify the view model state
                Assert.IsFalse(mainPageVM.IsAlbumView);
                Assert.IsTrue(String.IsNullOrEmpty(mainPageVM.AlbumQuery));
                Assert.IsTrue(String.IsNullOrEmpty(mainPageVM.AlbumTitleText));
            }

            [TestMethod]
            public async Task GetAlbumPhotosAsyncErrorTests()
            {
                var mainPageVM = new MainPageViewModel(new PhotoService());
                
                string nullAlbumQuery = null;
                mainPageVM.AlbumQuery = nullAlbumQuery;
                await mainPageVM.GetAlbumPhotosAsyncCommand.ExecuteAsync(null);

                Assert.AreEqual("To sort by album, you must input a number between 1 and 100", mainPageVM.ErrorText);
                Assert.IsTrue(mainPageVM.IsErrorAlertVisible);

                string invalidAlbumQuery = "zero";
                mainPageVM.AlbumQuery = invalidAlbumQuery;
                await mainPageVM.GetAlbumPhotosAsyncCommand.ExecuteAsync(null);

                Assert.IsTrue(mainPageVM.ErrorText.Contains(invalidAlbumQuery));
                Assert.IsTrue(mainPageVM.IsErrorAlertVisible);

                // make sure the dismiss command dismisses the error
                mainPageVM.DismissErrorAlertCommand.Execute(null);
                Assert.IsFalse(mainPageVM.IsErrorAlertVisible);
                Assert.IsTrue(String.IsNullOrEmpty(mainPageVM.ErrorText));
            }
        }
    }
}
