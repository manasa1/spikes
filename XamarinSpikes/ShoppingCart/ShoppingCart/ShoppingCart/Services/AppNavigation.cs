﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingCart.Services
{
    public class AppNavigation : IAppNavigation
    {
        private readonly INavigationService _navi;
        private readonly IPageFactory _pages;

        public AppNavigation(INavigationService navi, IPageFactory pages)
        {
            _navi = navi;
            _pages = pages;
        }

        public async Task LoggedIn(bool result)
        {
            if (result)
            {
                await _navi.PushAsync(_pages.GetPage(Pages.Categories));
            }
            else
            {
                await _navi.DisplayAlert("Error", "Invalid username or password", "ok");
            }
        }

        public async Task ShowLogin()
        {
            await _navi.PushAsync(_pages.GetPage(Pages.Login));
        }

        public async Task ShowProduct(Models.Product product)
        {
            var page = App.GetProductPage(product);
            await _navi.PushAsync(page);
        }

        public async Task ShowProductList(List<Models.Product> items, string title, bool canShowSinglePage = false)
        {
            if (items != null && items.Any())
            {
                Page page;
                if (canShowSinglePage && items.Count == 1)
                {
                    page = App.GetProductPage(items.First());
                }
                else
                {
                    page = App.GetProductsListPage(items, title);
                }
                await _navi.PushAsync(page);
            }
            else
            {
                await _navi.DisplayAlert("Error", "There are no items to show for " + title);
            }
        }
    }
}