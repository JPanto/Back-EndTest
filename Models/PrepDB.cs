using Back_EndTest.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Back_EndTest.Models{
    public static class PrepDB{
        public static void PrepPopulation(IApplicationBuilder app){
            using (var serviceScope = app.ApplicationServices.CreateScope()){
                SeedData(serviceScope.ServiceProvider.GetService<TestDbContext>());
            }
        }

        public static void SeedData(TestDbContext _context){
            System.Console.WriteLine("Appling Migrations...");
            
            _context.Database.Migrate();

            if(!_context.Tickets.Any()){
                System.Console.WriteLine("Adding data - seeding...");
                _context.Tickets.AddRange(
                    new Ticket(){user="JPanto7", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Ranges", creation_date=System.DateTime.Now,id_status_fk=0},
                    new Ticket(){user="Orion", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Milhouse", creation_date=System.DateTime.Now,id_status_fk=0},
                    new Ticket(){user="Roswell", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Tgrender", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Grossie", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Rost", creation_date=System.DateTime.Now,id_status_fk=0},
                    new Ticket(){user="Cash", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Dubell", creation_date=System.DateTime.Now,id_status_fk=1},
                    new Ticket(){user="Pan2023", creation_date=System.DateTime.Now,id_status_fk=1}
                );
                _context.SaveChanges();
            }else{
                System.Console.WriteLine("Already have data - now seeding");
            }
        }
    }
}