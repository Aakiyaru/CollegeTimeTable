using System;

namespace TimeTable.DataBase.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
        public int Cabinet { get; set; }
        public int Number { get; set; }
    }
}
