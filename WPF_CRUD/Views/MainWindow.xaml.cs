using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CRUD.ViewModels;
using WPF_CRUD.Models;

namespace WPF_CRUD.Views
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductViewModel();  // ViewModel을 DataContext로 설정
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = dgvProduct.SelectedItem as ProductModel;
            if (selectedProduct != null)
            {
                var viewModel = (ProductViewModel)DataContext;
                viewModel.ProductName = selectedProduct.Name;
                viewModel.Quantity = selectedProduct.Quantity.ToString();
                viewModel.UnitPrice = selectedProduct.UnitPrice.ToString();
            }
        }

        public void RefreshDataGrid()
        {
            dgvProduct.Items.Refresh(); // DataGrid 갱신
        }
    }
}
