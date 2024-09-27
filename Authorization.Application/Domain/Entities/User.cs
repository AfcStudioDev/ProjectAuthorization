using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace Authorization.Application.Domain.Entities
{
    [Index( nameof( Id ), IsUnique = true)]
    public class User
    {
        [Key]
        [ForeignKey("Device")]
        public uint Id { get; set; }
        [DisallowNull]
        [MaxLength(30)]
        [Required]
        public string Email { get; set; } = null!;
        [DisallowNull]
        [Required]
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// Поле для EF. Все устройства пользователя.
        /// </summary>
        public List<Device> Devices { get; set; } = new();
    }
}
