using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rewind.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rewind.Data
{
    
    public class RewindDB : IdentityDbContext
    {
        //construtoes da classe RewindDB
        //indica ao qual as tabelas estao associadas
        //ver o conteudo do ficheiro "startup.cs"
        public RewindDB(DbContextOptions<RewindDB> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            //importa todas as funcionalidades do método
            base.OnModelCreating(modelBuilder);
            //Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "g", Name = "Gestor", NormalizedName = "GESTOR" },
                new IdentityRole { Id = "u", Name = "utilizador", NormalizedName = "UTILIZADOR" }
                );
            //Seed
            modelBuilder.Entity<Estudios>().HasData(
                new Estudios { ID = 1, Estudio = "ABC", Pais = "Portugal" },
                new Estudios { ID = 2, Estudio = "ACR", Pais = "França" },
                new Estudios { ID = 3, Estudio = "TCB", Pais = "Espanha" }
                );
            modelBuilder.Entity<Utilizadores>().HasData(
                new Utilizadores { ID = 1, Utilizador = "admin", Email = "a@aa" },
                new Utilizadores { ID = 2, Utilizador = "antonio", Email = "b@bb" },
                new Utilizadores { ID = 3, Utilizador = "tomas", Email = "c@cc" }
                );
            modelBuilder.Entity<Series>().HasData(
                new Series { ID = 1, Titulo = "Lorem ipsum", Sinopse = "Morbi laoreet neque", Episodios = 1, Estado = "continuando", Ano = 2004, Imagem = "a.jpg", Data = new DateTime(2021, 6, 15), EstudioID = 1 },
                new Series { ID = 2, Titulo = "dolor sit amet", Sinopse = "ut erat gravida", Episodios = 4, Estado = "terminado", Ano = 2005, Imagem = "b.jpg", Data = new DateTime(2021, 5, 3), EstudioID = 2 },
                new Series { ID = 3, Titulo = "consectetur adipiscing elit", Sinopse = "Integer mattis lorem et lorem", Episodios = 2, Estado = "continuando", Ano = 2012, Imagem = "c.jpg", Data = new DateTime(2020, 5, 20), EstudioID = 3 }
                );
            modelBuilder.Entity<Comentarios>().HasData(
                new Comentarios { ID = 1, UtilizadoresID = 1, SeriesID = 1, Estado = "visivel", Data = new DateTime(2021, 6, 16), Comentario = "bom", Estrelas = 5 },
                new Comentarios { ID = 2, UtilizadoresID = 2, SeriesID = 2, Estado = "invisivel", Data = new DateTime(2021, 5, 4), Comentario = "mau", Estrelas = 4 },
                new Comentarios { ID = 3, UtilizadoresID = 3, SeriesID = 3, Estado = "visivel", Data = new DateTime(2020, 5, 21), Comentario = "mais ou menos", Estrelas = 3 }
                );
        }

        //Representa as tabelas da BD
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Estudios> Estudios { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Utilizadores> Utilizadores { get; set; }
    }
}
