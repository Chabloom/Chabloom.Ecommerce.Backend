// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Threading.Tasks;

namespace Chabloom.Ecommerce.Backend.Services
{
    public class EmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}