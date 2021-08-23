using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace UWPTest
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<ListModel> MyList = new ObservableCollection<ListModel>();


        public bool EditMode
        {
            get { return (bool)GetValue(EditViewProperty); }
            set
            {
                StaticHelper.EditView = value;
                Task.Run(() => BuildSource());
                SetValue(EditViewProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for EditView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditViewProperty =
            DependencyProperty.Register("EditView", typeof(bool), typeof(MainPage), new PropertyMetadata(false));

        private DataService _ds = new DataService();

        public MainPage()
        {
            this.InitializeComponent();
            _ds.LoadData();
            BuildSource();
        }

        private async void BuildSource()
        {
            var list = _ds.getCachedEntries();
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                 MyList.Clear();
                 foreach (var i in list)
                 {
                     MyList.Add(i);
                 }
            });
            await Task.CompletedTask;
        }
    }
}
