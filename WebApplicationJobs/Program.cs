using WebApplicationJobs.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao Razor Pages
builder.Services.AddRazorPages();

// Obtém a `connectionString` uma vez para uso nos repositórios
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o UserRepository e WorkRepository com a mesma conexão de banco de dados
builder.Services.AddSingleton(new UserRepository(connectionString));
builder.Services.AddSingleton(new WorkRepository(connectionString));

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
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
