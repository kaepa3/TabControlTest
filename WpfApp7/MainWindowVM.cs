using Livet;
using System;
using System.Collections.Generic;
using System.Text;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Windows;

namespace WpfApp7
{
    public class MainWindowVM : ViewModel
    {
        public ReactiveCollection<TabItemVM> TabItems { get; set; } = new ReactiveCollection<TabItemVM>();
        public ReactiveCommand<object> PushCommand { get; set; } = new ReactiveCommand<object>();
        public ReactiveProperty<string> Texts { get; set; } = new ReactiveProperty<string>();
         
        public MainWindowVM()
        {
            PushCommand.Subscribe(x => PushAction(x)).AddTo(this.CompositeDisposable);
            foreach (var item in new List<TabItemVM>()
            {
                new TabItemVM() { Header="item1"},
                new TabItemVM() { Header="item2"},
            })
            {
                TabItems.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        private void PushAction(object x)
        {

            if (x is TabControl tab)
            {
                var sb = new StringBuilder();
                foreach (var obj in ListChildVisualTree(tab))
                {
                    if (obj is TabItem ti)
                    {
                        var header = "";
                        if (ti.Header is TabItemVM vm)
                        {
                            header = vm.Header;
                        }
                        sb.Append($"{obj.GetType().Name}:{header}" + Environment.NewLine);
                    }
                    else if (obj is SelectTextBox st)
                    {
                        sb.Append($"{st.GetType().Name}=>{st.IsChoiced}" + Environment.NewLine);
                    }
                    else
                    {
                        sb.Append(obj.GetType().Name + Environment.NewLine);
                    }

                }
                Texts.Value = sb.ToString();
            }
            else
            {
                Texts.Value = "push";
            }
        }

        IEnumerable<DependencyObject> ListChildVisualTree(DependencyObject obj)
        {
            foreach (var index in Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(obj)))
            {
                var child = VisualTreeHelper.GetChild(obj, index);
                yield return child;
                foreach (var v in ListChildVisualTree(child))
                {
                    yield return v;
                }
            }
        }
    }
}
