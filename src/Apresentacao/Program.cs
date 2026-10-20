using eAgenda.Aplicacao.Compartilhado;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdicionarCamadaInfraestrutura(builder.Configuration);

builder.Services.AdicionarCamadaAplicacao();

builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{

    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Modulos/{1}s/Views/{0}.cshtml");
    options.ViewLocationFormats.Add("/Compartilhado/Views/{0}.cshtml");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EAgendaDbContext>();
    if (dbContext.Database.IsSqlServer())
        dbContext.Database.Migrate();
}

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
