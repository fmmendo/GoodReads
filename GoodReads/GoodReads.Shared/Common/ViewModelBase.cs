using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GoodReads.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected Action<string> Message;
        protected Func<string, string, bool> Confirm;

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetMessageBox(Action<string> popup)
        {
            Message = popup;
        }


        public void SetConfirmMessageBox(Func<string, string, bool> confirm)
        {
            Confirm = confirm;
        }


        public void ShowMessage(string msg)
        {
            if (Message != null)
                Message(msg);
        }


        public bool ShowMessageWithConfirmation(string caption, string msg)
        {
            if (Confirm != null)
                return Confirm(msg, caption);


            return false;
        }


        public void NotifyPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


        public string AppName { get { return "My Shelf"; } }
    }


}
