using eSportTournament.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSportTournament.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
       public DbSet<Equipe> Equipes { get; set; }

       public DbSet<Match> Matchs { get; set; }

        public DbSet<Jeu> Jeux { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<DemandeLicence> DemandeLicences { get; set; }
        public DbSet<DemandeOrganisateur> DemandeOrganisateurs { get; set; }

        public DbSet<DemandeEquipe> DemandesEquipes { get; set; }
        public DbSet<DemandeEquipeCreation> DemandesEquipeCreations { get; set; }

        public object Utilisateur { get; internal set; }
        public DbSet<eSportTournament.Models.CompetitionEquipe> CompetitionEquipe { get; set; }
    }
}
