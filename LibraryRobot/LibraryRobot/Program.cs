using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LibraryRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> isbnValues = new Dictionary<string, string>();

            isbnValues.Add("0312261217", "Air-Geoff Ryman-2005-St. Martin's Press");
            isbnValues.Add("0849917905", "Black-Ted Dekker-2004-W Pub Group");
            isbnValues.Add("0441785654", "Steel Beach-John Varley-1992-Ace");
            isbnValues.Add("0451451112", "Strata-Terry Pratchett-1981-Roc");
            isbnValues.Add("044901827X", "Anima-Marie Buchanan-1972-Ballantine Books");
            isbnValues.Add("1439541523", "The War of the Worlds-H G Wells-2008-Paw Prints");
            isbnValues.Add("0586066535", "One Million Tomorrows-Bob Shaw-1970-Grafton");
            isbnValues.Add("0515034797", "Mission of Gravity-Hal Clement-1954-Pyramid");
            isbnValues.Add("0553261711", "Halo-Paul Cook-1986-Random House Publishing");
            isbnValues.Add("0812520475", "Eon-Greg Bear-1991-Tor Science Fiction");
            isbnValues.Add("9353110335", "Panchatantra-Bharat-2017-Self");
            isbnValues.Add("9384564192", "Bhagavad Gita-Swami Prabhu Pada-2014-Bhaktivedanta Book Trust");
            

            bool Flag = false;
            do
            {
                Console.WriteLine("\nEnter: \n1) search - To search for a book. \n2) add - To add an item to the database. \n3) remove - To remove an item from the database. \n4) view - To view the database. \n5) quit - To quit the program.");
                string input = Console.ReadLine();
                input.ToLower();

                if (input == "search")
                {
                    bool Flag2 = false;
                    Console.Write("\nEnter a valid ISBN code: ");
                    string isbn = Console.ReadLine();

                    if (IsValid(isbn))
                    {                       
                        foreach (KeyValuePair<string, string> obj in isbnValues)
                        {
                            if (isbn == obj.Key)
                            {
                                string fnlStr = obj.Value;
                                string[] splStr = fnlStr.Split('-');
                                Console.WriteLine("\nThe book is '" + splStr[0] + "' written by " + splStr[1] + " in " + splStr[2] + " published by " + splStr[3] + ".");
                                Flag = true;
                                Flag2 = true;
                                Console.WriteLine("\nThank you for using this program. ");
                            }

                            else
                            {
                                continue;
                            }
                        } 
                        
                        if (Flag2 == false)
                        {
                            Console.WriteLine("\nBook not found. ");
                        }
                    }

                    else
                    {
                        Console.WriteLine("\nInvalid ISBN. ");
                    }
                }

                else if (input == "add")
                {                    
                    Console.Write("\nEnter the book name: ");
                    string book = Console.ReadLine();
                    Console.Write("Enter the author's name: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter the year of publication: ");
                    string year = Console.ReadLine();
                    Console.Write("Enter the publisher's name: ");
                    string publisher = Console.ReadLine();
                    Console.Write("Enter the ISBN code: ");
                    string isbn = Console.ReadLine();

                    if (IsValid(isbn))
                    {
                        try
                        {
                            string compressedString = (book + "-" + author + "-" + year + "-" + publisher);
                            isbnValues.Add(isbn, compressedString);
                            Console.WriteLine("\nDatabase updated. \nThank you for using this program. ");
                            Console.WriteLine("\nEnter 'view' to see the final database. ");
                        }
                        
                        catch (Exception)
                        {
                            Console.WriteLine("\nThe book already exists. ");
                        }
                    }

                    else
                    {
                        Console.WriteLine("\nInvalid ISBN. \nPlease try again. ");
                    }
                }

                else if (input == "remove")
                {
                    bool Flag3 = false;
                    Console.Write("Enter the ISBN of the book. ");
                    string isbnRem = Console.ReadLine();

                    foreach (KeyValuePair<string, string> data in isbnValues)
                    {
                        if (data.Key == isbnRem)
                        {
                            isbnValues.Remove(isbnRem);
                            string[] splData = data.Value.Split('-');
                            Console.WriteLine("\n" + splData[0] + " has been removed. ");
                            Console.WriteLine("\nEnter 'view' to view the final database. ");
                            Flag3 = true;
                        }

                        else
                        {
                            continue;
                        }
                    }

                    if (Flag3 == false)
                    {
                        Console.WriteLine("\nCorresponding book not found. ");
                    }
                }

                else if (input == "view")
                {
                    Console.WriteLine("|           BOOK           |        AUTHOR        |  YEAR  |           PUBLISHER           |   ISBN   |");
                    foreach (KeyValuePair<string, string> obj in isbnValues)
                    {
                        string ISBN = obj.Key;
                        string data = obj.Value;
                        string[] dataSpl = data.Split('-');                      
                        Console.WriteLine("|" + dataSpl[0] + String.Concat(Enumerable.Repeat(" ", 26 - dataSpl[0].Length)) + "|" + dataSpl[1] + String.Concat(Enumerable.Repeat(" ", 22 - dataSpl[1].Length)) + "|" + dataSpl[2] + String.Concat(Enumerable.Repeat(" ", 8 - dataSpl[2].Length)) + "|" + dataSpl[3] + String.Concat(Enumerable.Repeat(" ", 31 - dataSpl[3].Length)) + "|" + ISBN + "|"); 
                    }
                    Flag = true;
                    Console.WriteLine("\nThank you for using this program. ");
                }

                else if (input == "quit")
                {
                    Console.WriteLine("\nThank you for using this program. ");
                    Flag = true;
                }

                else
                {
                    Console.WriteLine("\nInvalid entry. \nPlease try again. ");
                }
            }
            while (Flag == false);
            Console.ReadLine();
        }

        static bool IsValid(string ISBN)
        {
            int sum1 = 0;
            try
            {
                int intISBN = Int32.Parse(ISBN.Substring(0, 9));
            }
            catch (Exception)
            {
                Console.WriteLine("\nThe first 9 characters must be numeric. ");
            }
            
            string[] arrISBN = new string[10];
            
            if (ISBN.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < ISBN.Length - 1; i++)
            {
                int x = Convert.ToInt32(ISBN[i]) - 48;
                if (x < 0 || x > 9)
                {
                    return false;
                }

                else
                {
                    sum1 += x * (10 - i);
                }
            }

            char endChar = ISBN[9];

            if (endChar != 'X' && (endChar < '0' || endChar > '9'))
            {
                return false;
            }
            else
            {
                if (endChar == 'X')
                {
                    sum1 += 10;
                }

                else
                {
                    int intEndChar = Convert.ToInt32(endChar) - 48;
                    sum1 += intEndChar;
                }
            }
            return (sum1 % 11 == 0);
        }   

    }
}
