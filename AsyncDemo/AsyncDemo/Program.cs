using System;
using System.Linq;
using System.Data.Entity; 
using System.Threading.Tasks;
using System.Threading; 

namespace AsyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
           var task =  PerformDatabaseOperations();

            Console.WriteLine();
            Console.WriteLine("Quote of the day");
            Console.WriteLine(" Don't worry about the world coming to an end today... ");
            Console.WriteLine(" It's already tomorrow in Australia.");


           // task.Wait(); 

            for (int i = 0 ; i<= 50 ;i++)
            {
                Thread.Sleep(10);
                   Console.WriteLine("Quote" + i);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static async Task PerformDatabaseOperations()
        {
            using (var db = new BloggingContext())
            {
                // log SQL to the console
                db.Database.Log = Console.Write; 
                // Create a new blog and save it 
                db.Blogs.Add(new Blog
                {
                    Name = "Test Blog #" + (db.Blogs.Count() + 1)
                });
                await db.SaveChangesAsync();
                Console.WriteLine("Between  two awaits");

                // Query for all blogs ordered by name 
                var blogs = await (from b in db.Blogs
                             orderby b.Name
                             select b).ToListAsync();

                // Write all blogs out to Console 
                Console.WriteLine();
                Console.WriteLine("All blogs:");
                foreach (var blog in blogs)
                {
                    Thread.Sleep(10);
                    Console.WriteLine(" " + blog.Name);
                }
            }
        }
    }
}