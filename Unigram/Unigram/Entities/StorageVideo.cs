﻿using System;
using System.Threading.Tasks;
using Unigram.Common;
using Unigram.Converters;
using Windows.Foundation;
using Windows.Media.Effects;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Unigram.Entities
{
    public class StorageVideo : StorageMedia
    {
        private BasicProperties _basic;

        public StorageVideo(StorageFile file, BasicProperties basic, VideoProperties props, MediaEncodingProfile profile)
            : base(file, basic)
        {
            _fullRectangle = new Rect(0, 0, props.GetWidth(), props.GetHeight());

            _basic = basic;
            Properties = props;
            Profile = profile;

            videoDuration = props.Duration.TotalMilliseconds;

            originalSize = (long)basic.Size;
            originalWidth = (int)props.GetWidth();
            originalHeight = (int)props.GetHeight();
            originalBitrate = bitrate = (int)props.Bitrate; //(trackBitrate / 100000 * 100000);

            //if (bitrate > 900000)
            //{
            //    bitrate = 900000;
            //}

            LoadPreview();
        }

        public override uint Width => Properties.GetWidth();
        public override uint Height => Properties.GetHeight();

        public override bool IsVideo => true;
        public override bool IsAnimation => IsMuted;

        public new static async Task<StorageVideo> CreateAsync(StorageFile file)
        {
            try
            {
                if (!file.IsAvailable)
                {
                    return null;
                }

                var profile = await MediaEncodingProfile.CreateFromFileAsync(file);
                if (profile.Video == null)
                {
                    return null;
                }

                var basic = await file.GetBasicPropertiesAsync();
                var video = await file.Properties.GetVideoPropertiesAsync();

                if (video.Width > 0 && video.Height > 0)
                {
                    return new StorageVideo(file, basic, video, profile);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public VideoProperties Properties { get; private set; }

        public MediaEncodingProfile Profile { get; private set; }

        public override void Refresh()
        {
            base.Refresh();
            LoadPreview();
        }

        public string Duration
        {
            get
            {
                var duration = Properties.Duration;
                if (duration.TotalHours >= 1)
                {
                    return duration.ToString("h\\:mm\\:ss");
                }
                else
                {
                    return duration.ToString("mm\\:ss");
                }
            }
        }

        private int _compression;
        public int Compression
        {
            get
            {
                return _compression;
            }
            set
            {
                Set(ref _compression, value);
            }
        }

        private int _maxCompression = 1;
        public int MaxCompression
        {
            get
            {
                return _maxCompression;
            }
            set
            {
                Set(ref _maxCompression, value);
                RaisePropertyChanged(() => CanCompress);
            }
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get
            {
                return _isMuted;
            }
            set
            {
                Set(ref _isMuted, value);
                Set(() => Compression, ref _compression, MaxCompression >= 1 ? 1 : 0);

                RaisePropertyChanged(() => CanCompress);
            }
        }

        public bool CanCompress
        {
            get
            {
                return !IsMuted && MaxCompression > 1;
            }
        }

        private void LoadPreview()
        {
            //_preview = _thumbnail;
            //_preview = await ImageHelper.GetPreviewBitmapAsync(File);
            //RaisePropertyChanged(() => Preview);

            int originalWidth = this.originalWidth;
            int originalHeight = this.originalHeight;
            int originalMaxCompression = MaxCompression;

            if (IsEdited && _editState is BitmapEditState state && state.Rectangle is Rect rectangle)
            {
                originalWidth = (int)rectangle.Width;
                originalHeight = (int)rectangle.Height;
            }

            if (originalWidth > 4096 || originalHeight > 4096)
            {
                MaxCompression = 8;
            }
            else if (originalWidth > 3840 || originalHeight > 3840)
            {
                MaxCompression = 7;
            }
            else if (originalWidth > 1920 || originalHeight > 1920)
            {
                MaxCompression = 6;
            }
            else if (originalWidth > 1280 || originalHeight > 1280)
            {
                MaxCompression = 5;
            }
            else if (originalWidth > 848 || originalHeight > 848)
            {
                MaxCompression = 4;
            }
            else if (originalWidth > 640 || originalHeight > 640)
            {
                MaxCompression = 3;
            }
            else if (originalWidth > 480 || originalHeight > 480)
            {
                MaxCompression = 2;
            }
            else
            {
                MaxCompression = 1;
            }

            if (originalMaxCompression != MaxCompression)
                Compression = MaxCompression; // no compression

            //if (Compression >= MaxCompression)
            //{
            //    Compression = MaxCompression - 1;
            //}
        }

        private int originalWidth;
        private int originalHeight;

        private long originalSize;
        private int originalBitrate;

        private int resultWidth;
        private int resultHeight;
#pragma warning disable CS0649 // Never assigned field, always null
        private int rotationValue;
#pragma warning disable CS0649 // Never assigned field, always null
        private int bitrate;
#pragma warning disable CS0649 // Never assigned field, always null
        private long audioFramesSize;
#pragma warning disable CS0649 // Never assigned field, always null
        private long videoFramesSize;
        private double videoDuration;

        private int estimatedSize;
        private long estimatedDuration;

        //private long startTime;
        //private long endTime;

        public override string ToString()
        {
            return ToString(_compression);
        }

        public async Task<MediaEncodingProfile> GetEncodingAsync()
        {
            int originalWidth = (int)Properties.Width;
            int originalHeight = (int)Properties.Height;

            if (IsEdited && _editState is BitmapEditState state && state.Rectangle is Rect rectangle)
            {
                originalWidth = (int)rectangle.Width;
                originalHeight = (int)rectangle.Height;
            }

            UpdateWidthHeightBitrateForCompression(_compression);

            var profile = await MediaEncodingProfile.CreateFromFileAsync(File);
            if (resultWidth != originalWidth || resultHeight != originalHeight)
            {
                profile.Video.Width = (uint)resultWidth;
                profile.Video.Height = (uint)resultHeight;
                profile.Video.Bitrate = Math.Min((uint)bitrate, profile.Video.Bitrate);
            }

            if (_isMuted)
            {
                profile.Audio = null;
            }

            return profile;
        }

        public string ToString(int compression)
        {
            UpdateWidthHeightBitrateForCompression(compression);

            var selectedCompression = compression;

            //estimatedDuration = (long)Math.ceil((videoTimelineView.getRightProgress() - videoTimelineView.getLeftProgress()) * videoDuration);
            estimatedDuration = (long)videoDuration;

            int width;
            int height;

            if (/*compressItem.getTag() == null ||*/ selectedCompression == _maxCompression)
            {
                width = rotationValue == 90 || rotationValue == 270 ? originalHeight : originalWidth;
                height = rotationValue == 90 || rotationValue == 270 ? originalWidth : originalHeight;
                estimatedSize = (int)(originalSize * ((float)estimatedDuration / videoDuration));
            }
            else
            {
                width = rotationValue == 90 || rotationValue == 270 ? resultHeight : resultWidth;
                height = rotationValue == 90 || rotationValue == 270 ? resultWidth : resultHeight;

                estimatedSize = (int)((audioFramesSize + videoFramesSize) * ((float)estimatedDuration / videoDuration));
                estimatedSize += estimatedSize / (32 * 1024) * 16;
            }

            //if (videoTimelineView.getLeftProgress() == 0)
            //{
            //    startTime = -1;
            //}
            //else
            //{
            //    startTime = (long)(videoTimelineView.getLeftProgress() * videoDuration) * 1000;
            //}
            //if (videoTimelineView.getRightProgress() == 1)
            //{
            //    endTime = -1;
            //}
            //else
            //{
            //    endTime = (long)(videoTimelineView.getRightProgress() * videoDuration) * 1000;
            //}

            String videoDimension = string.Format("{0}x{1}", width, height);
            int minutes = (int)(estimatedDuration / 1000 / 60);
            int seconds = (int)Math.Ceiling(estimatedDuration / 1000d) - minutes * 60;
            String videoTimeSize = string.Format("{0}:{1:D2}, ~{2}", minutes, seconds, FileSizeConverter.Convert(estimatedSize));
            return string.Format("{0}, {1}", videoDimension, videoTimeSize);
        }

        private void UpdateWidthHeightBitrateForCompression(int compression)
        {
            var selectedCompression = compression;

            if (selectedCompression != _maxCompression)
            {
                float maxSize;
                int targetBitrate;
                switch (selectedCompression)
                {
                    case 0:
                        maxSize = 432.0f;
                        targetBitrate = 400000;
                        break;
                    case 1:
                        maxSize = 640.0f;
                        targetBitrate = 900000;
                        break;
                    case 2:
                        maxSize = 848.0f;
                        targetBitrate = 1600000;
                        break;
                    case 3:
                        targetBitrate = 3200000;
                        maxSize = 1280.0f;
                        break;
                    case 4:
                        targetBitrate = 4400000;
                        maxSize = 1920.0f;
                        break;
                    case 5:
                        targetBitrate = 5000000;
                        maxSize = 3840.0f;
                        break;
                    case 6:
                        targetBitrate = 5000000;
                        maxSize = 4096.0f;
                        break;
                    case 7:
                    default: // case 8: is always uncompressed
                        targetBitrate = originalBitrate;
                        maxSize = 7680.0f;
                        break;
                }

                float scale = originalWidth > originalHeight ? maxSize / originalWidth : maxSize / originalHeight;
                resultWidth = (int)Math.Round(originalWidth * scale / 2) * 2;
                resultHeight = (int)Math.Round(originalHeight * scale / 2) * 2;
                bitrate = Math.Min(targetBitrate, originalBitrate);
                if (bitrate != 0)
                {
                    videoFramesSize = (long)(bitrate / 8 * videoDuration / 1000);
                }
            } else {
                resultWidth = originalWidth;
                resultHeight = originalHeight;
            }
        }

        public VideoTransformEffectDefinition GetTransform()
        {
            var crop = _editState?.Rectangle ?? Rect.Empty;
            if (!(crop.IsEmpty || (crop.X == 0 && crop.Y == 0 && crop.Width == 1 && crop.Height == 1)))
            {
                var transform = new VideoTransformEffectDefinition();
                transform.CropRectangle = crop;

                return transform;
            }

            return null;
        }
    }
}
