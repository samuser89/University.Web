using System;
using System.Linq;
using University.BL.Models;
using University.BL.Repositories;
using University.BL.Repositories.Implements;

namespace University.Test
{
    internal class Program
    {
        private static readonly UniversityModel university = new UniversityModel();
        private static readonly ICourseRepository courseRepository
            = new CourseRepository(new UniversityModel());
        static void Main(string[] args)
        {
            //var courses = university.Course.ToList();
            //var courses2 = courseRepository.GetAll().Result;
            //foreach (var item in courses2)
            //{
            //    Console.WriteLine($"{item.Title} {item.Credits}");
            //}

            var books = Book.Books();
            var authors = Author.Authors();
            var datepublic = Book.Books();

            //Linq
            //*****Mostrar en consola los 3 libros con más ventas.
            var ex1 = books.OrderByDescending(x => x.Sales).Take(3).ToList();

            //*****Mostrar en consola los 3 libros con menos ventas.
            Console.WriteLine("---------------");
            var ex2 = books.OrderBy(x => x.Sales).Take(3).ToList();
            foreach (var item in ex2)
            {
                Console.WriteLine($"{item.Title} - {item.Sales}");
            }

            //*****Mostrar en consola el autor con más libros publicados.
            //*****Mostrar en consola el autor y la cantidad de libros publicados.
            Console.WriteLine("---------------");
            var ex3 = from b in books
                           join a in authors on b.AuthorId equals a.AuthorId
                           group a by (a.AuthorId, a.Name) into query
                           orderby query.Count() descending
                           select query;

            var resultEx3 = ex3.FirstOrDefault();
            Console.WriteLine($"{resultEx3.Key.AuthorId} - {resultEx3.Key.Name} - {resultEx3.Count()}");

            //*****Mostrar en consola los libros publicados hace menos de 50 años.
            Console.WriteLine("---------------");
            var ex4 = books.Where(x => x.PublicationDate > DateTime.Now.AddYears(-50).Year).ToList();

            foreach (var item in ex4)
            {
                Console.WriteLine($"{item.Title} - {item.PublicationDate}");
            }

            //*****Mostrar en consola el libro más viejo.
            Console.WriteLine("---------------");
            var ex5 = books.OrderBy(x => x.PublicationDate).FirstOrDefault();


            //*****Mostrar en consola los autores que tengan un libro que comience con 'El'.
            Console.WriteLine("---------------");
            var ex6 = from b in books
                      join a in authors on b.AuthorId equals a.AuthorId
                      where b.Title.StartsWith("El")
                      select a;

            Console.ReadKey();
        }
    }
}
