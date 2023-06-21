using Microsoft.VisualStudio.TestTools.UnitTesting;
using leanPhotos.Logic.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using leanPhotos.Logic.Models;

namespace LeanPhotos.Tests
{

    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public async Task PhotoServiceGetPhotoTest()
        {
            var photoService = new PhotoService();

            var photoResult = await photoService.GetAllPhotosAsync();
            Assert.IsInstanceOfType(photoResult, typeof(List<Photo>));
            Assert.AreEqual(5000, photoResult.Count);
        }

        [TestMethod]
        public async Task PhotoServiceGetPhotoWithQueryTest()
        {
            var photoService = new PhotoService();

            var photoResult = await photoService.GetPhotosWithQueryAsync("5");
            Assert.IsInstanceOfType(photoResult, typeof(List<Photo>));
            Assert.AreEqual(50, photoResult.Count);

            // query method with null query input
            var nullPhotoResult = await photoService.GetPhotosWithQueryAsync(null);
            Assert.IsInstanceOfType(nullPhotoResult, typeof(List<Photo>));
            Assert.AreEqual(0, nullPhotoResult.Count);

            // query method with bad input
            var badInputPhotoResult = await photoService.GetPhotosWithQueryAsync("badInput");
            Assert.IsInstanceOfType(badInputPhotoResult, typeof(List<Photo>));
            Assert.AreEqual(0, badInputPhotoResult.Count);
        }
    }
}
