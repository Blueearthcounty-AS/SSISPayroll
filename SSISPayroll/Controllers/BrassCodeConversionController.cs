using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSISPayroll.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SSISPayroll.Controllers
{
    public class BrassCodeConversionController: Controller
    {
        private readonly HSDBContext _context;

        public BrassCodeConversionController(HSDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var brassconversion = _context.SsisRoleBrassConversion;
            return View(await brassconversion.ToListAsync());
            
        }



        public IActionResult Create()
        {           
            return PartialView("_CreatePartial");
        }

        // POST: FinishedSigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SsisRoleBrassConversionId,SsisRoleId,Brass,OrgCode")] SsisRoleBrassConversion ssisrolebrassconversion)
        {
            

            if (ModelState.IsValid)
            {
                if (!SsisRoleBrassConversionExists(ssisrolebrassconversion))
                {
                    _context.Add(ssisrolebrassconversion);
                    await _context.SaveChangesAsync();

                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "This record already exist!!");
                }
                
            }


            return PartialView("_CreatePartial", ssisrolebrassconversion);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ssisrolebrassconversion = await _context.SsisRoleBrassConversion.SingleOrDefaultAsync(m => m.SsisRoleBrassConversionId == id);
            if (ssisrolebrassconversion == null)
            {
                return NotFound();
            }

            return PartialView("_EditPartial", ssisrolebrassconversion);

        }

        // POST: FinishedSigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("SsisRoleBrassConversionId,SsisRoleId,Brass,OrgCode")] SsisRoleBrassConversion ssisrolebrassconversion)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ssisrolebrassconversion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SsisRoleBrassConversionExists(ssisrolebrassconversion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(ssisrolebrassconversion);
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ssisrolebrassconversion = await _context.SsisRoleBrassConversion.SingleOrDefaultAsync(m => m.SsisRoleBrassConversionId == id);
            if (ssisrolebrassconversion == null)
            {
                return NotFound();
            }
            

            return PartialView("_DeletePartial", ssisrolebrassconversion);
        }

        // POST: FinishedSigns/Delete/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SsisRoleBrassConversion ssisrolebrassconversion)
        {

            var SsisRoleBrassConver = await _context.SsisRoleBrassConversion.FindAsync(ssisrolebrassconversion.SsisRoleBrassConversionId);
           
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SsisRoleBrassConversion.Remove(SsisRoleBrassConver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SsisRoleBrassConversionExists(ssisrolebrassconversion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool SsisRoleBrassConversionExists(SsisRoleBrassConversion ssisrolebrassconv)
        {
            return _context.SsisRoleBrassConversion.Any(e => 
            e.SsisRoleId==ssisrolebrassconv.SsisRoleId && 
            e.Brass==ssisrolebrassconv.Brass &&
            e.OrgCode==ssisrolebrassconv.OrgCode);
        }
    }
}
