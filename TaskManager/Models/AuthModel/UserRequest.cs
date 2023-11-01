﻿using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models.AuthModel
{
    public class UserRequest
    {
        public string PasswordHash { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

    }
}