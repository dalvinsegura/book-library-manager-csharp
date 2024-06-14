namespace LibraryManager
{
    class Program
    {
        static public void Main(string[] args)
        {
            // Instances of objects
            Book book1 = new Book
            {
                Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "9780316769488", Genre = "Fiction",
                IsAvailable = true
            };
            Book book2 = new Book
            {
                Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", Genre = "Fiction",
                IsAvailable = true
            };

            User user1 = new User
                { FirstName = "Alice", LastName = "Smith", UserId = "A123", BorrowedBooks = new List<Book>() };
            User user2 = new User
                { FirstName = "Bob", LastName = "Johnson", UserId = "B456", BorrowedBooks = new List<Book>() };

            Admin admin1 = new Admin
                { FirstName = "Admin", LastName = "Adminson", UserId = "Admin1", HasSpecialPermissions = true };
            Librarian librarian1 = new Librarian
                { FirstName = "Libby", LastName = "Librarian", UserId = "Librarian1", Shift = "Morning" };

            var library = new Library();

            // Add books to the library
            library.AddBook(book1);
            library.AddBook(book2);

            // Register users in the library
            library.RegisterUser(user1);
            library.RegisterUser(user2);


            // SIMULATIONS
            user1.BorrowBook(book1);
            // user2.BorrowBook(book2);

            admin1.AddBookToLibrary(
                new Book
                {
                    Title = "Rich Dad, Poor Dad", Author = "Robert Kiyosaki", Genre = "Finance", IsAvailable = true,
                    ISBN = "2323243434"
                }, library);

            Console.WriteLine($"Search for Author: \n {library.FindBookByTitle("Rich Dad, Poor Dad").Author}");

            // Simulate registering users by librarian
            librarian1.RegisterUserInLibrary(
                new User { FirstName = "Eve", LastName = "Evans", UserId = "E789", BorrowedBooks = new List<Book>() },
                library);

            // Print library details to verify state
            Console.WriteLine("Books in Library:");
            foreach (var book in library.Books)
            {
                Console.WriteLine($"{book.Title} by {book.Author}");
            }

            Console.WriteLine("\nUsers in Library:");
            foreach (var user in library.Users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} (ID: {user.UserId})");
            }
        }
    }

    class Book
    {
        // Props
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }

        // Function
        private string GetAvailableMessage()
        {
            if (IsAvailable)
            {
                return "Available";
            }
            else
            {
                return "No available";
            }
        }

        // Methods
        public void DisplayInformation()
        {
            Console.WriteLine("\n//\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\n");
            Console.WriteLine("Book's Information:");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"ISB: {ISBN}");
            Console.WriteLine($"Availability: {GetAvailableMessage()}");
            Console.WriteLine("\n//\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\\n");
        }
    }

    class User
    {
        // Props
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        //Methods
        public void BorrowBook(Book book)
        {
            if (book.IsAvailable)
            {
                BorrowedBooks.Add(book);
                book.IsAvailable = false;
            }
            else
            {
                Console.WriteLine("OPS! This book is not available for borrowing :( /n");
            }
        }

        public void ReturnBook(Book book)
        {
            foreach (Book borrowedBook in BorrowedBooks.ToList())
            {
                if (borrowedBook.Equals(book))
                {
                    BorrowedBooks.Remove(book);
                    book.IsAvailable = true;
                }
                else
                {
                    Console.WriteLine($"We didn't borrow you this book '{book.Title}'.");
                }
            }
        }
    }

    class Admin : User
    {
        // Props
        public bool HasSpecialPermissions { get; set; }

        // Methods
        public void AddBookToLibrary(Book book, Library library)
        {
            if (this.HasSpecialPermissions)
            {
                library.AddBook(book);
            }
            else
            {
                Console.WriteLine("You do not have permission to add books.");
            }
        }

        public void RemoveBookFromLibrary(Book book, Library library)
        {
            if (this.HasSpecialPermissions)
            {
                library.RemoveBook(book);
            }
            else
            {
                Console.WriteLine("You do not have permission to add books.");
            }
        }
    }

    class Librarian : User
    {
        // Props
        public string Shift { get; set; }

        // Methods
        public void RegisterUserInLibrary(User user, Library library)
        {
            library.RegisterUser(user);
        }
    }

    class Library
    {
        // Constructor
        public Library()
        {
            Books = new List<Book>();
            Users = new List<User>();
        }

        // Props
        public List<Book> Books;
        public List<User> Users;

        // Methods
        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public void RegisterUser(User user)
        {
            Users.Add(user);
        }

        public Book FindBookByTitle(string title)
        {
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }

            return null;
        }
    }
}