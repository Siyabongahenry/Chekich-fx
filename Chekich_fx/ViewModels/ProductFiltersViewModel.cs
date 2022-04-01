using Chekich_fx.Enums;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewModels
{
    public class ProductFiltersViewModel<T>
    {
        public List<T> ProductList { get; set; }
        public Category Category{ get; set; }
        public string PriceFilter { get; set; }
        public List<SelectListItem> PriceFilters { get; } = new List<SelectListItem>
        {
            new SelectListItem{Value="0", Text="Normal"},
            new SelectListItem{Value="1", Text="Lowest to Highest"},
            new SelectListItem{Value="2", Text="Highest to Lowest"}
        };
        public PagingInfo PagingInfo { get; set; }
    }
}
