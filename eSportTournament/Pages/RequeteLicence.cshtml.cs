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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace eSportTournament.Pages
{
    public class RequeteLicenceModel : PageModel
    {
        private readonly eSportTournament.Data.ApplicationDbContext _context;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
       // private readonly UserManager<IdentityUser> _userManager;


        public RequeteLicenceModel(UserManager<IdentityUser> userManager,eSportTournament.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<DemandeLicence> DemandeLicence { get; set; } = new List<DemandeLicence>();


        public DemandeLicence[] demandes { get; set; }
        public async Task<PageResult> OnGetAsync()
        {

            DemandeLicence = await _context.DemandeLicences.Where(d => d.approuver == false ).ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            for (int i = 0; i < DemandeLicence.Count(); i++)
            {
                if(DemandeLicence[i].approuver == true)
                {
                    DemandeLicence demande = _context.DemandeLicences.FirstOrDefault(d => d.ID == DemandeLicence[i].ID);
                    System.Console.WriteLine("USERID" + demande.userID);
                    demande.approuver = true;
                    var user = await _userManager.FindByIdAsync(demande.userID);
                    await _userManager.AddToRoleAsync(user, "Licencie");
                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToPage("./Index");
        }
    }
}
