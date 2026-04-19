using Avalonia.Controls;
using AvaloniaApplication8.Data;
using AvaloniaApplication8.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

namespace AvaloniaApplication8.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ShopContext shopContext = new();

        private List<Products> _allProducts = new();
        public ObservableCollection<Products> Products { get; set; } = new();
        public ObservableCollection<Category> Categories { get; set; } = new();

        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private Category _searchCategory;
        [ObservableProperty]
        private string _sortCategory;
        [ObservableProperty]
        private ViewModelBase _currentPage;

        public List<string> SortOptions { get; } = new()
        {
            "Reset",
            "Name ↑",
            "Name ↓",
            "Price ↑",
            "Price ↓"
        };

        public MainWindowViewModel() 
        {
            LoadData();
            SortCategory = "Reset";
            ApplyFilters();
        }

        [RelayCommand]
        private void GoPageZero()
        {
            CurrentPage = new PageZero();
        }

        private async void LoadData()
        {
            Categories.Clear();
            Products.Clear();

            Categories.Add(new Category() { Id = -1, Name = "Все", Products = _allProducts});

            foreach (var category in await shopContext.Categories.ToListAsync())
            {
                Categories.Add(category);
            }
            foreach(var product in await shopContext.Products.ToListAsync())
            {
                Products.Add(product);
                _allProducts.Add(product);
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilters();
        }
        partial void OnSearchCategoryChanged(Category value)
        {
            ApplyFilters();
        }
        partial void OnSortCategoryChanged(string value)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            var query = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(p => p.Name.ToLower().Contains(SearchText.ToLower()));
            }

            if (SearchCategory != null && SearchCategory.Id != -1)
            {
                query = query.Where(p => p.Category.Id == SearchCategory.Id);
            }

            query = SortCategory switch
            {
                "Name ↑"  => query.OrderBy(p => p.Name),
                "Name ↓" => query.OrderByDescending(p => p.Name),
                "Price ↑" => query.OrderBy(p => p.Price),
                "Price ↓" => query.OrderByDescending(p => p.Price),
                _ => query

            };

            Products.Clear();

            foreach (var item in query)
            {
                Products.Add(item);
            }
        }
    }
}
