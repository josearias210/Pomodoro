using System;
using System.ComponentModel.DataAnnotations;

namespace Pomodoro.Models
{
    public class History
    {
        [Key]
        public short Id { get; set; }
        public double Durations { get; set; }
        public bool IsWorking { get; set; }
        public DateTime End { get; set; }
    }
}
