using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSportTournament.Data;
using eSportTournament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace eSportTournament.Pages.Competitions
{
    [AllowAnonymous]

    public class IndexModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public IndexModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Competition> Competition { get;set; }

        [BindProperty]
        public bool showDelete { get; set; }

        [BindProperty]
        public bool showCreate { get; set; }

        [BindProperty]
        public string[] jeuxNoms { get; set; }
        public async Task OnGetAsync()
        {
            
            Competition = await _context.Competitions.Where(c => c.isTournoi == false).ToListAsync();
            var user = await _userManager.GetUserAsync(User);
           if(user != null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                bool isOrgaOrAdmin = await _userManager.IsInRoleAsync(user, "Organisateur");
                showDelete = isAdmin;
                showCreate = isOrgaOrAdmin || isAdmin;
            }
            string[] temp = new string[Competition.Count];
           for(int i = 0; i < temp.Length; i++)
            {
                var jeu = await _context.Jeux.FirstOrDefaultAsync(j => j.ID == Competition[i].jeuId);
                temp[i] = jeu != null ? jeu.nomJeu : "";
            }
            jeuxNoms = temp;
        
        }
    }
}
