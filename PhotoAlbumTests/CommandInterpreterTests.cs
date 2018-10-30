using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoAlbumLibrary;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbumTests
{
    [TestClass]
    public class CommandInterpreterTests
    {
        private CommandInterpreter _interpreter;
        private Mock<IPhotoRepository> _photoRepository;
        private IEnumerable<Photo> _testPhotos;

        [TestInitialize]
        public void Setup()
        {
            _testPhotos = new List<Photo>
            {
                new Photo { Id=1, AlbumId=3, Title="photo1" },
                new Photo { Id=2, AlbumId=3, Title="photo2" },
                new Photo { Id=3, AlbumId=3, Title="photo3" },
                new Photo { Id=4, AlbumId=4, Title="photo4" },
                new Photo { Id=5, AlbumId=4, Title="photo5" }
            };

            _photoRepository = new Mock<IPhotoRepository>();

            _interpreter = new CommandInterpreter(_photoRepository.Object);
        }

        [TestMethod]
        public void Evaluate_EmptyInput_ReturnEmptyString()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("");

            // Assert
            Assert.AreEqual("", retVal);
        }

        [TestMethod]
        public void Evaluate_Help_ReturnHelpText()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("help");

            // Assert
            Assert.AreEqual(Constants.HelpText, retVal);
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_NoParameter_ReturnsUsage()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("photo-album");

            // Assert
            Assert.AreEqual(Constants.PhotoAlbumUsage, retVal);
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_TwoParameters_ReturnsUsage()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("photo-album parm1 parm2");

            // Assert
            Assert.AreEqual(Constants.PhotoAlbumUsage, retVal);
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_AlbumDoesNotExist_ReturnsErrorMessage()
        {
            // Arrange
            _photoRepository.Setup(pr => pr.GetAlbum(1)).Returns(_testPhotos.Where(p => p.AlbumId == 1));

            // Act
            var retVal = _interpreter.Evaluate("photo-album 1");

            // Assert
            Assert.AreEqual(Constants.AlbumNotFound, retVal);
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_AlbumExists_ReturnsPhotoList()
        {
            // Arrange
            _photoRepository.Setup(pr => pr.GetAlbum(3)).Returns(_testPhotos.Where(p => p.AlbumId == 3));

            // Act
            var retVal = _interpreter.Evaluate("photo-album 3");

            // Assert
            var expectedResult = "[1] photo1\n[2] photo2\n[3] photo3";
            Assert.AreEqual(retVal, expectedResult); // TODO: improve the assertion of this test
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_AttemptsToSearchForAlbum()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("photo-album 999");

            // Assert
            _photoRepository.Verify(pr => pr.GetAlbum(999));
        }

        [TestMethod]
        public void Evaluate_PhotoAlbum_NonIntParameter_ReturnsError()
        {
            // Arrange

            // Act
            var retVal = _interpreter.Evaluate("photo-album adsf");

            // Assert
            Assert.AreEqual(Constants.NonIntParameter, retVal);
        }
    }
}
