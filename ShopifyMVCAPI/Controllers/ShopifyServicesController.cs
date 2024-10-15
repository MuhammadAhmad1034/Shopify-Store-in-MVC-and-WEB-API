using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopifyMVCAPI.Models;
using ShopifyMVCAPI.Models.Authentication;
using System.Text;


namespace ShopifyMVCAPI.Controllers
{
    public class ShopifyServicesController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addNewUser()
        {
            ModelState.Clear();
            return View();
        }
        public IActionResult addNewAdmin()
        {
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public async Task<bool> isUserAlreadyRegistered(string email)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/isUserAlreadyRegistered?email=" + email))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //bool isRegister=false;
                    if (apiResponse == "true")
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        [HttpGet]
        public async Task<bool> isAdminAlreadyRegistered(string email)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/isAdminAlreadyRegistered?email=" + email))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //bool isRegister=false;
                    if (apiResponse == "true")
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        
        [HttpPost]
        public async Task<IActionResult> addNewUser(User user)
        {
            if (ModelState.IsValid) {
                string flag = isUserAlreadyRegistered(user.userEmail).Result.ToString();

                if (flag == "False" || flag == "false")
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/addNewUser", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //bool isRegister=false;
                            if (apiResponse == "true")
                            {
                                return RedirectToAction("getCategories");
                            }
                        }
                    }
                }
                return RedirectToAction("loginUser");
            }
            return BadRequest("Error in Creating User Account");
        }

        [HttpPost]
        public async Task<IActionResult> addNewAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                string flag = isAdminAlreadyRegistered(admin.adminEmail).Result.ToString();

                if (flag == "False" || flag == "false")
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/addNewAdmin", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //bool isRegister=false;
                            if (apiResponse == "true")
                            {
                                TempData["Admin"] = true;
                                return RedirectToAction("getCategories");
                            }
                        }
                    }
                }
                return RedirectToAction("loginAdmin");
            }
            return BadRequest("Error in Creating Admin Account");
        }

        public IActionResult loginUser()
        {
            ModelState.Clear();
            return View();
        }

        public IActionResult loginAdmin()
        {
            ModelState.Clear();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> loginAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/loginAdmin", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //bool isRegister=false;
                        if (apiResponse == "true")
                        {
                           TempData["Admin"] = true;
                            return RedirectToAction("getCategories");
                        }
                    }
                }
            }

            
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> loginUser(User user)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/loginUser", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //bool isRegister=false;
                        if (apiResponse == "true")
                        {
                            return View("getCategories");
                        }
                    }
                }
            }
            return BadRequest("Error in Logging User Account");
        }
        public async Task<IActionResult> getCategories()
        {   
            List<Category> categories = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/GetCategories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //JObject json = JObject.Parse(apiResponse);
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }
            if(TempData.ContainsKey("Admin"))
            {
                string isAdmin = TempData["Admin"].ToString();
                if(isAdmin == "True" || isAdmin == "true")
                {
                    ViewBag.IsAdmin = "True";
                }
                TempData.Keep();
            }
            else
            {
                ViewBag.IsAdmin = "User Account created Successfully";
            }
            return View(categories);
        }

        public async Task<IActionResult> getSubCategories(int id)
        {
            
            List<SubCategory>? subCategories = new List<SubCategory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/GetSubCategories?id="+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //JObject json = JObject.Parse(apiResponse);
                    subCategories = JsonConvert.DeserializeObject<List<SubCategory>>(apiResponse);
                }
                if (TempData.ContainsKey("Admin"))
                {
                    string isAdmin = TempData["Admin"].ToString();
                    if (isAdmin == "True" || isAdmin == "true")
                    {
                        ViewBag.IsAdmin = "True";
                    }
                    TempData.Keep();
                }
            }
            return View(subCategories);
        }

        public async Task<IActionResult> getItems(int id,int id2)
        {
            List<Item> items = new List<Item>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:44338/api/Category/GetItems?catId={id}&subCatId={id2}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //JObject json = JObject.Parse(apiResponse);
                    items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                }
            }
            if (TempData.ContainsKey("Admin"))
            {
                string isAdmin = TempData["Admin"].ToString();
                if (isAdmin == "True" || isAdmin == "true")
                {
                    ViewBag.IsAdmin = "True";
                }
                TempData.Keep();
            }
            return View(items);
        }
        //Admin 
        public IActionResult AddCategory()
        {
            //ModelState.Clear();
            return View();
        }

       

        [HttpPost]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/AddCategory?categoryName="+categoryName))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if(apiResponse == "false")
                        {
                            ViewBag.Error = "Something went wrong!";
                            
                        }
                        ViewBag.Error = "Added Successfully";
                        }
                    }
                
                return View();
            }
        public IActionResult AddSubCategory()
        {
            //ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(string subCategoryName,int categoryId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/AddSubCategory?subCategoryName="+subCategoryName+"&categoryId="+ categoryId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";
                    }
                    ViewBag.Error = "Added Successfully";
                }
            }

            return View();
        }

        public IActionResult AddItem()
        {
            //ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(string itemName, int quantity,float price,int subCategoryId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/AddItem?itemName="+itemName+"&quantity="+quantity+"&price="+price+"&subCategoryId="+subCategoryId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";
                    }
                    ViewBag.Error = "Added Successfully";
                    //var routeValues = new RouteValueDictionary
                    //{
                    //    {"id",subCategoryId }
                    //};
                    var routeValues = new RouteValueDictionary {
                    { "id", 0 },
                    {"id2" ,subCategoryId }
                    };
                    return RedirectToAction("getItems", routeValues);
                    
                }
            }

            
        }
        public async Task<IActionResult> UpdateCategory(int id)
        {

            List<Category>? categories = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/GetCategories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //JObject json = JObject.Parse(apiResponse);
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }


            return View(categories.Find(category => category.categoryId == id));
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id,Category category)
        {
            using (var httpClient = new HttpClient())
            {
                category.categoryId = id;
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/UpdateCategory", content)) 
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";
                    }
                    ViewBag.Message= "Updated Successfully";
                    return RedirectToAction("getCategories");
                }
            }

            
        }

        public async Task<IActionResult> UpdateSubCategory(int id)
        {
            SubCategory? subCat = new SubCategory();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/GetSubCategorybyID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    //JObject json = JObject.Parse(apiResponse);
                    subCat = JsonConvert.DeserializeObject<SubCategory>(apiResponse);
                }
            }
            return View(subCat);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSubCategory(int id, SubCategory category)
        {
            using (var httpClient = new HttpClient())
            {
                category.subCategoryId = id;
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/UpdateSubCategory", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";
                    }
                    ViewBag.Message = "Updated Successfully";
                    var routeValues = new RouteValueDictionary {
                    { "id", category.categoryId }
                    };
                    return RedirectToAction("getSubCategories",routeValues);
                }
            }


        }

        public async Task<IActionResult> UpdateItem(int id)
        {
            Item? item = new Item();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/GetItemById?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    //JObject json = JObject.Parse(apiResponse);
                    item = JsonConvert.DeserializeObject<Item>(apiResponse);
                }
            }
            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            using (var httpClient = new HttpClient())
            {
                item.itemId = id;
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44338/api/Category/UpdateItem", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";
                    }
                    ViewBag.Message = "Updated Successfully";
                    var routeValues = new RouteValueDictionary {
                    { "id", item.categoryId },
                    {"id2" ,item.subCategoryId }
                    };
                    return RedirectToAction("getItems", routeValues);
                }
            }


        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            using (var httpClient = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/DeleteCategory?id="+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";

                    }
                    ViewBag.Message = "Deleted Successfully";
                    return RedirectToAction("getCategories");

                }
            }
            //return View();
        }

        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            using (var httpClient = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/DeleteSubCategory?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";

                    }
                    ViewBag.Message = "Deleted Successfully";
                    return RedirectToAction("getCategories");

                }
            }
            //return View();
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            using (var httpClient = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/Category/DeleteItem?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "false")
                    {
                        ViewBag.Message = "Something went wrong!";

                    }
                    ViewBag.Message = "Deleted Successfully";
                    return RedirectToAction("getCategories");

                }
            }
            //return View();
        }

    }
}

