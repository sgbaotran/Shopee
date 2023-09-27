using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopee.Repository.Components
{
    //View Component here is very much similiar to a React Component
    public class BrandsViewComponent : ViewComponent
    {

        private readonly DataContext _dataContext;

        //This class will accept a data context object and acts like a middleman between the datbase
        public BrandsViewComponent(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        //Query all informaitons in the database and return as a list,
        //Whatever data is called will be passed into ~/Shared/Components/Brands/Default.cshtml as the Model
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _dataContext.Brands.ToListAsync());
        }
    }
}

