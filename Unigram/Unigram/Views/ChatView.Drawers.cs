using Telegram.Td.Api;
using Unigram.Common;
using Unigram.Converters;
using Unigram.ViewModels.Drawers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Unigram.Views
{
    public partial class ChatView
    {
        private Controls.MenuFlyoutMediaItem _previewFlyout;
        private Services.FileContext<StickerViewModel> _stickers = new Services.FileContext<StickerViewModel>();

        private void UpdateFileFyloutPreview(File file)
        {
            if (file.Local.IsDownloadingCompleted && 
                _stickers.TryGetValue(file.Id, out System.Collections.Generic.List<StickerViewModel> items) && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.UpdateFile(file);

                    if (_previewFlyout is Controls.MenuFlyoutMediaItem flyout)
                        flyout.UpdatePreview();
                }
                _stickers[file.Id].Clear();
            }
        }

        private void Sticker_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            var element = sender as FrameworkElement;
            var sticker = element.Tag as StickerViewModel;

            if (sticker == null)
            {
                return;
            }

            if (sticker.StickerValue is File file && // Download for preview
                !file.Local.IsDownloadingCompleted && 
                file.Local.CanBeDownloaded && !file.Local.IsDownloadingActive)
            {
                _stickers[file.Id].Add(sticker);
                sticker.ProtoService.DownloadFile(file.Id, 1);
            }

            var flyout = new MenuFlyout();
            _previewFlyout = new Controls.MenuFlyoutMediaItem(sticker);
            flyout.Items.Add(_previewFlyout);
            flyout.CreateFlyoutSeparator();
            flyout.CreateFlyoutItem(ViewModel.StickerViewCommand, (Sticker)sticker, Strings.Resources.ViewPackPreview, new FontIcon { Glyph = Icons.Stickers, FontFamily = Constants.SymbolThemeFontFamily });

            if (ViewModel.ProtoService.IsStickerFavorite(sticker.StickerValue.Id))
            {
                flyout.CreateFlyoutItem(ViewModel.StickerUnfaveCommand, (Sticker)sticker, Strings.Resources.DeleteFromFavorites, new FontIcon { Glyph = Icons.Unfavorite });
            }
            else
            {
                flyout.CreateFlyoutItem(ViewModel.StickerFaveCommand, (Sticker)sticker, Strings.Resources.AddToFavorites, new FontIcon { Glyph = Icons.Favorite });
            }

            if (ViewModel.Type == ViewModels.DialogType.History)
            {
                var chat = ViewModel.Chat;
                if (chat == null)
                {
                    return;
                }

                var self = ViewModel.CacheService.IsSavedMessages(chat);

                flyout.CreateFlyoutSeparator();
                flyout.CreateFlyoutItem(new RelayCommand<Sticker>(anim => ViewModel.StickerSendExecute(anim, null, true)), (Sticker)sticker, Strings.Resources.SendWithoutSound, new FontIcon { Glyph = Icons.Mute });
                //flyout.CreateFlyoutItem(new RelayCommand<Sticker>(anim => ViewModel.StickerSendExecute(anim, true, null)), sticker.Get(), self ? Strings.Resources.SetReminder : Strings.Resources.ScheduleMessage, new FontIcon { Glyph = Icons.Schedule });
                flyout.CreateFlyoutItem(new RelayCommand<Sticker>(anim => ViewModel.StickerSendExecute(anim)), (Sticker)sticker, Strings.Resources.Send, new FontIcon { Glyph = Icons.Send, FontFamily = Constants.TelegramThemeFontFamily });
            }

            args.ShowAt(flyout, element);
        }

        private void Animation_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            var element = sender as FrameworkElement;
            var animation = element.DataContext as Animation;

            if (animation == null)
            {
                return;
            }

            var flyout = new MenuFlyout();
            flyout.Items.Add(new Controls.MenuFlyoutMediaItem(animation));
            flyout.CreateFlyoutSeparator();

            if (ViewModel.ProtoService.IsAnimationSaved(animation.AnimationValue.Id))
            {
                flyout.CreateFlyoutItem(ViewModel.AnimationDeleteCommand, animation, Strings.Resources.Delete, new FontIcon { Glyph = Icons.Delete });
            }
            else
            {
                flyout.CreateFlyoutItem(ViewModel.AnimationSaveCommand, animation, Strings.Resources.SaveToGIFs, new FontIcon { Glyph = Icons.Animations, FontFamily = Constants.SymbolThemeFontFamily });
            }

            if (ViewModel.Type == ViewModels.DialogType.History)
            {
                var chat = ViewModel.Chat;
                if (chat == null)
                {
                    return;
                }

                var self = ViewModel.CacheService.IsSavedMessages(chat);

                flyout.CreateFlyoutSeparator();
                flyout.CreateFlyoutItem(new RelayCommand<Animation>(anim => ViewModel.AnimationSendExecute(anim, null, true)), animation, Strings.Resources.SendWithoutSound, new FontIcon { Glyph = Icons.Mute });
                //flyout.CreateFlyoutItem(new RelayCommand<Animation>(anim => ViewModel.AnimationSendExecute(anim, true, null)), animation, self ? Strings.Resources.SetReminder : Strings.Resources.ScheduleMessage, new FontIcon { Glyph = Icons.Schedule });
                flyout.CreateFlyoutItem(new RelayCommand<Animation>(anim => ViewModel.AnimationSendExecute(anim)), animation, Strings.Resources.Send, new FontIcon { Glyph = Icons.Send, FontFamily = Constants.TelegramThemeFontFamily });
            }

            args.ShowAt(flyout, element);
        }
    }
}
