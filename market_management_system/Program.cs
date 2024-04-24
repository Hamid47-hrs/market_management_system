using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();

// * Categories * //
builder.Services.AddTransient<ICreateCategoryUseCase, CreateCategoryUseCase>();
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();
builder.Services.AddTransient<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

// * Products * //
builder.Services.AddTransient<ICreateProductUseCase, CreateProductUseCase>();
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
builder.Services.AddTransient<IViewProductsByCategoryUseCase, ViewProductsByCategoryUseCase>();
builder.Services.AddTransient<IUpdateProductUseCase, UpdateProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();

// * Transactions * //
builder.Services.AddTransient<IAddTransactionUseCase, AddTransactionUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();
builder.Services.AddTransient<IViewCashierTransactionsUseCase, ViewCashierTransactionsUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
