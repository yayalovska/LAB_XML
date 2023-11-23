using System.Collections.ObjectModel;

namespace laab2;

public class MainPageViewModel : BindableObject
{
    public struct StudentParliament
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string Specialty { get; set; }
        public string Timing { get; set; }
    }

    private ObservableCollection<StudentParliament> studentItems;
    public ObservableCollection<StudentParliament> StudentItems
    {
        get => studentItems;
        set
        {
            if (studentItems != value)
            {
                studentItems = value;
                OnPropertyChanged("StudentItems");
            }
        }
    }

    public MainPageViewModel()
    {
        StudentItems = new ObservableCollection<StudentParliament> { };
        foreach (var student in new XMLDataHandlers().students)
        {
            StudentItems.Add(student);
        };
    }

    private string xmlContent;
    public string XmlContent
    {
        get => xmlContent;
        set
        {
            if (xmlContent != value)
            {
                xmlContent = value;
                OnPropertyChanged(nameof(XmlContent));
            }
        }
    }
}