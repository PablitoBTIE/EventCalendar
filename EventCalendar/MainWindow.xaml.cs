
using System.Windows;
using System.Windows.Controls;

namespace EventCalendar
{
    public partial class MainWindow
    {
        private List<Event> allEvents = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAddEventClick(object sender, RoutedEventArgs e)
        {
            var eventName = eventNameTextBox.Text;
            var dayOfWeek = (dayOfWeekComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var isImportant = importantCheckBox.IsChecked ?? false;

            var newEvent = new Event 
            {
                EventDetails = $"{eventName} - {dayOfWeek} - {(isImportant ? "Ważne" : "Nieważne")}",
                IsImportant = isImportant
            };
            
            allEvents.Add(newEvent);
            eventsListBox.Items.Add(newEvent);
        }

        private void OnViewDetailsClick(object sender, RoutedEventArgs e)
        {
            if (eventsListBox.SelectedItem is Event selectedEvent)
            {
                MessageBox.Show(selectedEvent.EventDetails, "Szczegóły Wydarzenia");
            }
        }

        private void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = (filterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var filteredEvents = filter switch
            {
                "Ważne" => allEvents.Where(ev => ev.IsImportant).ToList(),
                "Nieważne" => allEvents.Where(ev => !ev.IsImportant).ToList(),
                _ => allEvents
            };

            eventsListBox.Items.Clear();
            foreach (var ev in filteredEvents)
            {
                eventsListBox.Items.Add(ev);
            }
        }
    }

    public class Event
    {
        public string EventDetails { get; set; }
        public bool IsImportant { get; set; }
    }
}