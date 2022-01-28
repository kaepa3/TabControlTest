namespace WpfApp7
{
    public class TabItemVM
    {
        public string Header { get; set; }
        public ContentVM DisplayContent { get; set; } = new ContentVM();
    }
}