using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopee.Repository.Components
{
    public class CategoriesViewComponent : ViewComponent
    {

        private readonly DataContext _dataContext;

        //This object is automatically craeted and accpet dataContext which acts like a query tool
        public CategoriesViewComponent(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Query and get all records from Categories table
            //Whatever data is returned will be passed into ~/Shared/Components/Categories/Default.cshtml as the Model
            return View(await _dataContext.Categories.ToListAsync());
        }
    }
}

