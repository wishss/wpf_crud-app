﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input; // RelayCommand 사용을 위한 네임스페이스 추가
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using WPF_CRUD.Models;

namespace WPF_CRUD.ViewModels
{
    class ProductViewModel : ObservableObject
    {
        //private string searchProductName;
        private string productSearch;
        private string productName;
        private string quantity;
        private string unitPrice;
        private string selectedProduct;
        private string productSearchError;
        private string productNameError;
        private string quantityError;
        private string unitPriceError;

        // 데이터 바인딩을 위한 속성들
        //public string SearchProductName { get => searchProductName; set => SetProperty(ref searchProductName, value); }
        public string ProductSearch { get => productSearch; set => SetProperty(ref productSearch, value); }
        public string ProductName { get => productName; set => SetProperty(ref productName, value); }
        public string Quantity { get => quantity; set => SetProperty(ref quantity, value); }
        public string UnitPrice { get => unitPrice; set => SetProperty(ref unitPrice, value); }
        public string SelectedProduct { get => selectedProduct; set => SetProperty(ref selectedProduct, value); }
        public string ProductSearchError { get => productSearchError; set => SetProperty(ref productSearchError, value); }
        public string ProductNameError { get => productNameError; set => SetProperty(ref productNameError, value); }
        public string QuantityError { get => quantityError; set => SetProperty(ref quantityError, value); }
        public string UnitPriceError { get => unitPriceError; set => SetProperty(ref unitPriceError, value); }

        // ObservableCollection을 통한 제품 리스트 바인딩
        private ObservableCollection<ProductModel> originalProducts;  // 원본 데이터 저장
        public ObservableCollection<ProductModel> Products { get; set; }

        // ICommand 인터페이스를 통한 명령 (Command) 구현
        public ICommand AddProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand GetProductCommand { get; }

        public ProductViewModel()
        {
            originalProducts = new ObservableCollection<ProductModel>();
            Products = new ObservableCollection<ProductModel>();
            AddProductCommand = new RelayCommand(AddProduct);
            UpdateProductCommand = new RelayCommand(UpdateProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            GetProductCommand = new RelayCommand(GetProduct);
        }

        private void AddProduct()
        {
            ClearErrorMessages();

            if (!IsInputFieldsNull() || !IsInputFieldsInt())
                return;

            if (IsProductExist(ProductName))
            {
                ProductNameError = "이미 존재하는 제품입니다.";
                return;
            }

            // 새로운 ProductModel 객체 생성
            Products.Clear();
            originalProducts.Add(new ProductModel { Name = ProductName, Quantity = int.Parse(Quantity), UnitPrice = int.Parse(UnitPrice) });
            foreach (var product in originalProducts)
                Products.Add(product);

            ClearInputFields();
        }

        private void UpdateProduct()
        {
            // Update product logic
        }

        private void DeleteProduct()
        {
            // Delete product logic
        }

        private void GetProduct()
        {
            ClearErrorMessages();

            if (!originalProducts.Any() || !IsProductExist(ProductSearch))
            {
                ProductSearchError = "재고내역이 없습니다.";
                ClearInputFields();
                return;
            }

            if (string.IsNullOrEmpty(ProductSearch))
            {
                // 전체 조회
                Products.Clear();
                foreach (var product in originalProducts)
                {
                    Products.Add(product);
                }
                ClearInputFields();
                return;
            }

            // 특정 데이터 조회
            Products.Clear();
            var filteredProducts = originalProducts.Where(p => p.Name == ProductSearch).ToList();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }

            ClearInputFields();
        }

        public bool IsProductExist(string searchName)
        {
            return originalProducts.Any(p => p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsInputFieldsInt(string input)
        {
            return int.TryParse(input, out _);
        }

        private void ClearErrorMessages()
        {
            ProductSearchError = "";
            ProductNameError = "";
            QuantityError = "";
            UnitPriceError = "";
        }

        private void ClearInputFields()
        {
            ProductSearch = "";
            ProductName = "";
            Quantity = "";
            UnitPrice = "";
        }

        private bool IsInputFieldsNull()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(ProductName))
            {
                ProductNameError = "제품명을 입력해 주세요.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(Quantity))
            {
                QuantityError = "수량을 입력해 주세요.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(UnitPrice))
            {
                UnitPriceError = "단가를 입력해 주세요.";
                isValid = false;
            }

            return isValid;
        }

        private bool IsInputFieldsInt()
        {
            bool isValid = true;

            if (!int.TryParse(Quantity, out _))
            {
                QuantityError = "수량은 숫자여야 합니다.";
                isValid = false;
            }
            if (!int.TryParse(UnitPrice, out _))
            {
                UnitPriceError = "단가는 숫자여야 합니다.";
                isValid = false;
            }

            return isValid;
        }
    }
}

