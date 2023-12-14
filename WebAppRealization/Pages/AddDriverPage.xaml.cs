using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class AddCarBrandPage : Page
    {
        private ObservableCollection<CarModel> CarModels = new ();
        private DriverModel? Driver = null;
        
        public AddCarBrandPage()
        {
            InitializeComponent();
        }
        
        public AddCarBrandPage(DriverModel driverModel)
        {
            InitializeComponent();
            Driver = driverModel;

            NameTextBox.Text = Driver.Name;
            CountryManifactureTextBox.Text = Driver.Address;

            ButtonAction.Content = "Обновить";
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CarModels.Add(new CarModel
            {
                Brend = VinNumberTextBox.Text,
                Model = NameCarModelTextBox.Text
            });

            if (Driver == null)
            {
                var item = new DriverCreationModel()
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Cars = CarModels
                };
                if (await new ApiClient().PostCreateDriverAsync(item) == null)
                {
                    MessageBox.Show("Ошибка: Водитель не добавлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Водитель добавлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                var item = new DriverCreationModel
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Cars = CarModels
                };
                if (await new ApiClient().PutUpdateDriverdAsync(item, Driver.Id) != null)
                {
                    MessageBox.Show("Ошибка: Водитель не обновлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Водитель Обновлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void CreateCarModelButton_Click(object sender, RoutedEventArgs e)
        {
            if (VinNumberTextBox.Text == "" || NameCarModelTextBox.Text == "") return;
            
            var item = new CarCreationModel
            {
                Brend = VinNumberTextBox.Text,
                Model = NameCarModelTextBox.Text,
            };

            if(await new ApiClient().PostCarForDriverAsync(item, Driver.Id) != null)
            {
                MessageBox.Show("Авто добавлено.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Авто не добавлено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            VinNumberTextBox.Text = "";
            NameCarModelTextBox.Text = "";
        }
    }
}