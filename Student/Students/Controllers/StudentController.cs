﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Data;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentAdminContext _context;

        public StudentController(StudentAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(_context.Student.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(m => m.Marks)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RollNumber,FirstName,LastName,Email,CreatedAt,UpdatedAt,Gender,Status")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RollNumber,FirstName,LastName,Email,CreatedAt,UpdatedAt,Gender,Status")] Student student)
        {
            if (id != student.RollNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.RollNumber))
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
            return View(student);
        }

        public IActionResult Delete(string rollNumber)
        {
            var existStudent = _context.Student.Find(rollNumber);
            if (existStudent == null)
            {
                //TempData["message"] = "Delete Error";
                return Json(existStudent);
            }
            _context.Student.Remove(existStudent);
            _context.SaveChanges();
            //TempData["message"] = "Delete Success";
            return Json(existStudent);
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.RollNumber == id);
        }
    }
}
