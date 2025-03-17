using Microsoft.EntityFrameworkCore;
using ProautoCadastro.Data;
using ProdutoCadastro.Data.Context;
using ProdutoCadastro.Data.Repositories;
using ProdutoCadastro.Services.Interface;
using ProdutoCadastro.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAssociadoService, AssociadoService>();
builder.Services.AddScoped<IAssociadoRepository, AssociadoRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Associado}/{action=Autenticar}/{id?}");

app.MapControllers(); 

app.Run();
