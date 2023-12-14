using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class SelectedItemListBox : Page
    {
        private DriverModel? driverModel;
        private CompanyModel? companyModel;
        public SelectedItemListBox(DriverModel? _driverModel)
        {
            InitializeComponent();
            driverModel = _driverModel;
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Brend"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Model"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            //FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            //vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Model"));
            //stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить водителя и добавить авто";
            DeleteButton.Content = "Удалить водителя";
            DeleteCarModelOrEmployeeButton.Content = "Удалить выбранное авто";
        }
        public SelectedItemListBox(CompanyModel? _companyModel)
        {
            InitializeComponent();
            companyModel = _companyModel;
            
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Age"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Position"));
            stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить информацию о компании";
            DeleteButton.Content = "Удалить Компанию";
            DeleteCarModelOrEmployeeButton.Content = "Удалить выбранного сотрудника";
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                listBox.ItemsSource = await new ApiClient().GetEmployeesByCompanyAsync(companyModel.Id);
            }
            else
            {
                listBox.ItemsSource = await new ApiClient().GetCarsByDriverAsync(driverModel.Id);
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                await new ApiClient().DeleteDriverdAsync(companyModel.Id);
                
                NavigationService?.GoBack();
            }
            else
            {
                await new ApiClient().DeleteDriverdAsync(driverModel.Id);
                
                NavigationService?.GoBack();
            }
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                NavigationService.Navigate(new AddCompanyPage());
            }
            else
            {
                NavigationService.Navigate(new AddCarBrandPage(driverModel));
            }
        }

        private async void DeleteCarModelOrEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                var selectedItem = listBox.SelectedItem as EmployeeModel;
            }
            else
            {
                var selectedItem = listBox.SelectedItem as CarModel;
                
                await new ApiClient().DeleteCarAsync(selectedItem.Id, driverModel.Id);
            }
            
            
        }
    }
}