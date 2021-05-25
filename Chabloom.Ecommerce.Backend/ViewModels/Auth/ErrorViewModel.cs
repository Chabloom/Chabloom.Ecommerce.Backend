﻿// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Chabloom.Accounts.Backend.ViewModels
{
    public class ErrorViewModel
    {
        [Required]
        public string Id { get; set; }

        public string Error { get; set; }

        public string ErrorDescription { get; set; }

        public string RedirectUri { get; set; }
    }
}