using MagazziniMaterialiAPI;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Repositories;
using MagazziniMaterialiAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aggiungi servizi
builder.Services.AddScoped<EtichettaService>();
builder.Services.AddScoped<IMaterialeRepository, MaterialeRepository>();
builder.Services.AddScoped<MovimentazioneService>();
builder.Services.AddScoped<MissionePrelievoService>();
builder.Services.AddScoped<IGiacenzaRepository, GiacenzaRepository>();
builder.Services.AddScoped<IMovimentazioneRepository, MovimentazioneRepository>();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IMagazziniService, MagazziniService>();
builder.Services.AddScoped<IMagazzinoMapper, MagazzinoMapper>();
builder.Services.AddScoped<IMagazzinoRepository, MagazzinoRepository>();

builder.Services.AddScoped<IMaterialiService, MaterialiService>();
builder.Services.AddScoped<IMaterialeMapper, MaterialeMapper>();
builder.Services.AddScoped<IMaterialeRepository, MaterialeRepository>();

builder.Services.AddScoped<IMaterialeMagazziniService, MaterialeMagazziniService>();
builder.Services.AddScoped<IMaterialeMagazzinoRepository, MaterialeMagazzinoRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura Identity e JWT
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Autorizzazione
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Logging e CORS
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configura il middleware dell'app
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

// Chiama il metodo per creare il ruolo Admin e l'utente Admin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Crea ruolo Admin e utente Admin se non esistono
    await CreateAdminRoleAndUser(roleManager, userManager);
}

app.Run();

async Task CreateAdminRoleAndUser(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
    // Crea il ruolo Admin se non esiste
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    // Crea il ruolo Operatore se non esiste
    if (!await roleManager.RoleExistsAsync("Operatore"))
    {
        await roleManager.CreateAsync(new IdentityRole("Operatore"));
    }

    // Controlla se esiste già un utente Admin
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        var admin = new IdentityUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
