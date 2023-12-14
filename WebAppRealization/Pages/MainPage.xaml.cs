using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void CarBrands_Click(object sender, RoutedEventArgs e)
        {
            CarBrandsListBox.ItemsSource = await new ApiClient().GetDriversAsync();
        }

        private async void Companies_Click(object sender, RoutedEventArgs e)
        {
            CompaniesListBox.ItemsSource = await new ApiClient().GetCompaniesAsync();
        }

        private void CarBrandsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(CarBrandsListBox.SelectedItem as DriverModel));
        }

        private void CompaniesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(CompaniesListBox.SelectedItem as CompanyModel));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            TokenManager.Token = null;
            NavigationService.GoBack();
        }

        private void AddCarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarBrandPage());
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarBrandPage());
        }
    }
}