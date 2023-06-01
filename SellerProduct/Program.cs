using SellerProduct.IServices;
using SellerProduct.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddTransient<IUserServices, UserServices>();


/*
 * AddSignleton : Tạo ra 1 đối tượng services tồn tại cho đến khi vòng đời
 * của ứng dụng kết thúc. Services này sẽ được dùng chung cho các request
 * Loại đăng kí này phù hợp với các services mang tính toàn cục và không thay đổi
 * AddScoped: Mỗi loại request cụ thể sẽ tạo ra 1 đối tượng services, đối tượng này 
 * được dữ nguyên trong quá trình xử lý request. Phù hợp cho các services mà phục vụ cho một 
 * loại request cụ thể.
 * AddTransient: Mỗi request sẽ nhận một services cụ thể khi có yêu cầu. Mỗi services sẽ được tạo 
 * mới tại thời điểm có yêu cầu. Phù hợp cho các services
 * có nhiều trạng thái, nhiều nhiều cầu http và mang tính linh động hơn.
 * 
 */

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
});// thêm cái này để sử dụng được session

// Tất cả các dịch vụ đăng kí phải trước dòng này
var app = builder.Build(); //thực hiện tất cả services được cài đặt

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
//app.UseStatusCodePagesWithReExecute("/Home/Index"); // Midddleware tự động redirect người dùng đến trang /Home/index khi gặp bất kì nỗi nào HTTP status code
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
