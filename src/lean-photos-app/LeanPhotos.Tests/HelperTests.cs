using leanPhotos.Logic.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LeanPhotos.Tests
{
    // Input Validation

    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void InputValidationTests()
        {
            Assert.IsTrue(InputValidation.IsAlbumQueryInputValid("1"));
            Assert.IsTrue(InputValidation.IsAlbumQueryInputValid("99"));

            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid("0"));
            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid("1500"));

            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid("one"));
            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid(String.Empty));
            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid(null));
            Assert.IsFalse(InputValidation.IsAlbumQueryInputValid(""));
        }
    }
}