using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer.Entities
{
    public class CalculationHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Expression { get; set; }  // Yeni eklenen özellik
    }
}
