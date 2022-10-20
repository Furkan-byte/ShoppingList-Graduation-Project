using DataAccess.Repositories.InterfaceRepos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utility;

namespace ShoppingListMVC.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitofWork _context;
        public HomeController(IUnitofWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ShoppingList> AllShoppingLists = _context.ShoppingList.GetAll();
            return View(AllShoppingLists);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                _context.ShoppingList.Add(shoppingList);
                _context.Save();
                TempData["Success"] = "Shopping List created successufly!";
                return RedirectToAction("Index");
            }
            return View(shoppingList);
        }

        public IActionResult Edit(int id)
         {
            HttpContext.Session.SetInt32("ShopListId", id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var shoppingListFromDb = _context.ShoppingList.GetFirstOrDefault(a => a.Id == id);
            string state = shoppingListFromDb.State;
            if (state != null)
            {
                HttpContext.Session.SetString("State", state);
            }
           
            IEnumerable<ListnProducts> ShoppingListProducts = _context.ListnProducts.GetAll(u => u.ShoppingListId == id,includeProperties:"Product");
            return View(ShoppingListProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string GoingShoppingCheck,string ShoppingCompleted)
        {
            int shoppingListId =(int)HttpContext.Session.GetInt32("ShopListId");

            var shoppingListFromDb = _context.ShoppingList.GetFirstOrDefault(a => a.Id == shoppingListId);
            IEnumerable<ListnProducts> ShoppingListProducts = _context.ListnProducts.GetAll(u => u.ShoppingListId == shoppingListId, includeProperties: "Product");
            if (GoingShoppingCheck != null && ShoppingCompleted != null)
            {
                TempData["Error"] = "The List have only 1 state!";
                return View(ShoppingListProducts);
            }
            else if (GoingShoppingCheck!=null)
            {

                shoppingListFromDb.State = StaticDetails.StatusGoingShopping;

                _context.ShoppingList.Update(shoppingListFromDb);
                _context.Save();
                TempData["Success"] = "List state updated successufly!";
                return RedirectToAction(nameof(Index));
            }
            else if (ShoppingCompleted!=null)
            {
                shoppingListFromDb.State = StaticDetails.StatusShoppingFinished;

                _context.ShoppingList.Update(shoppingListFromDb);
                _context.Save();
                TempData["Success"] = "List state updated successufly!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "?";
            return View(ShoppingListProducts);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ShoppingListFromDb = _context.ShoppingList.GetFirstOrDefault(u => u.Id == id);
            return View(ShoppingListFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var ShoppingListItem = _context.ShoppingList.GetFirstOrDefault(u => u.Id == id);
            if (ShoppingListItem == null)
            {
                return NotFound();
            }
            _context.ShoppingList.Remove(ShoppingListItem);
            _context.Save();
            TempData["Success"] = "Shopping List deleted successufly!";
            return RedirectToAction("Index");

        }

        public IActionResult ViewProducts(int id)
        {
            IEnumerable<Product> productList = _context.Product.GetAll(includeProperties: "Category");
            ViewBag.shoppingListId = id;
            return View(productList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewProducts(string SearchKey)
        {
            if(SearchKey == null)
            {
                IEnumerable<Product> productList = _context.Product.GetAll(includeProperties: "Category");
                return View(productList);
            }
            IEnumerable<Product> productListByName = _context.Product.GetAll(a=>a.Name == SearchKey, includeProperties: "Category");
            return View(productListByName);
        }

        public IActionResult Details(int shoppingListId,int productId)
        {
            ListnProducts listnProducts = new()
            {
                Description = "",
                ProductId = productId,
                ShoppingListId=shoppingListId,
                Product = _context.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category"),
            };
            return View(listnProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ListnProducts listnProducts)
        {     
            _context.ListnProducts.Add(listnProducts);
             TempData["success"] = "Product added into list successfully.";
            _context.Save();
            return RedirectToAction(nameof(ViewProducts));
        }
        [HttpGet]
        public IActionResult CustomerDetails(int Id,int shoppingListId, int productId)
        {
            ListnProducts listnProductsDescription = _context.ListnProducts.GetFirstOrDefault(a => a.ID == Id);
            string? description = listnProductsDescription.Description; 
            ListnProducts listnProducts = new()
            {
                Description = description,
                ProductId = productId,
                ShoppingListId = shoppingListId,
                Product = _context.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category"),
                
            };
            return View(listnProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomerDetails(ListnProducts listnProducts,string? BoughtTheProduct)
        {
            if (ModelState.IsValid)
            {
                if (BoughtTheProduct!=null)
                {
                _context.ListnProducts.Remove(listnProducts);
                TempData["success"] = "Shopping List updated successfully.";
                _context.Save();
                return RedirectToAction(nameof(Edit), new { id = listnProducts.ShoppingListId });
                }
           
                _context.ListnProducts.Update(listnProducts);
                TempData["success"] = "Shopping List updated successfully.";
                _context.Save();
                return RedirectToAction(nameof(Edit) , new { id = listnProducts.ShoppingListId });
            }
            return View(listnProducts);
            
        }

    }
}
