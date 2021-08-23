using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPTest
{
    public class Model
    {
        public string Text;

        public Model(string t) { Text = t; }
    }

    public class ListModel
    {
        public List<Model> ModelList = new List<Model>();
    }

    public class ModelSelector : DataTemplateSelector
    {
        public DataTemplate ViewTemplate { get; set; }
        public DataTemplate EditTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return StaticHelper.EditView ? EditTemplate : ViewTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return StaticHelper.EditView ? EditTemplate : ViewTemplate;
        }
    }
    static class StaticHelper
    {
        public static bool EditView { get; set; } = false;
    }

    public class DataService
    {
        private List<ListModel> _cache = new List<ListModel>();

        public void LoadData()
        {
            _cache.Clear();
            for (var i = 0; i < 5; i++)
            {
                ListModel nl = new ListModel();
                for (var k = 0; k < 10; k++)
                {
                    nl.ModelList.Add(new Model(i + "-" + k));
                }
                _cache.Add(nl);
            }
        }

        public List<ListModel> getCachedEntries()
        {
            return _cache;
        }
    }
}
