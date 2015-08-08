﻿using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class MainFrameViewModel : SingletonViewModelBase<MainFrameViewModel>
    {
        #region Hamburger Menu States
        private bool? _isHomeChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsHomeChecked
        {
            get { return _isHomeChecked; }
            set { _isHomeChecked = value; OnPropertyChanged(); }
        }

        private bool? _isMyBooksChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsMyBooksChecked
        {
            get { return _isMyBooksChecked; }
            set { _isMyBooksChecked = value; OnPropertyChanged(); }
        }

        private bool? _isFriendsChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsFriendsChecked
        {
            get { return _isFriendsChecked; }
            set { _isFriendsChecked = value; OnPropertyChanged(); }
        }

        private bool? _isProfileChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsProfileChecked
        {
            get { return _isProfileChecked; }
            set { _isProfileChecked = value; OnPropertyChanged(); }
        }

        private bool? _isSettingsChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsSettingsChecked
        {
            get { return _isSettingsChecked; }
            set { _isSettingsChecked = value; OnPropertyChanged(); }
        }
        #endregion

        private bool _isPaneOpen;
        /// <summary>
        /// 
        /// </summary>
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { _isPaneOpen = value; OnPropertyChanged();  }
        }

        public void MenuClick()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        public void HomeClick()
        {
        }
        public void MyBooksClick()
        {
        }
        public void FriendsClick()
        {
        }
        public void ProfileClick()
        {
        }
        public void SettingsClick()
        {
        }
    }
}