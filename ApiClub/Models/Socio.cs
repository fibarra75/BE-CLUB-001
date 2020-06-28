﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiClub.Models
{
    public class Socio
    {
        public int IdSocio { get; set; }
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string Nombres { get; set; }
        public string Apaterno { get; set; }
        public string Amaterno { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public int NumeroCelular { get; set; }
        public int TipoSocio { get; set; }
        public int IdEstado { get; set; }
        public DateTime FecCreacion { get; set; }
        public DateTime FecModificacion { get; set; }
    }
}