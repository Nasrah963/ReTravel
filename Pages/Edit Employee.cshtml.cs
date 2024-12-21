using DB_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;

namespace DB_Project.Pages.Shared
{
    public class Edit_EmployeeModel : PageModel
    {
        private readonly DB db;
        public DataTable dt { get; set; }

        [BindProperty]
        public int Employee_ID { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public string Trip { get; set; }

        [BindProperty]
        public DateTime Start_Date { get; set; }

        [BindProperty]
        public DateTime End_Date { get; set; }

        [BindProperty]
        public int Num_of_Children { get; set; }

        [BindProperty]
        public int Num_of_Adults { get; set; }

        public Edit_EmployeeModel(DB db)
        {
            this.db = db;
        }


        public IActionResult OnGet(int id)
        {
            // Retrieve the employee data based on the given ID
            Employee employee = db.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            Employee_ID = employee.Employee_ID;
            Role = employee.Role;
            Trip = employee.Trip;
            Start_Date = employee.Start_date;
            End_Date = employee.End_date;
            Num_of_Children = employee.Num_of_children;
            Num_of_Adults = employee.Num_of_Adults;

            return Page();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Employee");
        }
    }
}
