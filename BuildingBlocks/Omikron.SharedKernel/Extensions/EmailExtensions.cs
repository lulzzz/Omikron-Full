using FluentEmail.Core;
using FluentEmail.Core.Defaults;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omikron.SharedKernel.Infrastructure.Email;
using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Options;
using SendGrid;

namespace Omikron.SharedKernel.Extensions
{
	public static class EmailExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
            if (emailSettings == null)
            {
                throw new Exception("Email Service cannot be registered properly without valid configuration. Please take a look at EmailSettings.cs");
            }

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings")).AddSingleton(x => x.GetRequiredService<IOptions<EmailSettings>>().Value);

            services
                .AddFluentEmail(emailSettings.Sender);

            services.AddScoped<ISendGridClient, SendGridClient>(x => new (emailSettings.SendGridKey));

            Email.DefaultRenderer = new ReplaceRenderer();

#if DEBUG
            Email.DefaultSender = new SmtpSender(new SmtpClient()
            {
                Host = emailSettings.MailServer,
                Port = emailSettings.MailPort,
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password)
            });
#else 
            Email.DefaultSender = new SendGridSender(emailSettings.SendGridKey);
#endif
            services.AddScoped<IEmailService>(provider => new FluentEmailService(emailSettings));
            return services;
        }

        public static IServiceCollection AddEmailContentFactories(this IServiceCollection services, Assembly[] assemblies)
        {
            const string marker = "IEmailContentFactory";
            const string sendGridMarker = "ISendGridMessageFactory";

            return services.AddServicesAsImplementedInterface(marker, ServiceLifetime.Transient, assemblies)
                .AddServicesAsImplementedInterface(sendGridMarker, ServiceLifetime.Transient, assemblies);
        }
    }
}