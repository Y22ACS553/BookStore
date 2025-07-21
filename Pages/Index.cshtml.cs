using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreContext _context;

        public IndexModel(BookStoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public List<Book> Books { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public Book EditBook { get; set; }

        public bool IsSearch { get; set; } = false;
        public bool NoResultsFound { get; set; } = false;

        public async Task OnGetAsync(string searchTerm = null)
        {
            SearchTerm = searchTerm; // retain search term in URL or page
            IsSearch = !string.IsNullOrEmpty(searchTerm);

            if (IsSearch)
            {
                Books = await _context.Books
                    .Where(b => b.Title.Contains(searchTerm))
                    .ToListAsync();

                if (Books.Count == 0)
                {
                    NoResultsFound = true;
                }
            }
            else
            {
                Books = await _context.Books.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (Book.ID == 0)
            {
                _context.Books.Add(Book);
            }
            else
            {
                _context.Books.Update(Book);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            EditBook = await _context.Books.FindAsync(id);
            Book = EditBook;
            Books = await _context.Books.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}