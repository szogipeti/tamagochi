﻿using System;
using System.Collections.Generic;

namespace Tamagochi.Model
{
    public partial class PasswordReset
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}