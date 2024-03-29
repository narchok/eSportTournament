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
    public class EditModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;

        public EditModel(eSportTournament.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Competition Competition { get; set; }
        [BindProperty]
        public List<Match> Matchs { get; set; }

        [BindProperty]
        public Match Match { get; set; }
        [BindProperty]
        public int nbMatchs { get; set; }

        [BindProperty]
        public string nbMatchText { get; set; }


        [BindProperty]
        public int SelectedTeamA { get; set; }

        [BindProperty]
        public int SelectedTeamB { get; set; }

        public IList<Equipe> EquipeAll { get; set; }

        [BindProperty]
        public bool showTree { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.ID == id);
            var nb = getNbMatchPremierTour(Competition.nombreJoueurs);
            var test = await _context.Matchs.Where(m => m.CompetitionID == id).ToListAsync();
            nbMatchs = nb - test.Count;
            if(nbMatchs == 0) { 
                showTree = true; 
            }
            var temp = await _context.Equipes.Where(e => e.valider == true).ToListAsync();
            var copy = temp;
            for (int i = 0; i < temp.Count; i++)
            {
                var matchs = await _context.Matchs.Where(m => m.gagnantID == null && m.CompetitionID == id && (m.EquipeAID == temp[i].ID || m.EquipeBID == temp[i].ID)).ToListAsync();
                if (matchs.Count > 0) { 
                    copy.RemoveAt(i);
                    i -= 1;
                }
            }
       
            ViewData["EquipeBID"] = new SelectList(copy, "ID", "nomEquipe");
            ViewData["EquipeAID"] = new SelectList(copy, "ID", "nomEquipe");
            if (Competition == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Competition = await _context.Competitions.FirstOrDefaultAsync(m => m.ID == id);
            var nb = getNbMatchPremierTour(Competition.nombreJoueurs);
            var test = await _context.Matchs.Where(m => m.CompetitionID == id).ToListAsync();

            Equipe equipeA = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == SelectedTeamA);
            Equipe equipeB = await _context.Equipes.FirstOrDefaultAsync(e => e.ID == SelectedTeamB);

           Match.EquipeA = equipeA;
            Match.EquipeB = equipeB;
          
            Match.CompetitionID = id;
            Match.Competition = Competition;
            _context.Matchs.Add(Match);
            await _context.SaveChangesAsync();

            nbMatchs = nb - test.Count;
            if(nbMatchs == 1)
            {
                return RedirectToPage("./EditMatch", new { id = Match.ID });
            }
            else
            {
                return RedirectToPage("./Edit", new { id = id });
            }

        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.ID == id);
        }
        private int getNbMatchPremierTour(int nbEquipe)
        {
            return nbEquipe / 2;
        }
    }
}
