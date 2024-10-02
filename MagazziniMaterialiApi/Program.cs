using Intersoft.Crosslight;
using MagazziniMaterialiAPI;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Mapper;
using MagazziniMaterialiAPI.Repositories;
using MagazziniMaterialiAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi logging
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Aggiungi servizi al container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra i servizi e i repository
builder.Services.AddScoped<EtichettaService>();
builder.Services.AddScoped<MovimentazioneService>();
builder.Services.AddScoped<MissionePrelievoService>();
builder.Services.AddScoped<IMagazziniService, MagazziniService>();
builder.Services.AddScoped<IMaterialiService, MaterialiService>();
builder.Services.AddScoped<IMaterialeMagazziniService, MaterialeMagazziniService>();

builder.Services.AddScoped<IMaterialeRepository, MaterialeRepository>();
builder.Services.AddScoped<IGiacenzaRepository, GiacenzaRepository>();
builder.Services.AddScoped<IGiacenzaService, GiacenzaService>();
builder.Services.AddScoped<IMovimentazioneRepository, MovimentazioneRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMagazzinoRepository, MagazzinoRepository>();
builder.Services.AddScoped<IMaterialeMagazzinoRepository, MaterialeMagazzinoRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMagazzinoMapper, MagazzinoMapper>();
builder.Services.AddScoped<IMaterialeMapper, MaterialeMapper>();
builder.Services.AddScoped<IGiacenzaMapper, GiacenzaMapper>();

// Configura il DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura Identity
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

// Configura l'autenticazione JWT
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

// Configura l'autorizzazione
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Configura CORS
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
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Crea il ruolo Admin e l'utente Admin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    await CreateAdminRoleAndUser(roleManager, userManager);
}

app.Run();

async Task CreateAdminRoleAndUser(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
    string[] roleNames = { "Admin", "Operatore" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var admin = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}