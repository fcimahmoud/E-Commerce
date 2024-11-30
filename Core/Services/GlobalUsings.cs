﻿global using UserAddress = Domain.Entities.Identity.Address;
global using Address = Domain.Entities.OrderEntities.Address;
global using Product = Domain.Entities.Product;
global using Microsoft.Extensions.Configuration;
global using Domain.Entities;
global using Shared;
global using AutoMapper;
global using Domain.Entities.OrderEntities;
global using Shared.OrderModels;
global using Services.Specifications;
global using Domain.Exceptions;
global using Microsoft.AspNetCore.Identity;
global using Shared.ErrorModels;
global using Domain.Entities.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Domain.Contracts;
global using Stripe;
global using Services.Abstractions;
