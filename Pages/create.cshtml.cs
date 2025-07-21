using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreContext _context;

        public CreateModel(BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}