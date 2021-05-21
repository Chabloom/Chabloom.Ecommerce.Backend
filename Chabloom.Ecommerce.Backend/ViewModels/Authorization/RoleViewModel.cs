// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Authorization
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}