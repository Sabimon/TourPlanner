﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.ViewModels {
    public abstract class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null) {
            //ValidatePropertyName(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ValidatePropertyName(string propertyName) { //[CallerMemberName] , nameof()
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                throw new ArgumentException("Invalid property name: " + propertyName);
            }
        }
    }
}
