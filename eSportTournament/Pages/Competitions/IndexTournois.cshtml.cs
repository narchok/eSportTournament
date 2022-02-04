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

    public class IndexTournoisModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexTournoisModel(UserManager<IdentityUser> userManager, eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Competition> Competition { get;set; }
        [BindProperty]
        public bool showDelete { get; set; }

        public async Task OnGetAsync()
        {
            Competition = await _context.Competitions.Where(c => c.isTournoi == true).ToListAsync();
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                showDelete = isAdmin;
            }
           
        }
    }
}
