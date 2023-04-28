﻿using BuberDinner.Application.Authentication;
using BuberDinner.Application.Authentication.Commands;
using BuberDinner.Application.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
