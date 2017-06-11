using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Vnap.ViewModels
{
    public interface IBaseViewModel
    {
        Task LoadAsync();
    }

    public class BaseViewModel : BindableBase, INavigationAware, IBaseViewModel
    {
        private string _title = string.Empty;
        private string _subTitle = string.Empty;
        private string _icon = string.Empty;
        private bool _isNotBusy = true;
        private bool _canLoadMore = true;
        private bool _isBusy;

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
                return this._title;
            }
            set
            {
                this.SetProperty(ref this._title, value);
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
                return this._subTitle;
            }
            set
            {
                this.SetProperty(ref this._subTitle, value);
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
                return this._icon;
            }
            set
            {
                this.SetProperty(ref this._icon, value);
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
                return this._isBusy;
            }
            set
            {
                if (!this.SetProperty(ref this._isBusy, value))
                    return;
                this.IsNotBusy = !this._isBusy;
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
                return this._isNotBusy;
            }
            private set
            {
                this.SetProperty(ref this._isNotBusy, value);
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
                return this._canLoadMore;
            }
            set
            {
                this.SetProperty(ref this._canLoadMore, value);
            }
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
