using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Mocking;

using Moq;

namespace TestNinja.UnitTests.Mocking
{

    [TestFixture]
    internal class VideoServiceTests
    {
        VideoService _videoService;
        Mock<IFileReader> _fileReader;
        Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            //_videoService = new VideoService(new FakeFileReader());

            //OR
            //Automatic FakeFileReader created using Mock
            _fileReader = new Mock<IFileReader>();

            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
        }
             
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorText()
        {
            _fileReader.Setup(f => f.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error").IgnoreCase);
        }




        //[Test]
        //public void ReadVideoTitle_FileInput_ReturnString()
        //{
        //    _fileReader.Setup(f => f.Read("video.txt")).Returns("videoAbstractTextHere");

        //    var result = _videoService.ReadVideoTitle();

        //    Assert.That(result, Is.EqualTo("video"));
        //}



        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosProcessed_ReturnsEmptyString()
        {
            _videoRepository.Setup(f => f.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That( result , Is.EqualTo(""));
        }

        [Test]

        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnsVideoIdsAsString()
        {
            var videoList = new List<Video>
            {
                new Video{ Id =1},
                new Video{ Id =2},
                new Video{ Id =3}
            };
            _videoRepository.Setup(f => f.GetUnprocessedVideos()).Returns(videoList);

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
