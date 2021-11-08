using DSCC._8392.FrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSCC._8392.FrontEnd.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient()
        {
            //URL to access microservice API
            BaseAddress = new Uri("http://ec2-34-203-40-166.compute-1.amazonaws.com/")
        };
        // GET: Book
        public async Task<ActionResult> Index()
        {
            //get all books
            List<Book> books = new List<Book>();

            HttpResponseMessage response = await _httpClient.GetAsync("api/books");

            if (response.IsSuccessStatusCode)
            {
                books = await ReadResponse<List<Book>>(response);
            }
            return View(books);
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //get particular book
            Book book = await GetBook(id);
           if(book == null)
            {
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/Create
        public async Task<ActionResult> Create()
        {
          
            var bookViewModel = new Book();
            //populate dropdown menu with book genres
            bookViewModel.Genres = await PrepareSelectListData();
            return View(bookViewModel);
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            //create book
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/books/", book);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(book);
            }
            catch
            {

                book.ErrorMessage = "Something went wrong";
                return View(book);
            }
        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //show book info in edit form 
            Book book = await GetBook(id);
            if(book==null)
            {
                return RedirectToAction("Index");
            }
            //populate dropdown menu with books
            book.Genres = await PrepareSelectListData();
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            //edit particular book
            try
            {
                var result = await _httpClient.PutAsJsonAsync("api/books/" + book.Id, book);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(book);
            }
            catch
            {
                book.ErrorMessage = "Something went wrong";
                return View(book);
            }
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //show info about book to delete
            Book book = await GetBook(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Book book)
        {
            //delete particular book
            try
            {

                var result = await _httpClient.DeleteAsync("api/books/" + book.Id);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(book);
            }
            catch
            {
                book.ErrorMessage = "Something went wrong";
                return View(book);
            }
        }
        //method for populating dropdown with genres
        private async Task<SelectList> PrepareSelectListData()
        {
            List<Genre> genres = new List<Genre>();
            //get all genres
            HttpResponseMessage genreResponse = await _httpClient.GetAsync("api/books/genres");

            if (genreResponse.IsSuccessStatusCode)
            {
                var genreResponseStr = await genreResponse.Content.ReadAsStringAsync();

                genres = JsonConvert.DeserializeObject<List<Genre>>(genreResponseStr);
            }
            //put genres in select list
            return new SelectList(genres, "Id", "Title");
        }
        //reading http response is in separate method in order to avoid repetitive code
        private async Task<T> ReadResponse<T>(HttpResponseMessage message)
        {
            var responseStr = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseStr);
        }
        //method for getting particular book is in separate method to avoid repetitive code
        private async Task<Book> GetBook(int id)
        {
            Book book = null;
            HttpResponseMessage response = await _httpClient.GetAsync("api/books/" + id);

            if (response.IsSuccessStatusCode)
            {
                book = await ReadResponse<Book>(response);
            }
            return book;
        }
    }
}
