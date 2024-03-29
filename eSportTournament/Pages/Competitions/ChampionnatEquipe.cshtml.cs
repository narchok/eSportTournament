﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;

namespace eSportTournament.Pages.Competitions
{
    public class ChampionnatEquipeModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public ChampionnatEquipeModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Competition Competition { get; set; }

        [BindProperty]
        public int[] SelectedTeams { get; set; }
        List<SelectListItem> select { get; set; }

   

        List<Equipe> EquipeAll { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.ID == id);
            EquipeAll = new List<Equipe>();
            EquipeAll = await _context.Equipes.Where(e => e.valider == true).ToListAsync();
            int equipesCount = await _context.Equipes.Where(e => e.valider == true).CountAsync();
            
            int nb = EquipeAll.Count;
            var copy = EquipeAll;
            for (int i = 0; i < EquipeAll.Count; i++)
            {

                var matchs = await _context.Matchs.Where(m => (m.EquipeAID == EquipeAll[i].ID || m.EquipeBID == EquipeAll[i].ID) && m.gagnantID == null).ToListAsync();
                foreach (Match m in matchs)
                {
                    var compet = await _context.Competitions.Where(c => c.ID == m.CompetitionID).ToListAsync();

                    foreach (var c in compet)
                    {
                        if (c.jeuId == Competition.jeuId)
                        {
                            if(i < copy.Count && i >= 0)
                            {
                                copy.RemoveAt(i);
                                i -= 1;
                            }
                        }
                        break;
                    }
                }
            }
            select = new List<SelectListItem>(); 
            foreach (Equipe element in copy)
            {

                select.Add(new SelectListItem { Value = element.ID.ToString(), Text = element.nomEquipe });
            }
            ViewData["TEAMS"] = select;


            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<int> idEquipes = new List<int>();
            for (int i = 0; i < SelectedTeams.Length; i++)
            {
                idEquipes.Add(SelectedTeams[i]);
            }
            ListMatches(idEquipes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(Competition.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.ID == id);
        }
        // Rotate the entries one position.
      

        public void ListMatches(List<int> ListTeam)
        {
            if (ListTeam.Count % 2 != 0)
            {
                ListTeam.Add(1);
            }

            int numDays = ListTeam.Count - 1;
            int halfSize = ListTeam.Count / 2;

            List<int> teams = new List<int>();

            teams.AddRange(ListTeam.Skip(halfSize).Take(halfSize));
            teams.AddRange(ListTeam.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {

                int teamIdx = day % teamsSize;

                Equipe equipeA2 = _context.Equipes.FirstOrDefault(e => e.ID == teams[teamIdx]);
                Equipe equipeB2 = _context.Equipes.FirstOrDefault(e => e.ID == ListTeam[0]);
                Match premierMatch = new Match();
                premierMatch.CompetitionID = Competition.ID;
                premierMatch.EquipeAID = equipeA2.ID;
                premierMatch.EquipeBID = equipeB2.ID;
                premierMatch.EquipeA = equipeA2;
                premierMatch.EquipeB = equipeB2;
                _context.Matchs.Add(premierMatch);
                _context.SaveChanges();

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    Equipe equipeA = _context.Equipes.FirstOrDefault(e => e.ID == teams[firstTeam]);
                    Equipe equipeB = _context.Equipes.FirstOrDefault(e => e.ID == teams[secondTeam]);
                    Match match = new Match();
                    match.CompetitionID = Competition.ID;
                    match.EquipeAID = equipeA.ID;
                    match.EquipeBID = equipeB.ID;
                    match.EquipeA = equipeA;
                    match.EquipeB = equipeB;
                    _context.Matchs.Add(match);
                    _context.SaveChanges();

                }
            }

        }
    }
}
