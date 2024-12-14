﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Api.Helpers;
using Telegram.Api.Services;
using Telegram.Api.TL;
using Windows.Globalization.DateTimeFormatting;
//using Unigram.Strings;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Unigram.Converters
{
    public class BindConvert
    {
        private static BindConvert _current;
        public static BindConvert Current
        {
            get
            {
                if (_current == null)
                    _current = new BindConvert();

                return _current;
            }
        }

        public DateTimeFormatter ShortDate { get; } = new DateTimeFormatter("shortdate", Windows.System.UserProfile.GlobalizationPreferences.Languages);
        public DateTimeFormatter ShortTime { get; } = new DateTimeFormatter("shorttime", Windows.System.UserProfile.GlobalizationPreferences.Languages);
        public DateTimeFormatter LongDate { get; } = new DateTimeFormatter("longdate", Windows.System.UserProfile.GlobalizationPreferences.Languages);
        public DateTimeFormatter LongTime { get; } = new DateTimeFormatter("longtime", Windows.System.UserProfile.GlobalizationPreferences.Languages);

        public List<SolidColorBrush> PlaceholderColors { get; private set; }

        private BindConvert()
        {
            PlaceholderColors = new List<SolidColorBrush>();

            for (int i = 0; i < 6; i++)
            {
                PlaceholderColors.Add((SolidColorBrush)Application.Current.Resources[$"Placeholder{i}Brush"]);
            }
        }

        public SolidColorBrush Bubble(int uid)
        {
            return PlaceholderColors[(uid + SettingsHelper.UserId) % PlaceholderColors.Count];
        }


        //private SolidColorBrush BubbleInternal(int? value)
        //{
        //    return Application.Current.Resources[$"Placeholder{Utils.GetColorIndex(value ?? 0)}Brush"] as SolidColorBrush;

        //    switch (Utils.GetColorIndex(value ?? 0))
        //    {
        //        case 0:
        //            return Application.Current.Resources["PlaceholderRedBrush"] as SolidColorBrush;
        //        case 1:
        //            return Application.Current.Resources["PlaceholderGreenBrush"] as SolidColorBrush;
        //        case 2:
        //            return Application.Current.Resources["PlaceholderYellowBrush"] as SolidColorBrush;
        //        case 3:
        //            return Application.Current.Resources["PlaceholderBlueBrush"] as SolidColorBrush;
        //        case 4:
        //            return Application.Current.Resources["PlaceholderPurpleBrush"] as SolidColorBrush;
        //        case 5:
        //            return Application.Current.Resources["PlaceholderPinkBrush"] as SolidColorBrush;
        //        case 6:
        //            return Application.Current.Resources["PlaceholderCyanBrush"] as SolidColorBrush;
        //        case 7:
        //            return Application.Current.Resources["PlaceholderOrangeBrush"] as SolidColorBrush;
        //        default:
        //            return Application.Current.Resources["ListViewItemPlaceholderBackgroundThemeBrush"] as SolidColorBrush;
        //    }
        //}

        public string CallDuration(int seconds)
        {
            if (seconds < 60)
            {
                var format = /*AppResources.CallSeconds_any*/"CallSeconds_any";
                var number = seconds;
                if (number == 1)
                {
                    format = /*AppResources.CallSeconds_1*/"CallSeconds_1";
                }
                else if (number == 2)
                {
                    format = /*AppResources.CallSeconds_2*/"CallSeconds_2";
                }
                else if (number == 4)
                {
                    format = /*AppResources.CallSeconds_3_10*/"CallSeconds_3_10";
                }

                return string.Format(format, seconds);
            }
            else
            {
                var format = /*AppResources.CallMinutes_any*/"CallMinutes_any";
                var number = seconds / 60;
                if (number == 1)
                {
                    format = /*AppResources.CallMinutes_1*/"CallMinutes_1";
                }
                else if (number == 2)
                {
                    format = /*AppResources.CallMinutes_2*/"CallMinutes_2";
                }
                else if (number == 4)
                {
                    format = /*AppResources.CallMinutes_3_10*/"CallMinutes_3_10";
                }

                return string.Format(format, seconds);
            }
        }

        public string CallShortDuration(int seconds)
        {
            if (seconds < 60)
            {
                var format = /*AppResources.CallShortSeconds_any*/"CallShortSeconds_any";
                var number = seconds;
                if (number == 1)
                {
                    format = /*AppResources.CallShortSeconds_1*/"CallShortSeconds_1";
                }
                else if (number == 2)
                {
                    format = /*AppResources.CallShortSeconds_2*/"CallShortSeconds_2";
                }
                else if (number == 4)
                {
                    format = /*AppResources.CallShortSeconds_3_10*/"CallShortSeconds_3_10";
                }

                return string.Format(format, seconds);
            }
            else
            {
                var format = /*AppResources.CallShortMinutes_any*/"CallShortMinutes_any";
                var number = seconds / 60;
                if (number == 1)
                {
                    format = /*AppResources.CallShortMinutes_1*/"CallShortMinutes_1";
                }
                else if (number == 2)
                {
                    format = /*AppResources.CallShortMinutes_2*/"CallShortMinutes_2";
                }
                else if (number == 4)
                {
                    format = /*AppResources.CallShortMinutes_3_10*/"CallShortMinutes_3_10";
                }

                return string.Format(format, seconds);
            }
        }

        public string Date(int value)
        {
            return ShortTime.Format(DateTime(value));
        }

        public DateTime DateTime(int value)
        {
            var clientDelta = MTProtoService.Current.ClientTicksDelta;
            var utc0SecsLong = value * 4294967296 - clientDelta;
            var utc0SecsInt = utc0SecsLong / 4294967296.0;
            var dateTime = Utils.UnixTimestampToDateTime(utc0SecsInt);

            return dateTime;
        }

        public string State(TLMessageState value)
        {
            switch (value)
            {
                case TLMessageState.Sending:
                    return "\uE600";
                case TLMessageState.Confirmed:
                    return "\uE602";
                case TLMessageState.Read:
                    return "\uE601";
                default:
                    return "\uFFFD";
            }
        }

        public string Views(TLMessage message, int? views)
        {
            var number = string.Empty;

            if (message.HasViews)
            {
                number = ShortNumber(views ?? 0);

                if (message.IsPost && message.HasFromId && message.From != null)
                {
                    number += $"   {message.From.FullName},";
                }
            }

            return number;
        }

        public string ShortNumber(int number)
        {
            var K = string.Empty;
            var lastDec = 0;

            while (number / 1000 > 0)
            {
                K += "K";
                lastDec = (number % 1000) / 100;
                number /= 1000;
            }

            if (lastDec != 0 && K.Length > 0)
            {
                if (K.Length == 2)
                {
                    return string.Format("{0}.{1}M", number, lastDec);
                }
                else
                {
                    return string.Format("{0}.{1}{2}", number, lastDec, K);
                }
            }

            if (K.Length == 2)
            {
                return string.Format("{0}M", number);
            }
            else
            {
                return string.Format("{0}{1}", number, K);
            }
        }
    }
}
