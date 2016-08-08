using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Vnap.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        private string title = string.Empty;
        private string subTitle = string.Empty;
        private string icon = string.Empty;
        private bool isNotBusy = true;
        private bool canLoadMore = true;
        private bool isBusy;

        /// <summary>
        /// Gets or sets the title.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.SetProperty(ref this.title, value);
            }
        }

        /// <summary>
        /// Gets or sets the subtitle.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The subtitle.
        /// </value>
        public string Subtitle
        {
            get
            {
                return this.subTitle;
            }
            set
            {
                this.SetProperty(ref this.subTitle, value);
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The icon.
        /// </value>
        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.SetProperty(ref this.icon, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                if (!this.SetProperty(ref this.isBusy, value))
                    return;
                this.IsNotBusy = !this.isBusy;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is not busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotBusy
        {
            get
            {
                return this.isNotBusy;
            }
            private set
            {
                this.SetProperty(ref this.isNotBusy, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance can load more; otherwise, <c>false</c>.
        /// </value>
        public bool CanLoadMore
        {
            get
            {
                return this.canLoadMore;
            }
            set
            {
                this.SetProperty(ref this.canLoadMore, value);
            }
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
