using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCase;
using market_management_system.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

if (builder.Environment.IsEnvironment("Quality_Assurance"))
{
    builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
    builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
}
else
{
    builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
    builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
    builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
