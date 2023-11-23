using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Xsl;


namespace laab2;

public partial class MainPage : ContentPage
{
    MainPageViewModel mainPageViewModel;
    XMLDataHandlers dataHandlers;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = mainPageViewModel = new MainPageViewModel();
        dataHandlers = new();
        UpdateXmlContent();
    }

    void Search_Submit(Object sender, EventArgs e)
    {

        if (Linq.IsChecked)
        {
            var data = from @event in mainPageViewModel.StudentItems.AsEnumerable()
                       where @event.Name.Contains(string.IsNullOrWhiteSpace(Name.Text) ? "" : Name.Text)
                       where @event.Faculty.Contains(string.IsNullOrWhiteSpace(Faculty.Text) ? "" : Faculty.Text)
                       where @event.Department.Contains(string.IsNullOrWhiteSpace(Department.Text) ? "" : Department.Text)
                       where @event.Specialty.Contains(string.IsNullOrWhiteSpace(Specialty.Text) ? "" : Specialty.Text)
                       where @event.Timing.Contains(string.IsNullOrWhiteSpace(Timing.Text) ? "" : Timing.Text)
                       select @event;

            MyCollectionView.ItemsSource = data;
        }

        if (Sax.IsChecked)
        {
            dataHandlers = new XMLDataHandlers();
            MyCollectionView.ItemsSource = dataHandlers.SAX_Search(new MainPageViewModel.StudentParliament
            {
                Name = string.IsNullOrWhiteSpace(Name.Text) ? "" : Name.Text,
                Faculty = string.IsNullOrWhiteSpace(Faculty.Text) ? "" : Faculty.Text,
                Department = string.IsNullOrWhiteSpace(Department.Text) ? "" : Department.Text,
                Specialty = string.IsNullOrWhiteSpace(Specialty.Text) ? "" : Specialty.Text,
                Timing = string.IsNullOrWhiteSpace(Timing.Text) ? "" : Timing.Text,
            });
        }

        if (Dom.IsChecked)
        {
            dataHandlers = new XMLDataHandlers();
            MyCollectionView.ItemsSource = dataHandlers.DOM_Search(new MainPageViewModel.StudentParliament
            {
                Name = string.IsNullOrWhiteSpace(Name.Text) ? "" : Name.Text,
                Faculty = string.IsNullOrWhiteSpace(Faculty.Text) ? "" : Faculty.Text,
                Department = string.IsNullOrWhiteSpace(Department.Text) ? "" : Department.Text,
                Specialty = string.IsNullOrWhiteSpace(Specialty.Text) ? "" : Specialty.Text,
                Timing = string.IsNullOrWhiteSpace(Timing.Text) ? "" : Timing.Text,

            });
        }
    }
    void Clear_Fields(Object sender, EventArgs e)
    {
        Name.Text = "";
        Faculty.Text = "";
        Department.Text = "";
        Specialty.Text = "";
        Timing.Text = "";
    }
    void Export_HTML(Object sender, EventArgs e)
    {
        XslCompiledTransform xct = new XslCompiledTransform();
        xct.Load(@"/Users/yanayalovska/Projects/laab2/laab2/assets/styles.xsl");
        XmlDocument newDoc = dataHandlers.CreateXmlDocument();
        newDoc.Save(@"/Users/yanayalovska/Projects/laab2/laab2/assets/curent.xml");
        xct.Transform(@"/Users/yanayalovska/Projects/laab2/laab2/assets/curent.xml", @"/Users/yanayalovska/Projects/laab2/laab2/assets/export.html");

        UpdateXmlContent();
    }

    void UpdateXmlContent()
    {
        XmlDocument xmlDoc = dataHandlers.CreateXmlDocument();
        mainPageViewModel.XmlContent = xmlDoc.OuterXml;
    }

    async void OnExitButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmation", "Do you really want to close the program?", "Yes", "No");

        if (answer)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}