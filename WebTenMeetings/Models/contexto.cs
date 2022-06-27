using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace WebTenMeetings.Models
{
    public class contexto : DbContext
    {
        public contexto(DbContextOptions<contexto> options) : base(options)
        {

        }
        public DbSet<Assembleia> Assembleias { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Pauta> Pautas { get; set; }

    }
}