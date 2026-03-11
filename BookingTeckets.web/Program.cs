
using BookingTeckets.web.CustomMidleWares;

using BookingTeckets.web.Extensions;
using Domain.Contracts;
using Domain.Modules.UserModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles.TripProfiles;
using ServicesAbstraction;
using StackExchange.Redis;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace BookingTickets.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Add services to the container
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BookingTicketsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityCore<User>(options => {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<BookingTicketsDbContext>()
                .AddDefaultTokenProviders().
                AddSignInManager<SignInManager<User>>(); 


            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnectionString"); // أو "localhost:6379"

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(redisConnectionString!)
            );

            builder.Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"])),
                };
                
            });
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("IdentityPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1); // المدة الزمنية
                    opt.PermitLimit = 3;                 // عدد المحاولات المسموحة
                    opt.QueueLimit = 0;                  // لو زادوا ميتوزعوش في طابور، يترفضوا فوراً
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                // اختياري: تخصيص الرد لما الـ Limit يخلص
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            #endregion
            var app = builder.Build();
            await app.SeedDataBaseAsync();


            #region Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseRateLimiter();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
