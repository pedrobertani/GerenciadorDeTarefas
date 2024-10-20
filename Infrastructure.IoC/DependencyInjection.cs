﻿using Application.Common;
using Application.InterfacesService;
using Application.Mappings;
using Application.Services;
using AutoMapper.Internal;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDataBasesContext(ref services, configuration);

        services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false,
            typeof(UserProfile),
            typeof(TaskProfile)
            );

        //repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        //service
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;

    }

    private static string GetConnectionString(IConfiguration configuration, string connectionKey)
    {
        var connString = configuration.GetConnectionString(connectionKey);
        var builder = new SqlConnectionStringBuilder(connString);
        var dbPwd = configuration.GetSection("AppSettings:DbPwd")?.Value;

        if (!string.IsNullOrWhiteSpace(dbPwd))
        {
            try
            {
                builder.Password = CipherService.Descriptografar(dbPwd);
            }
            catch (Exception)
            {
            }
        }

        return builder.ConnectionString;
    }

    public static void AddDataBasesContext(ref IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.ConfigureWarnings(w => w.Ignore(SqlServerEventId.DecimalTypeKeyWarning))
                   .UseSqlServer(GetConnectionString(configuration, "DefaultConnection"),
                                 builder =>
                                 {
                                     builder.UseRelationalNulls();
                                     builder.EnableRetryOnFailure(5);
                                     builder.CommandTimeout(600);
                                 }));
    }

}