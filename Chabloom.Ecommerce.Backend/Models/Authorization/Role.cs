// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models.Authorization
{
    public class Role
    {
        [Required]
        [Key]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}