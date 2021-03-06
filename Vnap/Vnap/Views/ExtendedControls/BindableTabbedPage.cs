﻿using System.Collections;
using System.Collections.Generic;
using FormsPlugin.Iconize;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Vnap.Views.ExtendedControls
{
    public class BindableTabbedPage : IconTabbedPage
    {
        public BindableTabbedPage()
        {

        }


        public static BindableProperty ChildrenListProperty = BindableProperty.Create("ChildrenList",
            typeof(IList), typeof(BindableTabbedPage),
            null,
            BindingMode.Default,
            null,
            new BindableProperty.BindingPropertyChangedDelegate(UpdateList));

        public IList<Page> ChildrenList
        {
            get { return (IList<Page>)GetValue(ChildrenListProperty); }
            set
            {
                SetValue(ChildrenListProperty, value);
            }
        }

        private static void UpdateList(BindableObject bindable, object oldvalue, object newvalue)
        {

            BindableTabbedPage bindableTabbedPage = bindable as BindableTabbedPage;
            if (bindableTabbedPage == null)
            {
                return;
            }

            bindableTabbedPage.Children.Clear();

            IList<Page> pageList = (IList<Page>)newvalue;

            if (pageList == null || pageList.Count == 0)
            {
                return;
            }

            foreach (Page page in pageList)
            {
                bindableTabbedPage.Children.Add(page);
            }
        }

    }
}
