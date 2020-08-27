using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDBContext pp = new MyDBContext();

            using (var db = new MyDBContext())
            {
                db.Notes.Add(new Note { Text = "Hello, world" });
                db.Notes.Add(new Note { Text = "A second note" });
                db.Notes.Add(new Note { Text = "F Sharp" });
                db.SaveChanges();
            }

            using (var db = new MyDBContext())
            {
                foreach (var note in db.Notes)
                {
                    Console.WriteLine("Note {0} = {1}", note.NoteId, note.Text);
                }
            }

            Console.Write("Press any key . . . ");
            Console.ReadKey();
        }

        public class Note
        {
            public long NoteId { get; set; }
            public string Text { get; set; }
        }

        public class MyDBContext : DbContext
        {
            // default constructor should do this automatically but fails in this case
            public MyDBContext()
                : base("MyDBContext")
            {

            }
            public DbSet<Note> Notes { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
    }
}
