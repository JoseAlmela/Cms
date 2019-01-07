using System;
using System.Collections.Generic;
using System.Text;

namespace EurovalBusinessLogic.Services.ViewModels
{
    public class ReservaViewModel
    {
        public int Id { get; set; }
        public int PistaId { get; set; }
        public PistaViewModel Pista { get; set; }
        public int SocioId { get; set; }
        public SocioViewModel Socio { get; set; }
        public DateTime FechaReserva { get; set; }
        public TimeSpan Duracion { get; set; }

    }
}
