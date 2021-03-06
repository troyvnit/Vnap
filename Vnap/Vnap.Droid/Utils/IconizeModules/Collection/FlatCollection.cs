using System;
using System.Collections.Generic;
using Plugin.Iconize;

namespace Vnap.Droid.Utils.IconizeModules.Collection
{
    public static class FlatCollection
    {
        /// <summary>
        /// Gets the icons.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The icons.
        /// 
        /// </value>
        public static IList<IIcon> Icons { get; } = (IList<IIcon>)new List<IIcon>();

        static FlatCollection()
        {
            var iconDictionaty = new Dictionary<string, char>()
            {
                {"flaticon-add", '\xf100'},
                {"flaticon-add-1", '\xf101'},
                {"flaticon-add-2", '\xf102'},
                {"flaticon-add-3", '\xf103'},
                {"flaticon-agenda", '\xf104'},
                {"flaticon-alarm", '\xf105'},
                {"flaticon-alarm-1", '\xf106'},
                {"flaticon-alarm-clock", '\xf107'},
                {"flaticon-alarm-clock-1", '\xf108'},
                {"flaticon-albums", '\xf109'},
                {"flaticon-app", '\xf10a'},
                {"flaticon-archive", '\xf10b'},
                {"flaticon-archive-1", '\xf10c'},
                {"flaticon-archive-2", '\xf10d'},
                {"flaticon-archive-3", '\xf10e'},
                {"flaticon-attachment", '\xf10f'},
                {"flaticon-back", '\xf110'},
                {"flaticon-battery", '\xf111'},
                {"flaticon-battery-1", '\xf112'},
                {"flaticon-battery-2", '\xf113'},
                {"flaticon-battery-3", '\xf114'},
                {"flaticon-battery-4", '\xf115'},
                {"flaticon-battery-5", '\xf116'},
                {"flaticon-battery-6", '\xf117'},
                {"flaticon-battery-7", '\xf118'},
                {"flaticon-battery-8", '\xf119'},
                {"flaticon-battery-9", '\xf11a'},
                {"flaticon-binoculars", '\xf11b'},
                {"flaticon-blueprint", '\xf11c'},
                {"flaticon-bluetooth", '\xf11d'},
                {"flaticon-bluetooth-1", '\xf11e'},
                {"flaticon-bookmark", '\xf11f'},
                {"flaticon-bookmark-1", '\xf120'},
                {"flaticon-briefcase", '\xf121'},
                {"flaticon-broken-link", '\xf122'},
                {"flaticon-calculator", '\xf123'},
                {"flaticon-calculator-1", '\xf124'},
                {"flaticon-calendar", '\xf125'},
                {"flaticon-calendar-1", '\xf126'},
                {"flaticon-calendar-2", '\xf127'},
                {"flaticon-calendar-3", '\xf128'},
                {"flaticon-calendar-4", '\xf129'},
                {"flaticon-calendar-5", '\xf12a'},
                {"flaticon-calendar-6", '\xf12b'},
                {"flaticon-calendar-7", '\xf12c'},
                {"flaticon-checked", '\xf12d'},
                {"flaticon-checked-1", '\xf12e'},
                {"flaticon-clock", '\xf12f'},
                {"flaticon-clock-1", '\xf130'},
                {"flaticon-close", '\xf131'},
                {"flaticon-cloud", '\xf132'},
                {"flaticon-cloud-computing", '\xf133'},
                {"flaticon-cloud-computing-1", '\xf134'},
                {"flaticon-cloud-computing-2", '\xf135'},
                {"flaticon-cloud-computing-3", '\xf136'},
                {"flaticon-cloud-computing-4", '\xf137'},
                {"flaticon-cloud-computing-5", '\xf138'},
                {"flaticon-command", '\xf139'},
                {"flaticon-compact-disc", '\xf13a'},
                {"flaticon-compact-disc-1", '\xf13b'},
                {"flaticon-compact-disc-2", '\xf13c'},
                {"flaticon-compass", '\xf13d'},
                {"flaticon-compose", '\xf13e'},
                {"flaticon-controls", '\xf13f'},
                {"flaticon-controls-1", '\xf140'},
                {"flaticon-controls-2", '\xf141'},
                {"flaticon-controls-3", '\xf142'},
                {"flaticon-controls-4", '\xf143'},
                {"flaticon-controls-5", '\xf144'},
                {"flaticon-controls-6", '\xf145'},
                {"flaticon-controls-7", '\xf146'},
                {"flaticon-controls-8", '\xf147'},
                {"flaticon-controls-9", '\xf148'},
                {"flaticon-database", '\xf149'},
                {"flaticon-database-1", '\xf14a'},
                {"flaticon-database-2", '\xf14b'},
                {"flaticon-database-3", '\xf14c'},
                {"flaticon-diamond", '\xf14d'},
                {"flaticon-diploma", '\xf14e'},
                {"flaticon-dislike", '\xf14f'},
                {"flaticon-dislike-1", '\xf150'},
                {"flaticon-divide", '\xf151'},
                {"flaticon-divide-1", '\xf152'},
                {"flaticon-division", '\xf153'},
                {"flaticon-document", '\xf154'},
                {"flaticon-download", '\xf155'},
                {"flaticon-edit", '\xf156'},
                {"flaticon-edit-1", '\xf157'},
                {"flaticon-eject", '\xf158'},
                {"flaticon-eject-1", '\xf159'},
                {"flaticon-equal", '\xf15a'},
                {"flaticon-equal-1", '\xf15b'},
                {"flaticon-equal-2", '\xf15c'},
                {"flaticon-error", '\xf15d'},
                {"flaticon-exit", '\xf15e'},
                {"flaticon-exit-1", '\xf15f'},
                {"flaticon-exit-2", '\xf160'},
                {"flaticon-eyeglasses", '\xf161'},
                {"flaticon-fast-forward", '\xf162'},
                {"flaticon-fast-forward-1", '\xf163'},
                {"flaticon-fax", '\xf164'},
                {"flaticon-file", '\xf165'},
                {"flaticon-file-1", '\xf166'},
                {"flaticon-file-2", '\xf167'},
                {"flaticon-film", '\xf168'},
                {"flaticon-fingerprint", '\xf169'},
                {"flaticon-flag", '\xf16a'},
                {"flaticon-flag-1", '\xf16b'},
                {"flaticon-flag-2", '\xf16c'},
                {"flaticon-flag-3", '\xf16d'},
                {"flaticon-flag-4", '\xf16e'},
                {"flaticon-focus", '\xf16f'},
                {"flaticon-folder", '\xf170'},
                {"flaticon-folder-1", '\xf171'},
                {"flaticon-folder-10", '\xf172'},
                {"flaticon-folder-11", '\xf173'},
                {"flaticon-folder-12", '\xf174'},
                {"flaticon-folder-13", '\xf175'},
                {"flaticon-folder-14", '\xf176'},
                {"flaticon-folder-15", '\xf177'},
                {"flaticon-folder-16", '\xf178'},
                {"flaticon-folder-17", '\xf179'},
                {"flaticon-folder-18", '\xf17a'},
                {"flaticon-folder-19", '\xf17b'},
                {"flaticon-folder-2", '\xf17c'},
                {"flaticon-folder-3", '\xf17d'},
                {"flaticon-folder-4", '\xf17e'},
                {"flaticon-folder-5", '\xf17f'},
                {"flaticon-folder-6", '\xf180'},
                {"flaticon-folder-7", '\xf181'},
                {"flaticon-folder-8", '\xf182'},
                {"flaticon-folder-9", '\xf183'},
                {"flaticon-forbidden", '\xf184'},
                {"flaticon-funnel", '\xf185'},
                {"flaticon-garbage", '\xf186'},
                {"flaticon-garbage-1", '\xf187'},
                {"flaticon-garbage-2", '\xf188'},
                {"flaticon-gift", '\xf189'},
                {"flaticon-help", '\xf18a'},
                {"flaticon-hide", '\xf18b'},
                {"flaticon-hold", '\xf18c'},
                {"flaticon-home", '\xf18d'},
                {"flaticon-home-1", '\xf18e'},
                {"flaticon-home-2", '\xf18f'},
                {"flaticon-hourglass", '\xf190'},
                {"flaticon-hourglass-1", '\xf191'},
                {"flaticon-hourglass-2", '\xf192'},
                {"flaticon-hourglass-3", '\xf193'},
                {"flaticon-house", '\xf194'},
                {"flaticon-id-card", '\xf195'},
                {"flaticon-id-card-1", '\xf196'},
                {"flaticon-id-card-2", '\xf197'},
                {"flaticon-id-card-3", '\xf198'},
                {"flaticon-id-card-4", '\xf199'},
                {"flaticon-id-card-5", '\xf19a'},
                {"flaticon-idea", '\xf19b'},
                {"flaticon-incoming", '\xf19c'},
                {"flaticon-infinity", '\xf19d'},
                {"flaticon-info", '\xf19e'},
                {"flaticon-internet", '\xf19f'},
                {"flaticon-key", '\xf1a0'},
                {"flaticon-lamp", '\xf1a1'},
                {"flaticon-layers", '\xf1a2'},
                {"flaticon-layers-1", '\xf1a3'},
                {"flaticon-like", '\xf1a4'},
                {"flaticon-like-1", '\xf1a5'},
                {"flaticon-like-2", '\xf1a6'},
                {"flaticon-link", '\xf1a7'},
                {"flaticon-list", '\xf1a8'},
                {"flaticon-list-1", '\xf1a9'},
                {"flaticon-lock", '\xf1aa'},
                {"flaticon-lock-1", '\xf1ab'},
                {"flaticon-locked", '\xf1ac'},
                {"flaticon-locked-1", '\xf1ad'},
                {"flaticon-locked-2", '\xf1ae'},
                {"flaticon-locked-3", '\xf1af'},
                {"flaticon-locked-4", '\xf1b0'},
                {"flaticon-locked-5", '\xf1b1'},
                {"flaticon-locked-6", '\xf1b2'},
                {"flaticon-login", '\xf1b3'},
                {"flaticon-magic-wand", '\xf1b4'},
                {"flaticon-magnet", '\xf1b5'},
                {"flaticon-magnet-1", '\xf1b6'},
                {"flaticon-magnet-2", '\xf1b7'},
                {"flaticon-map", '\xf1b8'},
                {"flaticon-map-1", '\xf1b9'},
                {"flaticon-map-2", '\xf1ba'},
                {"flaticon-map-location", '\xf1bb'},
                {"flaticon-megaphone", '\xf1bc'},
                {"flaticon-megaphone-1", '\xf1bd'},
                {"flaticon-menu", '\xf1be'},
                {"flaticon-menu-1", '\xf1bf'},
                {"flaticon-menu-2", '\xf1c0'},
                {"flaticon-menu-3", '\xf1c1'},
                {"flaticon-menu-4", '\xf1c2'},
                {"flaticon-microphone", '\xf1c3'},
                {"flaticon-microphone-1", '\xf1c4'},
                {"flaticon-minus", '\xf1c5'},
                {"flaticon-minus-1", '\xf1c6'},
                {"flaticon-more", '\xf1c7'},
                {"flaticon-more-1", '\xf1c8'},
                {"flaticon-more-2", '\xf1c9'},
                {"flaticon-multiply", '\xf1ca'},
                {"flaticon-multiply-1", '\xf1cb'},
                {"flaticon-music-player", '\xf1cc'},
                {"flaticon-music-player-1", '\xf1cd'},
                {"flaticon-music-player-2", '\xf1ce'},
                {"flaticon-music-player-3", '\xf1cf'},
                {"flaticon-mute", '\xf1d0'},
                {"flaticon-muted", '\xf1d1'},
                {"flaticon-navigation", '\xf1d2'},
                {"flaticon-navigation-1", '\xf1d3'},
                {"flaticon-network", '\xf1d4'},
                {"flaticon-newspaper", '\xf1d5'},
                {"flaticon-next", '\xf1d6'},
                {"flaticon-note", '\xf1d7'},
                {"flaticon-notebook", '\xf1d8'},
                {"flaticon-notebook-1", '\xf1d9'},
                {"flaticon-notebook-2", '\xf1da'},
                {"flaticon-notebook-3", '\xf1db'},
                {"flaticon-notebook-4", '\xf1dc'},
                {"flaticon-notebook-5", '\xf1dd'},
                {"flaticon-notepad", '\xf1de'},
                {"flaticon-notepad-1", '\xf1df'},
                {"flaticon-notepad-2", '\xf1e0'},
                {"flaticon-notification", '\xf1e1'},
                {"flaticon-paper-plane", '\xf1e2'},
                {"flaticon-paper-plane-1", '\xf1e3'},
                {"flaticon-pause", '\xf1e4'},
                {"flaticon-pause-1", '\xf1e5'},
                {"flaticon-percent", '\xf1e6'},
                {"flaticon-percent-1", '\xf1e7'},
                {"flaticon-perspective", '\xf1e8'},
                {"flaticon-photo-camera", '\xf1e9'},
                {"flaticon-photo-camera-1", '\xf1ea'},
                {"flaticon-photos", '\xf1eb'},
                {"flaticon-picture", '\xf1ec'},
                {"flaticon-picture-1", '\xf1ed'},
                {"flaticon-picture-2", '\xf1ee'},
                {"flaticon-pin", '\xf1ef'},
                {"flaticon-placeholder", '\xf1f0'},
                {"flaticon-placeholder-1", '\xf1f1'},
                {"flaticon-placeholder-2", '\xf1f2'},
                {"flaticon-placeholder-3", '\xf1f3'},
                {"flaticon-placeholders", '\xf1f4'},
                {"flaticon-play-button", '\xf1f5'},
                {"flaticon-play-button-1", '\xf1f6'},
                {"flaticon-plus", '\xf1f7'},
                {"flaticon-power", '\xf1f8'},
                {"flaticon-previous", '\xf1f9'},
                {"flaticon-price-tag", '\xf1fa'},
                {"flaticon-print", '\xf1fb'},
                {"flaticon-push-pin", '\xf1fc'},
                {"flaticon-radar", '\xf1fd'},
                {"flaticon-reading", '\xf1fe'},
                {"flaticon-record", '\xf1ff'},
                {"flaticon-repeat", '\xf200'},
                {"flaticon-repeat-1", '\xf201'},
                {"flaticon-restart", '\xf202'},
                {"flaticon-resume", '\xf203'},
                {"flaticon-rewind", '\xf204'},
                {"flaticon-rewind-1", '\xf205'},
                {"flaticon-route", '\xf206'},
                {"flaticon-save", '\xf207'},
                {"flaticon-search", '\xf208'},
                {"flaticon-search-1", '\xf209'},
                {"flaticon-send", '\xf20a'},
                {"flaticon-server", '\xf20b'},
                {"flaticon-server-1", '\xf20c'},
                {"flaticon-server-2", '\xf20d'},
                {"flaticon-server-3", '\xf20e'},
                {"flaticon-settings", '\xf20f'},
                {"flaticon-settings-1", '\xf210'},
                {"flaticon-settings-2", '\xf211'},
                {"flaticon-settings-3", '\xf212'},
                {"flaticon-settings-4", '\xf213'},
                {"flaticon-settings-5", '\xf214'},
                {"flaticon-settings-6", '\xf215'},
                {"flaticon-settings-7", '\xf216'},
                {"flaticon-settings-8", '\xf217'},
                {"flaticon-settings-9", '\xf218'},
                {"flaticon-share", '\xf219'},
                {"flaticon-share-1", '\xf21a'},
                {"flaticon-share-2", '\xf21b'},
                {"flaticon-shuffle", '\xf21c'},
                {"flaticon-shuffle-1", '\xf21d'},
                {"flaticon-shutdown", '\xf21e'},
                {"flaticon-sign", '\xf21f'},
                {"flaticon-sign-1", '\xf220'},
                {"flaticon-skip", '\xf221'},
                {"flaticon-smartphone", '\xf222'},
                {"flaticon-smartphone-1", '\xf223'},
                {"flaticon-smartphone-10", '\xf224'},
                {"flaticon-smartphone-11", '\xf225'},
                {"flaticon-smartphone-2", '\xf226'},
                {"flaticon-smartphone-3", '\xf227'},
                {"flaticon-smartphone-4", '\xf228'},
                {"flaticon-smartphone-5", '\xf229'},
                {"flaticon-smartphone-6", '\xf22a'},
                {"flaticon-smartphone-7", '\xf22b'},
                {"flaticon-smartphone-8", '\xf22c'},
                {"flaticon-smartphone-9", '\xf22d'},
                {"flaticon-speaker", '\xf22e'},
                {"flaticon-speaker-1", '\xf22f'},
                {"flaticon-speaker-2", '\xf230'},
                {"flaticon-speaker-3", '\xf231'},
                {"flaticon-speaker-4", '\xf232'},
                {"flaticon-speaker-5", '\xf233'},
                {"flaticon-speaker-6", '\xf234'},
                {"flaticon-speaker-7", '\xf235'},
                {"flaticon-speaker-8", '\xf236'},
                {"flaticon-spotlight", '\xf237'},
                {"flaticon-star", '\xf238'},
                {"flaticon-star-1", '\xf239'},
                {"flaticon-stop", '\xf23a'},
                {"flaticon-stop-1", '\xf23b'},
                {"flaticon-stopwatch", '\xf23c'},
                {"flaticon-stopwatch-1", '\xf23d'},
                {"flaticon-stopwatch-2", '\xf23e'},
                {"flaticon-stopwatch-3", '\xf23f'},
                {"flaticon-stopwatch-4", '\xf240'},
                {"flaticon-street", '\xf241'},
                {"flaticon-street-1", '\xf242'},
                {"flaticon-substract", '\xf243'},
                {"flaticon-substract-1", '\xf244'},
                {"flaticon-success", '\xf245'},
                {"flaticon-switch", '\xf246'},
                {"flaticon-switch-1", '\xf247'},
                {"flaticon-switch-2", '\xf248'},
                {"flaticon-switch-3", '\xf249'},
                {"flaticon-switch-4", '\xf24a'},
                {"flaticon-switch-5", '\xf24b'},
                {"flaticon-switch-6", '\xf24c'},
                {"flaticon-switch-7", '\xf24d'},
                {"flaticon-tabs", '\xf24e'},
                {"flaticon-tabs-1", '\xf24f'},
                {"flaticon-target", '\xf250'},
                {"flaticon-television", '\xf251'},
                {"flaticon-television-1", '\xf252'},
                {"flaticon-time", '\xf253'},
                {"flaticon-trash", '\xf254'},
                {"flaticon-umbrella", '\xf255'},
                {"flaticon-unlink", '\xf256'},
                {"flaticon-unlocked", '\xf257'},
                {"flaticon-unlocked-1", '\xf258'},
                {"flaticon-unlocked-2", '\xf259'},
                {"flaticon-upload", '\xf25a'},
                {"flaticon-user", '\xf25b'},
                {"flaticon-user-1", '\xf25c'},
                {"flaticon-user-2", '\xf25d'},
                {"flaticon-user-3", '\xf25e'},
                {"flaticon-user-4", '\xf25f'},
                {"flaticon-user-5", '\xf260'},
                {"flaticon-user-6", '\xf261'},
                {"flaticon-user-7", '\xf262'},
                {"flaticon-users", '\xf263'},
                {"flaticon-users-1", '\xf264'},
                {"flaticon-video-camera", '\xf265'},
                {"flaticon-video-camera-1", '\xf266'},
                {"flaticon-video-player", '\xf267'},
                {"flaticon-video-player-1", '\xf268'},
                {"flaticon-video-player-2", '\xf269'},
                {"flaticon-view", '\xf26a'},
                {"flaticon-view-1", '\xf26b'},
                {"flaticon-view-2", '\xf26c'},
                {"flaticon-volume-control", '\xf26d'},
                {"flaticon-volume-control-1", '\xf26e'},
                {"flaticon-warning", '\xf26f'},
                {"flaticon-wifi", '\xf270'},
                {"flaticon-wifi-1", '\xf271'},
                {"flaticon-windows", '\xf272'},
                {"flaticon-windows-1", '\xf273'},
                {"flaticon-windows-2", '\xf274'},
                {"flaticon-windows-3", '\xf275'},
                {"flaticon-windows-4", '\xf276'},
                {"flaticon-wireless-internet", '\xf277'},
                {"flaticon-worldwide", '\xf278'},
                {"flaticon-worldwide-1", '\xf279'},
                {"flaticon-zoom-in", '\xf27a'},
                {"flaticon-zoom-out", '\xf27b'}
            };
            foreach (var icon in iconDictionaty)
            {
                Icons.Add(icon.Key, icon.Value);
            }
        }
    }
}