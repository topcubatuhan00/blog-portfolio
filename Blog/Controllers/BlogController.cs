using Blog.Application.Abstract;
using Blog.Domain.Models.Blog;
using Blog.Domain.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;

    public BlogController(IBlogService blogService, ICategoryService categoryService)
    {
        _blogService = blogService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _blogService.GetBlogs(false);
        return View(blogs);
    }
    
    
    public async Task<IActionResult> Details(int id) 
    {
        var blog = await _blogService.GetBlog(id);
        return View(blog);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBlog(int? id)
    {
        var cats = await _categoryService.GetCategories();
        ViewBag.Categories = cats;  
        
        UpdateBlogModel model =  new UpdateBlogModel();
        if(id != null)
        {
            var res = await _blogService.GetBlog(id.Value);
            model.Title = res.Title;
            model.Content = res.Content;
            model.Author = res.Author;
            model.CatId = res.CatId;
            model.Id = res.Id;
            model.ImageUrl = res.ImageUrl;
            model.IsActive = res.IsActive;
            model.CreatedDate = res.Created;
        }
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBlog(CreateBlogModel model)
    {
        var chx = Request.Form["IsActive"];
        if (model.Id != null)
        {
            var res2 = await _blogService.UpdateBlog(new UpdateBlogModel
            {
                Title = model.Title,
                Content = model.Content,
                Author = model.Author,
                CatId = model.CatId,
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                UpdatedBy = "Batuhan Topcu",
                IsActive = chx == "on" ? true : false,
                CreatedBy = model.CreatedBy,
                CreatedDate =  DateTime.SpecifyKind(
                    model.CreatedDate, 
                    DateTimeKind.Utc
                )
            });
            if (!res2)
                ModelState.AddModelError("Blog","Yazı Güncellenirken Bir Sorun Oluştu");    
        }
        else
        {
            var res = await _blogService.AddBlog(model);

            if (!res)
                ModelState.AddModelError("Blog","Yazı Eklenirken Bir Sorun Oluştu");    
        }
        
        return RedirectToAction("Index","Blog");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Category()
    {
        var res = await _categoryService.GetCategories();
        return View(res);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory(int? id)
    {
        UpdateCategoryModel model =  new UpdateCategoryModel();
        if(id != null)
        {
            var res = await _categoryService.GetCategory(id.Value);
            model.Description = res.Description;
            model.Name = res.Name;
            model.Id = res.Id;
        }
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory(UpdateCategoryModel model)
    {

        if (model.Id != null && model.Id > 0)
        {
            var res2 = await _categoryService.UpdateCategory(model);

            if (!res2)
            {
                ModelState.AddModelError("Blog", "Kategori Güncellenirken Bir Sorun Oluştu");
            return View(model);
            }
            return RedirectToAction("Category", "Blog");
        }

        var res = await _categoryService.AddCategory(new CreateCategoryModel { Name = model.Name, Description = model.Description, Order = model.Order, CreatedBy ="API"});

        if (!res)
            ModelState.AddModelError("Blog", "Kategori Eklenirken Bir Sorun Oluştu");

        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BlogList()
    {
        var model = await _blogService.GetBlogs(true);
        return View(model);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile upload)
    {
        if (upload != null && upload.Length > 0)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads");

// Klasör yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            var imageUrl = Url.Content($"~/images/uploads/{fileName}");
            return Json(new { url = imageUrl });
        }

        return Json(new { uploaded = false, error = new { message = "Upload failed" } });
    }
}
