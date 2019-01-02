using System;
using System.Collections.Generic;
using System.Text;

namespace EurovalDataAccess.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public int PistaId { get; set; }
        public Pista Pista { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
        public DateTime FechaReserva { get; set; }
        public TimeSpan Duracion { get; set; }

    }
}
