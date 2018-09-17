using System.ComponentModel.DataAnnotations;

namespace Pomodoro.Models
{
    public class Configuration
    {
        [Key]
        public short Id { get; set; }
        public short Working { get; set; }
        public short ShortBreak { get; set; }
        public short LongBreak { get; set; }
        public short Pomorodos { get; set; }
    }
}
