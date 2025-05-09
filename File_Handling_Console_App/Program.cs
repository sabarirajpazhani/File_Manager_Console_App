using System;
using System.Configuration;
using System.Text.RegularExpressions;
namespace File_Handling_Console.App
{
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
                    Console.WriteLine("                        4. Delete the content inside the file        ");
                    Console.WriteLine("                        5. Delete File                               ");
                    Console.WriteLine("                        6. Exit                                      ");
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
                        if (choice > 6)
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
                                Console.Write("Enter the File Name to create new File : ");
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

                                if (!File.Exists(filePath))
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

                        case 2:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("             You Enter '2' for Reading the created File              ");
                            Console.ResetColor();
                            Console.WriteLine();

                            try
                            {
                                ReadFile:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the File Name for Read : ");
                                Console.ResetColor();
                                string fileName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(fileName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File name should not be Empty");
                                    Console.ResetColor();
                                    goto ReadFile; ;
                                }
                                if (!Regex.IsMatch(fileName, @"^[A-Za-z]+\.txt$"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter the file name using only letters—no numbers or special symbols.");
                                    Console.ResetColor();
                                    goto ReadFile;
                                }

                                string filePath = Path.Combine(DirectoryPath, fileName);

                                if (!File.Exists(filePath))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File Not Found ");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("If you want to create file Press 'C' ");
                                    Console.WriteLine("If you want to Re-Enter the file name Press 'R' ");

                                    char ch = char.Parse(Console.ReadLine());
                                    if(ch == 'C' || ch == 'c')
                                    {
                                        goto CreateFile;
                                    }
                                    else if(ch =='R'  || ch == 'r')
                                    {
                                        goto ReadFile;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter the Decison Properly ");
                                        Console.ResetColor();
                                    }
                                    
                                }
                                else
                                {
                                    FileInfo fi = new FileInfo(filePath);
                                    if (fi.Length == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("The file exists but contains no data.");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        string content;
                                        using (StreamReader sr = new StreamReader(filePath))
                                        {
                                            content = sr.ReadToEnd();
                                        }

                                        if (content == null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Empty File");
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("               ------- Content inside the File -------               ");
                                            Console.ResetColor();
                                            Console.WriteLine();

                                            Console.WriteLine(content);

                                            Console.WriteLine();
                                            Console.WriteLine("               ---------------------------------------               ");
                                            Console.WriteLine();

                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("Content in the file was successfully read  :)");
                                            Console.ResetColor();
                                        }
                                    }
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

                        case 3:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("       You Enter '3' for Appending (Writing) the created File        ");
                            Console.ResetColor();
                            Console.WriteLine();

                            try
                            {
                                WriteFile:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the File Name for Appending : ");
                                Console.ResetColor();
                                string fileName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(fileName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File name should not be Empty");
                                    Console.ResetColor();
                                    goto WriteFile; ;
                                }
                                if (!Regex.IsMatch(fileName, @"^[A-Za-z]+\.txt$"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter the file name using only letters—no numbers or special symbols.");
                                    Console.ResetColor();
                                    goto WriteFile;
                                }

                                string filePath = Path.Combine(DirectoryPath, fileName);

                                if (!File.Exists(filePath))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File Not Found ");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("If you want to create file Press 'C' ");
                                    Console.WriteLine("If you want to Re-Enter the file name Press 'R' ");

                                    char ch = char.Parse(Console.ReadLine());
                                    if (ch == 'C' || ch == 'c')
                                    {
                                        goto CreateFile;
                                    }
                                    else if (ch == 'R' || ch == 'r')
                                    {
                                        goto WriteFile;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter the Decison Properly ");
                                        Console.ResetColor();
                                    }

                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("               -------- Enter the Content Here -------               ");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    string contentToAppend = Console.ReadLine();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("               ---------------------------------------               ");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                        


                                    using (StreamWriter sw = new StreamWriter(filePath, append: true))
                                    {
                                        sw.WriteLine(contentToAppend);
                                    }

                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("Thank you for entering the content for the file  :)");
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Your content was appended successfully :)");
                                    Console.ResetColor();
                                   
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

                        case 4:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("         You Enter '4' for Deleting the content of the  File         ");
                            Console.ResetColor();
                            Console.WriteLine();

                            try
                            {
                            DeleteFile:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the File Name for Delete : ");
                                Console.ResetColor();
                                string fileName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(fileName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File name should not be Empty");
                                    Console.ResetColor();
                                    goto DeleteFile; ;
                                }
                                if (!Regex.IsMatch(fileName, @"^[A-Za-z]+\.txt$"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter the file name using only letters—no numbers or special symbols.");
                                    Console.ResetColor();
                                    goto DeleteFile;
                                }

                                string filePath = Path.Combine(DirectoryPath, fileName);

                                if (!File.Exists(filePath))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File Not Found ");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("If you want to create file Press 'C' ");
                                    Console.WriteLine("If you want to Re-Enter the file name Press 'R' ");

                                    char ch = char.Parse(Console.ReadLine());
                                    if (ch == 'C' || ch == 'c')
                                    {
                                        goto CreateFile;
                                    }
                                    else if (ch == 'R' || ch == 'r')
                                    {
                                        goto DeleteFile;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter the Decison Properly ");
                                        Console.ResetColor();
                                    }

                                }
                                else
                                {
                                    FileInfo fi = new FileInfo(filePath);
                                    if (fi.Length == 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Already the file is Empty");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        File.WriteAllText(filePath, String.Empty);

                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine("Thank you for Deleting the content form the file  :)");
                                        Console.ResetColor();
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("Your content was Deleted successfully :)");
                                        Console.ResetColor();
                                    }
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


                        case 5:
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("                  You Enter '5' for Delete the File                  ");
                            Console.ResetColor();
                            Console.WriteLine();

                            try
                            {
                            DeleteFile:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the File Name for Delete the file : ");
                                Console.ResetColor();
                                string fileName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(fileName))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File name should not be Empty");
                                    Console.ResetColor();
                                    goto DeleteFile; ;
                                }
                                if (!Regex.IsMatch(fileName, @"^[A-Za-z]+\.txt$"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Enter the file name using only letters—no numbers or special symbols.");
                                    Console.ResetColor();
                                    goto DeleteFile;
                                }

                                string filePath = Path.Combine(DirectoryPath, fileName);

                                if (!File.Exists(filePath))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File not Found");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("If you want to create file Press 'C' ");
                                    Console.WriteLine("If you want to Re-Enter the file name Press 'R' ");

                                    char ch = char.Parse(Console.ReadLine());
                                    if (ch == 'C' || ch == 'c')
                                    {
                                        goto CreateFile;
                                    }
                                    else if (ch == 'R' || ch == 'r')
                                    {
                                        goto DeleteFile;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter the Decison Properly ");
                                        Console.ResetColor();
                                    }

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write($"If You want to Delete {filePath} (Y/N) :  ");
                                    Console.ResetColor();
                                    char ch = char.Parse(Console.ReadLine());  
                                    if(ch == 'Y' || ch == 'y')
                                    {
                                        File.Delete(filePath);
                                        
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("Your File was Deleted successfully :)");
                                        Console.ResetColor();
                                    }
                                    else if(ch == 'N' || ch == 'n')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("Okay thnak You...!!!");
                                        Console.ResetColor();
                                    }
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

                        case 6:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("                     You Choose to Exist :)                  ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();

                            for (int i = 5; i > 0; i--)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("                 Existing From File Manager : ");
                                Console.ResetColor();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($" {i} ");
                                Console.ResetColor();
                                Thread.Sleep(1000);
                            }
                            break;
                    }
                    if (Choice == 6)
                    {

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("                      ~ * Thank You * ~                    ");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
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