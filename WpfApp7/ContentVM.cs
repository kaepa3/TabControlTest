using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp7
{
    public class ContentVM
    {
        public ReactiveProperty<string> Content { get; set; } = new ReactiveProperty<string>("testing");
    }
}
