using System;
using System.Collections.Generic;
using Plugin.Iconize;

namespace Vnap.Droid.Utils.IconizeModules.Collection
{
    public static class LinearCollection
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

        static LinearCollection()
        {
            var iconDictionaty = new Dictionary<string, char>()
            {
                {"lnr-home", '\xe800'},{"lnr-apartment", '\xe801'},{"lnr-pencil", '\xe802'},{"lnr-magic-wand", '\xe803'},{"lnr-drop", '\xe804'},{"lnr-lighter", '\xe805'},{"lnr-poop", '\xe806'},{"lnr-sun", '\xe807'},{"lnr-moon", '\xe808'},{"lnr-cloud", '\xe809'},{"lnr-cloud-upload", '\xe80a'},{"lnr-cloud-download", '\xe80b'},{"lnr-cloud-sync", '\xe80c'},{"lnr-cloud-check", '\xe80d'},{"lnr-database", '\xe80e'},{"lnr-lock", '\xe80f'},{"lnr-cog", '\xe810'},{"lnr-trash", '\xe811'},{"lnr-dice", '\xe812'},{"lnr-heart", '\xe813'},{"lnr-star", '\xe814'},{"lnr-star-half", '\xe815'},{"lnr-star-empty", '\xe816'},{"lnr-flag", '\xe817'},{"lnr-envelope", '\xe818'},{"lnr-paperclip", '\xe819'},{"lnr-inbox", '\xe81a'},{"lnr-eye", '\xe81b'},{"lnr-printer", '\xe81c'},{"lnr-file-empty", '\xe81d'},{"lnr-file-add", '\xe81e'},{"lnr-enter", '\xe81f'},{"lnr-exit", '\xe820'},{"lnr-graduation-hat", '\xe821'},{"lnr-license", '\xe822'},{"lnr-music-note", '\xe823'},{"lnr-film-play", '\xe824'},{"lnr-camera-video", '\xe825'},{"lnr-camera", '\xe826'},{"lnr-picture", '\xe827'},{"lnr-book", '\xe828'},{"lnr-bookmark", '\xe829'},{"lnr-user", '\xe82a'},{"lnr-users", '\xe82b'},{"lnr-shirt", '\xe82c'},{"lnr-store", '\xe82d'},{"lnr-cart", '\xe82e'},{"lnr-tag", '\xe82f'},{"lnr-phone-handset", '\xe830'},{"lnr-phone", '\xe831'},{"lnr-pushpin", '\xe832'},{"lnr-map-marker", '\xe833'},{"lnr-map", '\xe834'},{"lnr-location", '\xe835'},{"lnr-calendar-full", '\xe836'},{"lnr-keyboard", '\xe837'},{"lnr-spell-check", '\xe838'},{"lnr-screen", '\xe839'},{"lnr-smartphone", '\xe83a'},{"lnr-tablet", '\xe83b'},{"lnr-laptop", '\xe83c'},{"lnr-laptop-phone", '\xe83d'},{"lnr-power-switch", '\xe83e'},{"lnr-bubble", '\xe83f'},{"lnr-heart-pulse", '\xe840'},{"lnr-construction", '\xe841'},{"lnr-pie-chart", '\xe842'},{"lnr-chart-bars", '\xe843'},{"lnr-gift", '\xe844'},{"lnr-diamond", '\xe845'},{"lnr-linearicons", '\xe846'},{"lnr-dinner", '\xe847'},{"lnr-coffee-cup", '\xe848'},{"lnr-leaf", '\xe849'},{"lnr-paw", '\xe84a'},{"lnr-rocket", '\xe84b'},{"lnr-briefcase", '\xe84c'},{"lnr-bus", '\xe84d'},{"lnr-car", '\xe84e'},{"lnr-train", '\xe84f'},{"lnr-bicycle", '\xe850'},{"lnr-wheelchair", '\xe851'},{"lnr-select", '\xe852'},{"lnr-earth", '\xe853'},{"lnr-smile", '\xe854'},{"lnr-sad", '\xe855'},{"lnr-neutral", '\xe856'},{"lnr-mustache", '\xe857'},{"lnr-alarm", '\xe858'},{"lnr-bullhorn", '\xe859'},{"lnr-volume-high", '\xe85a'},{"lnr-volume-medium", '\xe85b'},{"lnr-volume-low", '\xe85c'},{"lnr-volume", '\xe85d'},{"lnr-mic", '\xe85e'},{"lnr-hourglass", '\xe85f'},{"lnr-undo", '\xe860'},{"lnr-redo", '\xe861'},{"lnr-sync", '\xe862'},{"lnr-history", '\xe863'},{"lnr-clock", '\xe864'},{"lnr-download", '\xe865'},{"lnr-upload", '\xe866'},{"lnr-enter-down", '\xe867'},{"lnr-exit-up", '\xe868'},{"lnr-bug", '\xe869'},{"lnr-code", '\xe86a'},{"lnr-link", '\xe86b'},{"lnr-unlink", '\xe86c'},{"lnr-thumbs-up", '\xe86d'},{"lnr-thumbs-down", '\xe86e'},{"lnr-magnifier", '\xe86f'},{"lnr-cross", '\xe870'},{"lnr-menu", '\xe871'},{"lnr-list", '\xe872'},{"lnr-chevron-up", '\xe873'},{"lnr-chevron-down", '\xe874'},{"lnr-chevron-left", '\xe875'},{"lnr-chevron-right", '\xe876'},{"lnr-arrow-up", '\xe877'},{"lnr-arrow-down", '\xe878'},{"lnr-arrow-left", '\xe879'},{"lnr-arrow-right", '\xe87a'},{"lnr-move", '\xe87b'},{"lnr-warning", '\xe87c'},{"lnr-question-circle", '\xe87d'},{"lnr-menu-circle", '\xe87e'},{"lnr-checkmark-circle", '\xe87f'},{"lnr-cross-circle", '\xe880'},{"lnr-plus-circle", '\xe881'},{"lnr-circle-minus", '\xe882'},{"lnr-arrow-up-circle", '\xe883'},{"lnr-arrow-down-circle", '\xe884'},{"lnr-arrow-left-circle", '\xe885'},{"lnr-arrow-right-circle", '\xe886'},{"lnr-chevron-up-circle", '\xe887'},{"lnr-chevron-down-circle", '\xe888'},{"lnr-chevron-left-circle", '\xe889'},{"lnr-chevron-right-circle", '\xe88a'},{"lnr-crop", '\xe88b'},{"lnr-frame-expand", '\xe88c'},{"lnr-frame-contract", '\xe88d'},{"lnr-layers", '\xe88e'},{"lnr-funnel", '\xe88f'},{"lnr-text-format", '\xe890'},{"lnr-text-format-remove", '\xe891'},{"lnr-text-size", '\xe892'},{"lnr-bold", '\xe893'},{"lnr-italic", '\xe894'},{"lnr-underline", '\xe895'},{"lnr-strikethrough", '\xe896'},{"lnr-highlight", '\xe897'},{"lnr-text-align-left", '\xe898'},{"lnr-text-align-center", '\xe899'},{"lnr-text-align-right", '\xe89a'},{"lnr-text-align-justify", '\xe89b'},{"lnr-line-spacing", '\xe89c'},{"lnr-indent-increase", '\xe89d'},{"lnr-indent-decrease", '\xe89e'},{"lnr-pilcrow", '\xe89f'},{"lnr-direction-ltr", '\xe8a0'},{"lnr-direction-rtl", '\xe8a1'},{"lnr-page-break", '\xe8a2'},{"lnr-sort-alpha-asc", '\xe8a3'},{"lnr-sort-amount-asc", '\xe8a4'},{"lnr-hand", '\xe8a5'},{"lnr-pointer-up", '\xe8a6'},{"lnr-pointer-right", '\xe8a7'},{"lnr-pointer-down", '\xe8a8'},{"lnr-pointer-left", '\xe8a9'}
            };
            foreach (var icon in iconDictionaty)
            {
                Icons.Add(icon.Key, icon.Value);
            }
        }
    }
}