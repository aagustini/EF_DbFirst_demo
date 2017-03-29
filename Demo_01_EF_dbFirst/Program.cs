using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_01_EF_dbFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            #region demo_db_first

            //// crud - adiciona na um novo filme à coleção
            ////        remove o primeiro filme do banco
            ////        atualiza os dados de um filme
            //using (var contexto = new MoviesContext())
            //{
            //    var listaFilmes = contexto.Movies.ToList();

            //    // insert
            //    contexto.Movies.Add(new Movy()
            //    {
            //        Title = "Logan",
            //        Director = "James Mangold",
            //        Rating = 8.5,
            //        ReleaseDate = new DateTime(2017, 03, 24),
            //        GenreID = 1
            //    });

            //    // edit
            //    Movy batman = listaFilmes.Where(f => f.Title == "The Dark Knight").FirstOrDefault<Movy>();
            //    if (batman != null)
            //        batman.Title = "Batman - " + batman.Title;

            //    // delete
            //    contexto.Movies.Remove(listaFilmes.ElementAt<Movy>(0));

            //    // persistir
            //    contexto.SaveChanges();
            //}


            //// lista todos os generos
            //using (var contexto = new MoviesContext())
            //{
            //    //contexto.Database.Log = Console.Write;

            //    Console.WriteLine("Todos os generos");
            //    foreach (Genre genero in contexto.Genres)
            //    {
            //        Console.WriteLine("{0} \t {1}", genero.GenreID, genero.Name);

            //    }
            //}

            //Console.WriteLine("\n");
            //// lista todos os filmes do genero "Action"
            //using (var contexto = new MoviesContext())
            //{
            //    //contexto.Database.Log = Console.Write;
            //    Genre genero = contexto.Genres.Find(1);
            //    if (genero != null)
            //    {
            //        Console.WriteLine("\nFilmes do genero: " + genero.Name);
            //        foreach (Movy filme in genero.Movies)
            //        {
            //            Console.WriteLine("\t{0}", filme.Title);

            //        }
            //    }
            //}

            //// gera uma exceção pois o contexto não está disponível
            //Console.WriteLine("\nDesconectado...\n");
            //MoviesContext cntx = new MoviesContext();
            //cntx.Database.Log = Console.Write;
            //Genre action = cntx.Genres.Find(1);
            //cntx.Dispose();
            //if (action != null)
            //{
            //    Console.WriteLine("\nFilmes do genero: " + action.Name);

            //    foreach (Movy filme in action.Movies)
            //    {
            //        Console.WriteLine("\t{0}", filme.Title);
            //    }
            //}


            //// Desconectato
            //MoviesContext cntx = new MoviesContext();
            //cntx.Database.Log = Console.Write;
            //List<Genre> generos = cntx.Genres.ToList<Genre>();
            //cntx.Dispose();
            //foreach (Genre genero in generos)
            //{
            //    Console.WriteLine("{0} \t {1}", genero.GenreID, genero.Name);
            //}

            //cntx = new MoviesContext();
            ////cntx.Database.Log = Console.Write;
            ////var action= cntx.Genres.Find(1);
            //var action2 = cntx.Genres.Include("Movies").Where(g => g.GenreID == 1).FirstOrDefault();
            //cntx.Dispose();
            //if (action2 != null)
            //{
            //    Console.WriteLine("{0} \t {1}", action2.Name, action2.Description);
            //    foreach (Movy filme in action2.Movies)
            //    {
            //        Console.WriteLine("\t{0}", filme.Title);
            //    }
            //}

            #endregion

            #region - consultas

            MoviesContext contexto = new MoviesContext();
            //// filmes do diretor “Quentin Tarantino”
            //var query1 = from f in contexto.Movies
            //             where f.Director == "Quentin Tarantino"
            //             select f;

            //var query2 = from f in contexto.Movies
            //              where f.Director == "Quentin Tarantino"
            //              select f.Title;

            //var query3 = contexto.Movies
            //                      .Where(f => f.Director == "Quentin Tarantino")
            //                      .Select(f => f.Title);

            //Console.WriteLine("Filmes do diretor Quentin Tarantino");
            //foreach (String titulo in query2)
            //{
            //    Console.WriteLine(titulo);
            //}


            // todos os filmes do genero "Action"
            Console.WriteLine("\nFilmes de ação");
            ////contexto.Database.Log = Console.Write;
            //var query4 = (from genero in contexto.Genres
            //                                   .Include("Movies")
            //            where genero.Name == "Action"
            //            select genero).First();

            //foreach (var filme in query4.Movies)
            //{
            //    Console.WriteLine("\t" + filme.Title);
            //}

            //// projeção sobre o título e dada de lançamento dos 
            //// filmes do diretor “Quentin Tarantino” 
            //var query5 = from f in contexto.Movies
            //           where f.Director == "Quentin Tarantino"
            //           select new { f.Title, f.ReleaseDate };

            //foreach (var filme in query5)
            //{
            //    Console.WriteLine("{0}\t {1}", filme.ReleaseDate.ToShortDateString(), filme.Title);
            //}

            //// Gêneros ordenados pelo nome
            //var query6 = from g in contexto.Genres
            //             orderby g.Name descending
            //             select g;

            //foreach (var genero in query6)
            //{
            //    Console.WriteLine("{0}\t {1}", genero.Name, genero.Description);
            //}

            ////Filmes agrupados pelo ano de lançamento
            //var query7 = from f in contexto.Movies
            //             group f by f.ReleaseDate.Year;

            //foreach (var ano in query7.OrderByDescending(g => g.Key))
            //{
            //    Console.WriteLine("Ano: {0}", ano.Key);
            //    foreach (var filme in ano)
            //    {
            //        Console.WriteLine("\t{0:dd/MM}\t {1}", filme.ReleaseDate, filme.Title);
            //    }
            //}

            // Projeção do faturamento total, quantidade de filmes 
            // e avaliação média agrupadas por gênero
            var query8 = from f in contexto.Movies
                         group f by f.Genre.Name into grpGen
                         select new
                         {
                             Categoria = grpGen.Key,
                             Filmes = grpGen,
                             Faturamento = grpGen.Sum(e => e.Gross),
                             Avaliacao = grpGen.Average(e => e.Rating),
                             Quantidade = grpGen.Count()
                         };

            foreach (var genero in query8)
            {
                Console.WriteLine("Genero: {0}", genero.Categoria);
                Console.WriteLine("\tFaturamento total: {0}\n\t Avaliação média: {1}\n\tNumero de filmes: {2}",
                                    genero.Faturamento, genero.Avaliacao, genero.Quantidade);
            }


            #endregion
            Console.ReadKey();
        }
    }
}
