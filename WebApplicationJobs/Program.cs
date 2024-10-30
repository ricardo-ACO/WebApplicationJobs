using WebApplicationJobs.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao Razor Pages
builder.Services.AddRazorPages();

// Obt�m a `connectionString` uma vez para uso nos reposit�rios
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o UserRepository e WorkRepository com a mesma conex�o de banco de dados
builder.Services.AddSingleton(new UserRepository(connectionString));
builder.Services.AddSingleton(new WorkRepository(connectionString));

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
