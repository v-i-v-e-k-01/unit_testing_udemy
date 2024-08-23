﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;



namespace TestNinja.Mocking
{
    public class VideoService
    {
        //// Dependency Injection via Properties
        //public IFileReader FileReader { get; set; } 
        //public VideoService()
        //{
        //    FileReader = new FileReader();
        //}


        // Dependency injection via Constructor
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;
        public VideoService(IFileReader fileReader, IVideoRepository videoRepository= null)
        {
            _fileReader = fileReader;
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        //for ease of access, use above constructor for testing and below constructor for production code
        public VideoService()
        {
            _fileReader = new FileReader();
        }


        //we can combine above two constructors as below
        //public VideoService(IFileReader fileReader = null)
        //{
        //    _fileReader = fileReader ?? new FileReader();
        //}


        public string ReadVideoTitle()
        {
            //var str = FileReader.Read("video.txt");
            var str = _fileReader.Read("video.txt");

            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}