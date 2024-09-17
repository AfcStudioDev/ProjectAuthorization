﻿namespace Authorization.Application.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public List<License> Licenses { get; set; } = new();
    }
}
