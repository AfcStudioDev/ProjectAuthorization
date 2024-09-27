using System.ComponentModel.DataAnnotations;

namespace Authorization.Application.Domain.Entities
{
    /// <summary>
    /// Таблица для работы с лицензиями. Отображение на фронте, удобная смена цен и сроков админом.
    /// </summary>
    public class LicenseType
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public UInt16 Duration { get; set; }
    }
}