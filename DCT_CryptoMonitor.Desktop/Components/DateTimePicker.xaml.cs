using System.Windows.Controls;

namespace DCT_CryptoMonitor.Desktop.Components;
/// <summary>
/// Interaction logic for DateTimePicker.xaml
/// </summary>
public partial class DateTimePicker : UserControl
{
    public DateTime selectedDateTime = DateTime.Now;
    //public DateTime selectedDateTime
    //{
    //    get { return (DateTime)GetValue(selectedDateTimeProperty); }
    //    set { SetValue(selectedDateTimeProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for selectedDateTime.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty selectedDateTimeProperty =
    //    DependencyProperty.Register("selectedDateTime", typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(0));


    public DateTimePicker()
    {
        InitializeComponent();
        datePicker.SelectedDate = selectedDateTime;

        // Populate hours ComboBox
        for (int i = 0; i < 24; i++)
        {
            hoursComboBox.Items.Add(i.ToString("00"));
        }
        hoursComboBox.SelectedIndex = DateTime.Now.Hour;

        // Populate minutes ComboBox
        for (int i = 0; i < 60; i++)
        {
            minutesComboBox.Items.Add(i.ToString("00"));
        }
        minutesComboBox.SelectedIndex = DateTime.Now.Minute;
    }

    private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedDateTime = datePicker.SelectedDate ?? DateTime.MinValue;

        if (hoursComboBox.SelectedItem != null && minutesComboBox.SelectedItem != null)
        {
            int selectedHour = int.Parse(hoursComboBox.SelectedItem.ToString());
            int selectedMinute = int.Parse(minutesComboBox.SelectedItem.ToString());

            selectedDateTime = selectedDateTime.AddHours(selectedHour).AddMinutes(selectedMinute);
        }
    }
}
