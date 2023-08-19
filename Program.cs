using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using LibraryManagementConsoleApp.DATA;
using LibraryManagementConsoleApp.DATA.Models;

class Program
{
    static void Main(string[] args)
    {
        var dataManager = new DataManager();

        try 
        { 
            string jsonData = File.ReadAllText("sampleData.json").Trim();

            // Deserialize JSON data into models
            SampleData parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject<SampleData>(jsonData);        
            // Populate data manager with parsed data
            dataManager.Authors.AddRange(parsedData.Authors);
            dataManager.Books.AddRange(parsedData.Books);

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Add Author");
                Console.WriteLine("3. View Books");
                Console.WriteLine("4. Search Books");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add Book logic
                        //Get the book name
                        Console.WriteLine("Add Book selected. Please enter the new book name: ");
                        string newBookName = Console.ReadLine();

                        //Get the authors
                        Console.WriteLine("Please enter the authors name in this format (Alan, John): ");
                        string authorsNames = Console.ReadLine();
                        string[] authorsNameArray = authorsNames.Split(new char[] { ',' });
                        List<string> authorsNameList = new List<string>(authorsNameArray);
                        List<Author> authors = new List<Author>();
                        int authorIdCounter = dataManager.Authors.Count + 1;

                        foreach (string name in authorsNameList)
                        {
                            authors.Add(new Author()
                            {
                                Id = authorIdCounter,
                                Name = name
                            }); ;
                            authorIdCounter++;
                        }

                        //Get the description
                        Console.WriteLine("Please enter teh book description: ");
                        string description = Console.ReadLine();

                        int bookIdCounter = dataManager.Books.Count + 1;
                        dataManager.AddBook(new Book()
                        {
                            Id= bookIdCounter,
                            Title = newBookName,
                            Authors=authors,
                            Description=description

                        });;
                        break;
                    case "2":
                        // Add Author logic
                        Console.WriteLine("Add Author selected. This section is still under construction!");
                        // TODO
                        break;
                    case "3":
                        // View Books logic
                        Console.WriteLine("View Books selected.");
                        foreach (var book in dataManager.Books)
                        {
                            Console.WriteLine($"ID: {book.Id}");
                            Console.WriteLine($"Title: {book.Title}");

                            Console.Write("Authors: ");
                            foreach (var author in book.Authors)
                            {
                                Console.Write($"{author.Name}, ");
                            }
                            Console.WriteLine();

                            Console.WriteLine($"Description: {book.Description}");
                            Console.WriteLine($"Cover Image URL: {book.CoverImageUrl}");

                            Console.WriteLine();
                        }
                        break;
                    case "4":
                        // Search Books logic
                        Console.WriteLine("Search Books selected.  This section is still under construction!");
                        // TODO
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: JSON file not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during JSON deserialization: {ex.Message}");
        }

    }
}

public class SampleData
{
    public SampleData()
    {
        Authors = new List<Author>();
        Books = new List<Book>();
    }
    public List<Author> Authors { get; set; }
    public List<Book> Books { get; set; }
}
