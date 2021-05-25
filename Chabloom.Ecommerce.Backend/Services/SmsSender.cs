// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Threading.Tasks;

namespace Chabloom.Ecommerce.Backend.Services
{
    public class SmsSender
    {
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            return Task.CompletedTask;
        }
    }
}