using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields
        
        SfListView listView;
        VisualContainer visualContainer;
        bool loaded;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            listView = bindable.FindByName<SfListView>("listView");
            visualContainer = listView.GetVisualContainer();
            visualContainer.PropertyChanged += Container_PropertyChanged;
            listView.Loaded += ListView_Loaded;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            listView.Loaded += ListView_Loaded;
            visualContainer.PropertyChanged += Container_PropertyChanged;
            visualContainer = null;
            listView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Callbacks
        private void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height" && listView.HeightRequest != visualContainer.Height && loaded)
                listView.HeightRequest = visualContainer.Height;
        }

        private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            var extent = (double)visualContainer.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "TotalExtent").GetValue(visualContainer);
            listView.HeightRequest = extent;
            loaded = true;
        }
        #endregion
    }
}