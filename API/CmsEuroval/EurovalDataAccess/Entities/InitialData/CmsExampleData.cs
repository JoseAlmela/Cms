using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EurovalDataAccess.Entities.InitialData
{
    public static class CmsExampleData
    {
        private static int lastId = 1;
        private static Random rnd = new Random();
        public static Pista[] Pistas { get; } =
            {
                new Pista { Id=1, Nombre="Padding"},
                new Pista { Id=2, Nombre="Football"},
                new Pista { Id=3, Nombre="Soccer"},
            };

        public static Socio[] Socios { get; } =
            {
                        new Socio { Id=1, Nombre="Jose", Email="micorreo@euroval.com"},
                        new Socio { Id=2, Nombre="Juan", Email="micorre2o@euroval.com"},
                        new Socio { Id=3, Nombre="Miguel", Email="micorre3o@euroval.com"},
            };

        public static Reserva[] Reservas { get; } = 
            Pistas.SelectMany(
                s => Socios,
                (p, s) => new Reserva {
                    Id= lastId++,
                    FechaReserva= DateTime.Now.AddDays(rnd.Next(241)),
                    Duracion = new TimeSpan(0, rnd.Next(241),0),
                    PistaId = p.Id,
                    SocioId= s.Id,
                }).ToArray();

    }
}
