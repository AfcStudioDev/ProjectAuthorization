using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Authorization.Application.Domain.Entities
{
    [Index( nameof( Id ), IsUnique = true )]
    public class Device
    {
        [Key]
        public uint Id { get; set; }
        [DisallowNull]
        [Required]
        public string DeviceNumber { get; set; } = null!;
        [Required]
        public uint UserId { get; set; }
        [AllowNull]
        public DateOnly ExpirationLicense { get; set; }
        
        /// <summary>
        /// Для EF. Чтобы получать из девайса юзера.
        /// </summary>
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
