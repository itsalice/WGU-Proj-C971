using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("term_id")]
        public int TermId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("start")]
        public DateTime Start { get; set; }
        [Column("end")]
        public DateTime End { get; set; }
        [Column("start_notification")]
        public bool IsStart { get; set; }
        [Column("end_notification")]
        public bool IsEnd { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("note")]
        public string? Note { get; set; }

        [Indexed]
        [Column("instructor_id")]
        public int InstructorId { get; set; }

        public string CombinedDates => $"{Start:MM/dd/yyyy} - {End:MM/dd/yyyy}";

    }
}
