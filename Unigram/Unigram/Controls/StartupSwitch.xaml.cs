﻿using System;
using System.Threading.Tasks;
using Unigram.Services;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Unigram.Controls
{
    public sealed partial class StartupSwitch : UserControl
    {
        public StartupSwitch()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.StartupTaskContract", 2))
            {
#if DESKTOP_BRIDGE
                FindName(nameof(ToggleMinimized));
#endif

                OnLoaded();
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private async void OnLoaded()
        {
            Toggle.Toggled -= OnToggled;

            if (ToggleMinimized != null)
            {
                ToggleMinimized.Toggled -= Minimized_Toggled;
            }

            var task = await GetTaskAsync();
            if (task == null || task.State == StartupTaskState.DisabledByUser)
            {
                Toggle.IsOn = false;
                Toggle.IsEnabled = false;

                if (ToggleMinimized != null)
                {
                    ToggleMinimized.IsOn = false;
                    ToggleMinimized.Visibility = Visibility.Collapsed;
                }

                Headered.Footer = Strings.Resources.lng_settings_auto_start_disabled_uwp
                    .Replace("Telegram Desktop", "Unigram");

                Visibility = Visibility.Visible;
            }
            else if (task.State == StartupTaskState.Enabled)
            {
                Toggle.IsOn = true;
                Toggle.IsEnabled = true;

                if (ToggleMinimized != null)
                {
                    ToggleMinimized.IsOn = SettingsService.Current.IsLaunchMinimized;
                    ToggleMinimized.Visibility = Visibility.Visible;
                }

                Headered.Footer = string.Empty;

                Visibility = Visibility.Visible;
            }
            else if (task.State == StartupTaskState.Disabled)
            {
                Toggle.IsOn = false;
                Toggle.IsEnabled = true;

                if (ToggleMinimized != null)
                {
                    ToggleMinimized.IsOn = false;
                    ToggleMinimized.Visibility = Visibility.Collapsed;
                }

                Headered.Footer = string.Empty;

                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }

            Toggle.Toggled += OnToggled;

            if (ToggleMinimized != null)
            {
                ToggleMinimized.Toggled += Minimized_Toggled;
            }
        }

        private async void OnToggled(object sender, RoutedEventArgs e)
        {
            var task = await GetTaskAsync();
            if (task == null)
            {
                return;
            }

            if (Toggle.IsOn)
            {
                await task.RequestEnableAsync();
            }
            else
            {
                task.Disable();
            }

            OnLoaded();
        }

        private async Task<StartupTask> GetTaskAsync()
        {
            try
            {
                return await StartupTask.GetAsync("Telegram");
            }
            catch
            {
                return null;
            }
        }

        private void Minimized_Toggled(object sender, RoutedEventArgs e)
        {
            SettingsService.Current.IsLaunchMinimized = ToggleMinimized.IsOn;
        }
    }
}
