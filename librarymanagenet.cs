using System;
using System.Collections.Generic;

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
}

class Borrower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> BooksBorrowed { get; set; } = new List<Book>();
}

class Library
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<Borrower> Borrowers { get; set; } = new List<Borrower>();
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Borrower");
            Console.WriteLine("3. Borrow Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. List Books");
            Console.WriteLine("6. List Borrowers");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddBook(library);
                    break;
                case 2:
                    AddBorrower(library);
                    break;
                case 3:
                    BorrowBook(library);
                    break;
                case 4:
                    ReturnBook(library);
                    break;
                case 5:
                    ListBooks(library);
                    break;
                case 6:
                    ListBorrowers(library);
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddBook(Library library)
    {
        Book book = new Book
        {
            Id = library.Books.Count + 1,
            Title = Console.ReadLine(),
            Author = Console.ReadLine(),
            IsAvailable = true,
        };

        library.Books.Add(book);
        Console.WriteLine("Book added successfully.");
    }

    static void AddBorrower(Library library)
    {
        Borrower borrower = new Borrower
        {
            Id = library.Borrowers.Count + 1,
            Name = Console.ReadLine(),
        };

        library.Borrowers.Add(borrower);
        Console.WriteLine("Borrower added successfully.");
    }

    static void BorrowBook(Library library)
    {
        Console.Write("Enter Borrower ID: ");
        int borrowerId = int.Parse(Console.ReadLine());

        Borrower borrower = library.Borrowers.Find(b => b.Id == borrowerId);
        if (borrower == null)
        {
            Console.WriteLine("Borrower not found.");
            return;
        }

        Console.Write("Enter Book ID to borrow: ");
        int bookId = int.Parse(Console.ReadLine());

        Book book = library.Books.Find(b => b.Id == bookId);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (!book.IsAvailable)
        {
            Console.WriteLine("Book is already borrowed.");
            return;
        }

        borrower.BooksBorrowed.Add(book);
        book.IsAvailable = false;
        Console.WriteLine("Book borrowed successfully.");
    }

    static void ReturnBook(Library library)
    {
        Console.Write("Enter Borrower ID: ");
        int borrowerId = int.Parse(Console.ReadLine());

        Borrower borrower = library.Borrowers.Find(b => b.Id == borrowerId);
        if (borrower == null)
        {
            Console.WriteLine("Borrower not found.");
            return;
        }

        Console.Write("Enter Book ID to return: ");
        int bookId = int.Parse(Console.ReadLine());

        Book book = library.Books.Find(b => b.Id == bookId);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (book.IsAvailable)
        {
            Console.WriteLine("This book is not borrowed.");
            return;
        }

        borrower.BooksBorrowed.Remove(book);
        book.IsAvailable = true;
        Console.WriteLine("Book returned successfully.");
    }

    static void ListBooks(Library library)
    {
        Console.WriteLine("List of Books:");
        foreach (var book in library.Books)
        {
            Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Available: {book.IsAvailable}");
        }
    }

    static void ListBorrowers(Library library)
    {
        Console.WriteLine("List of Borrowers:");
        foreach (var borrower in library.Borrowers)
        {
            Console.WriteLine($"ID: {borrower.Id}, Name: {borrower.Name}");
            Console.WriteLine("Books Borrowed:");
            foreach (var book in borrower.BooksBorrowed)
            {
                Console.WriteLine($"  Title: {book.Title}, Author: {book.Author}");
            }
        }
    }
}
