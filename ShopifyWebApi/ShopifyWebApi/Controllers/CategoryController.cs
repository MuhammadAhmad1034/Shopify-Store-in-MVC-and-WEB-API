using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopifyWebApi.Repository;
using ShopifyWebApi.Models;

using System.Text.Json.Nodes;
using ShopifyWebApi.Models.Auth;

namespace ShopifyWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public bool AddCategory(string categoryName)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            return categoryRepo.AddCategory(categoryName);
        }

        [HttpPost]
        public bool UpdateCategory(Category category)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            return categoryRepo.UpdateCategory(category);
        }

        [HttpGet]
        public bool DeleteCategory(int id)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            return categoryRepo.DeleteCategory(id);
        }



        [HttpGet]
        public bool AddSubCategory(string subCategoryName,int categoryId)
        {
            SubCategoryRepo repo = new SubCategoryRepo();
            return repo.AddSubCategory(subCategoryName, categoryId);
        }

        [HttpPost]
        public bool UpdateSubCategory(SubCategory subCategory)
        {
            SubCategoryRepo repo = new SubCategoryRepo();
            return repo.UpdateSubCategory(subCategory);
        }

        [HttpGet]
        public bool DeleteSubCategory(int id)
        {
            SubCategoryRepo categoryRepo = new SubCategoryRepo();
            return categoryRepo.DeleteSubCategory(id);
        }


        [HttpGet]
        public List<Category> GetCategories()
        {
            CategoryRepo repo = new CategoryRepo();
            return repo.getCategories();
        }

        [HttpGet]
        public List<SubCategory> GetSubCategories(int id)
        {
            SubCategoryRepo repo = new SubCategoryRepo();
            return repo.getSubCategories(id);
        }

        [HttpGet]
        public List<Item> GetItems(int catId, int subCatId) // cat id for array selection and subCatId for item Selection
        {
            ItemRepo repo = new ItemRepo();
            return repo.getItems(catId, subCatId);
        }

        [HttpGet]
        public bool AddItem(string itemName, int quantity,float price,int subCategoryId)
        {
            ItemRepo repo = new ItemRepo();
            return repo.AddItem(itemName, quantity, price, subCategoryId);
        }

        [HttpPost]
        public bool UpdateItem(Item item)
        {
            ItemRepo repo = new ItemRepo();
            return repo.UpdateItem(item);
        }

        [HttpGet]
        public bool DeleteItem(int id)
        {
            ItemRepo itemRepo = new ItemRepo();
            return itemRepo.DeleteItem(id);
        }

        [HttpGet]
        public SubCategory GetSubCategorybyID(int id)
        {
            SubCategoryRepo repo = new SubCategoryRepo();
            return repo.GetSubCategorybyID(id);
        }

        [HttpGet]
        public Item GetItemById(int id)
        {
            ItemRepo repo = new ItemRepo();
            return repo.GetItemById(id);
        }
        [HttpPost]
        public void addToCart(List<object> item)
        {
            CartRepo.cart.Add(item);
        }

        [HttpPost]
        public void addOrder(List<object> item)
        {
            OrderRepo.orders.Add(item);
        }

        [HttpGet]
        public bool isUserAlreadyRegistered(string email)
        {
            AuthRepo repo = new AuthRepo();
            return repo.isUserAlredyRegistered(email);
        }

        [HttpGet]
        public bool isAdminAlreadyRegistered(string email)
        {
            AuthRepo repo = new AuthRepo();
            return repo.isAdminAlredyRegistered(email);
        }

        [HttpPost]
        public bool loginUser(User user)
        {
            AuthRepo repo = new AuthRepo();
            return repo.loginUser(user.userEmail,user.userPassword);
        }

        [HttpPost]
        public bool loginAdmin(Admin admin)
        {
            AuthRepo repo = new AuthRepo();
            return repo.loginAdmin(admin.adminEmail, admin.adminPassword);
        }


        [HttpPost]
        public bool addNewUser(User user)
        {
            AuthRepo repo = new AuthRepo();
            return repo.addNewuser(user);
        }

        [HttpPost]
        public bool addNewAdmin(Admin admin)
        {
            AuthRepo repo = new AuthRepo();
            return repo.addNewAdmin(admin);
        }




        //[HttpGet]
        //public List<List<object>> getCart()
        //{
        //    return CartRepo.cart;
        //}

    }
}
