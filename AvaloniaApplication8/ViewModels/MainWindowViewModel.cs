using AvaloniaApplication8.Data;
using AvaloniaApplication8.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace AvaloniaApplication8.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ShopContext shopContext = new();

        public List<Products> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        public MainWindowViewModel() 
        {
            LoadData();
        }

        private async void LoadData()
        {
            Categories.Clear();

            foreach (var category in await shopContext.Categories.ToListAsync())
            {
                Categories.Add(category);
                Debug.WriteLine(category.Name);
            }
        }
    }
}
