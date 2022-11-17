using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Data.Components
{
    public class CategoryViewComponent: ViewComponent
    {
        private readonly ShopContext _context;
        public CategoryViewComponent(ShopContext context)
        {
            _context = context; 
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());
    }
}
