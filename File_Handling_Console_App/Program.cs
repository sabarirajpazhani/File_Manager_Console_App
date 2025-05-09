using System;
using System.Configuration;
using System.Text.RegularExpressions;
namespace File_Handling_Console.App
{
    //public interface FileInterface
    //{
    //    string IsValidFile(string filename);  
    //}
    //public class FileMethods : FileInterface
    //{
    //    public string IsValidFile(string filename)   // Method to validate the file name
    //    {
            

    //        if (string.IsNullOrWhiteSpace(filename))
    //        {
    //            return "Error1"; // Empty
    //        }
    //        else if (!Regex.IsMatch(filename, @"^[A-Za-z]+\.txt$"))
    //        {
    //            return "Error2";  // Invalid format
    //        }

    //        return "None";
    //    }
    //}
    public class Program
    {
        static void Main(string[] args)
        {
            String DirectoryPath = ConfigurationManager.AppSettings["Directory_Path"];

            while (true)
            {

                if (Directory.Exists(DirectoryPath))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Your Directory Path ==>   ");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{DirectoryPath}");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("-------------------------- File Manager -----------------------------");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("                        1. Create File                               ");
                    Console.WriteLine("                        2. Read File                                 ");
                    Console.WriteLine("                        3. Append to File                            ");
                    Console.WriteLine("                        4. Delete File                               ");
                    Console.WriteLine("                        5. Exit                                      ");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("---------------------------------------------------------------------");

                    //FileInterface file = new FileMethods();

                    int Choice = 0;

                    Choice:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter the Choice for File Operations : ");
                        Console.ResetColor();
                        int choice = int.Parse(Console.ReadLine());
                        if (choice == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Choice cannot be zero.");
                            Console.ResetColor();
                            goto Choice;
                        }
                        if (choice > 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; 
                            Console.WriteLine("Choice must be between 1 and 6.");
                            Console.ResetColor();
                            goto Choice;
                        }
                        Choice = choice;
                    }
                    catch(FormatException e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter the Choice in Digit Only not in Characters, Symbols and whitespace");
                        Console.ResetColor();
                        goto Choice;
                    }
                    catch(OverflowException e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Choice, Enter Proper Choice");
                        Console.ResetColor();
                        goto Choice;
                    }

                    String FilePath = "EmptyFile";
                    
                    switch (Choice)
                    {
                        case 1:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("               You Enter '1' for Creating the New File               ");
                            Console.ResetColor();
                            Console.WriteLine();
                            CreateFile:
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the File Name : ");
                                Console.ResetColor();
                                string fileName = Console.ReadLine();

                                if(string.IsNullOrWhiteSpace(fileName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File name should not be Empty");
                                    Console.ResetColor();
                                    goto CreateFile; ;
                                }
                                if (!Regex.IsMatch(fileName, @"^[A-Za-z]+\.txt$"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter the file name using only letters—no numbers or special symbols.");
                                    Console.ResetColor();
                                    goto CreateFile;
                                }

                                string filePath = Path.Combine(DirectoryPath, fileName);

                                if (!File.Exists(fileName))
                                {
                                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("File created successfully :)"); // Assign path to global FilePath variable
                                        Console.ResetColor();
                                        Console.WriteLine();
                                    }

                                    FilePath = filePath;
                                }
                                else
                                {

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("This file already exists. \nYou may continue with update, read, and delete operations.");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                }
                            }
                            catch (UnauthorizedAccessException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Access Denied: You don't have permission to access this file or directory.\nPlease check your access rights.");
                                Console.ResetColor();
                                goto CreateFile;
                            }
                            catch (IOException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("A problem occurred while trying to read from or write to the file.\nPlease ensure the file is not being used by another process and try again.");
                                Console.ResetColor();
                                Console.WriteLine();
                            }

                            break;

                        //case 2:


                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Directory path not found. \nPlease provide the correct file path in the application configuration file (App.config).");
                    Console.ResetColor();
                    Console.WriteLine();

                }


            }


        }
    }
}