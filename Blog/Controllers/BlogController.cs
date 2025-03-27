using Blog.Application.Abstract;
using Blog.Domain.Models.Blog;
using Blog.Domain.Models.Category;
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
        var blogs = await _blogService.GetBlogs();
        return View(blogs);
    }

    public IActionResult AddBlog()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddBlog(CreateBlogModel model)
    {
        var res = await _blogService.AddBlog(model);

        if (!res)
            ModelState.AddModelError("Blog","Yazı Eklenirken Bir Sorun Oluştu");

        return View(model);
    }

    public async Task<IActionResult> Category()
    {
        var res = await _categoryService.GetCategories();
        return View(res);
    }

    public async Task<IActionResult> AddCategory(int? id)
    {
        if(id != null)
        {
            var res = await _categoryService.GetCategory(id.Value);
            return View(res);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(UpdateCategoryModel model)
    {

        if (model.Id != null && model.Id > 0)
        {
            var res2 = await _categoryService.UpdateCategory(model);

            if (!res2)
                ModelState.AddModelError("Blog", "Kategori Güncellenirken Bir Sorun Oluştu");

            return View(model);
        }

        var res = await _categoryService.AddCategory(new CreateCategoryModel { Name = model.Name, Description = model.Description, Order = model.Order, CreatedBy ="API"});

        if (!res)
            ModelState.AddModelError("Blog", "Kategori Eklenirken Bir Sorun Oluştu");

        return View(model);
    }
}
