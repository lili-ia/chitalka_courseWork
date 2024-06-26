﻿using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace chitalka_courseWork
{
    public enum ReadingStatus
    {
        NotStarted,
        InProgress,
        Read
    }

    public partial class Book : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private string _author;

        [ObservableProperty]
        private int _pagesCount;

        [ObservableProperty]
        private ReadingStatus _readingStatus;

        [ObservableProperty]
        private string? _coverImageUrl;

        [ObservableProperty]
        private Statistics _stats;

        public Book(string title, string author, string? description, int pagesCount, string? coverImageUrl)
        {
            Title = title;
            Author = author;
            Description = description;
            PagesCount = pagesCount;
            ReadingStatus = ReadingStatus.NotStarted;
            CoverImageUrl = coverImageUrl;
            Stats = new Statistics();
            Stats.SetTotalPageCount(PagesCount);  
            Stats.PropertyChanged += Stats_PropertyChanged;
        }

        private void Stats_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Statistics.Progress))
            {
                UpdateReadingStatus();
            }
        }


        public void UpdateReadingStatus()
        {
            if (Stats.Progress >= 100)
                ReadingStatus = ReadingStatus.Read;
            
            if (ReadingStatus == ReadingStatus.Read && Stats.Progress < 100)
                ReadingStatus = ReadingStatus.InProgress;
        }

        partial void OnPagesCountChanged(int value)
        {
            if (Stats != null)
                Stats.UpdateProgress();
            else
                Stats = new Statistics();
        }

    }
}
