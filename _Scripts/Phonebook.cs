using System;

namespace Phonebook_F3_OOP
{
    class Phonebook
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Ultra Phonebook App.");

            int tempNum = ReadInt("How big would you like the phonebook to be?");
            int arraySize = LimitNumber(tempNum, false, 2, 10);

            Entry[] entryArray = new Entry[arraySize];
            InitializeArrayEmpty(entryArray);

            Console.WriteLine("Created a phonebook of " + entryArray.Length + " entry slots!");
            PrintSeparatorLines();

            MenuSelectionSequence(entryArray);
        }

        /// <summary>
        /// Initialize the Entry array with empty entries so there are no null Exceptions
        /// </summary>
        /// <param name="entries">The entry array</param>
        private static void InitializeArrayEmpty(Entry[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                Entry tempEntry = new Entry();
                entries[i] = tempEntry;
            }
        }

        private static void MenuSelectionSequence(Entry[] entryArray)
        {
            string userChoice = null;

            while (userChoice != "0")
            {
                Console.WriteLine("Main Menu\n\n" +
                "1) List phonebook entries\n" +
                "2) Add/modify entry\n" +
                "3) Delete entry\n" +
                "4) Search\n" +
                "0) Exit\n" +
                "\n" +
                "Choice?");

                userChoice = ReadString("").Trim();
                PrintSeparatorLines();

                switch (userChoice)
                {
                    case "1":
                        ListEntries(entryArray);
                        PrintSeparatorLines();
                        break;
                    case "2":
                        AddModifyEntry(entryArray);
                        PrintSeparatorLines();
                        break;
                    case "3":
                        DeleteEntry(entryArray);
                        PrintSeparatorLines();
                        break;
                    case "4":
                        SearchEntry(entryArray);
                        PrintSeparatorLines();
                        break;
                    case "0":
                        Console.WriteLine("Terminating program");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        PrintSeparatorLines();
                        break;
                }
            }
        }

        /// <summary>
        /// Print all the elements from the Entry[] array
        /// </summary>
        /// <param name="entryArray">The array to print</param>
        static void ListEntries(Entry[] entryArray)
        {
            int slotNum = 0;

            foreach (Entry entry in entryArray)
            {
                if (entry.EntryState == EntryState.Empty || entry == null)
                    continue;

                if (entry.EntryState == EntryState.Used && entry != null)
                {
                    Console.WriteLine($"Slot {slotNum} {entry.GetEntryInfo()}");
                    slotNum++;
                }
            }

            if (slotNum == 0)
            {
                Console.WriteLine("The list is currently empty.");
            }
        }

        /// <summary>
        /// Add an entry to the Entry[] array at the user given index
        /// </summary>
        /// <param name="entryArray"></param>
        private static void AddModifyEntry(Entry[] entryArray)
        {
            string name;
            int userIdx = 0;
            long number;

            userIdx = LimitNumber(ReadInt($"Entry number? (0 - {entryArray.Length - 1})"), false, 0, entryArray.Length - 1);
            name = ReadString("Name? ");
            number = ReadLong("Phone? ");

            //++GlobalIndex;

            Entry newEntry = new Entry(userIdx, name, number);

            entryArray[userIdx] = newEntry;
        }

        /// <summary>
        /// Replace an element with an empty Entry obj at the user given index
        /// to create the deletion illusion.
        /// </summary>
        /// <param name="entryArray"></param>
        private static void DeleteEntry(Entry[] entryArray)
        {
            int entryToDelete;
            entryToDelete = LimitNumber(ReadInt($"Which entry would you like to delete? (0 - {entryArray.Length - 1})"), false, 0, entryArray.Length - 1);

            entryArray[entryToDelete] = new Entry();
            Console.WriteLine($"The entry {entryToDelete} got deleted.");
        }

        /// <summary>
        /// Search the whole Entry[] array for the user given string value
        /// </summary>
        /// <param name="entryArray"></param>
        private static void SearchEntry(Entry[] entryArray)
        {
            string searchedName;
            searchedName = ReadString("What name to search for?").Trim();

            int results = 0;
            foreach (Entry entry in entryArray)
            {
                if (entry.EntryState == EntryState.Empty)
                {
                    continue;
                }
                else if (entry.Name.ToLower() == searchedName.ToLower())
                {
                    Console.WriteLine(entry.GetEntryInfo());
                    results++;
                }
            }

            Console.WriteLine($"{results} result(s) found in the phonebook.");
        }


        //Utility methods below

        /// <summary>
        /// Prompt the user for a string input,
        /// If the input is Empty or Spaces then prompt the user again.
        /// </summary>
        /// <param name="prompt">The message to print to the user</param>
        /// <returns>A string of the users input</returns>
        static string ReadString(string prompt)
        {
            string tempStr = "";

            do
            {
                Console.WriteLine(prompt);
                tempStr = Console.ReadLine();
            } while (String.IsNullOrEmpty(tempStr) || String.IsNullOrWhiteSpace(tempStr));

            return tempStr;
        }

        /// <summary>
        /// Prompt the user to give a number, try parsing it to int,
        /// if the parsing fails print an Invalid input message.
        /// </summary>
        /// <param name="prompt">The message to print to the console for the user</param>
        /// <returns>The parsed string as an int</returns>
        static int ReadInt(string prompt)
        {
            string tempStr = "";
            int tempNum = 0;

            while (true)
            {
                tempStr = ReadString(prompt);

                if (int.TryParse(tempStr, out tempNum))
                {
                    //Parsing was succesfull
                    break;
                }
                else
                {
                    //Parsing failed, prompt user for input again
                    Console.WriteLine("Invalid input");
                }
            }

            return tempNum;
        }

        /// <summary>
        /// Prompt the user to give a number, try parsing it to long,
        /// if the parsing fails print an Invalid input message.
        /// </summary>
        /// <param name="prompt">The message to print to the console for the user</param>
        /// <returns>The parsed string as a long</returns>
        static long ReadLong(string prompt)
        {
            string tempStr = "";
            long tempNum = 0;

            while (true)
            {
                tempStr = ReadString(prompt);

                if (long.TryParse(tempStr, out tempNum))
                {
                    //Parsing was succesfull
                    break;
                }
                else
                {
                    //Parsing failed, prompt user for input again
                    Console.WriteLine("Invalid input");
                }
            }

            return tempNum;
        }

        /// <summary>
        /// Limit the number the user gave as input
        /// </summary>
        /// <param name="value">The value to check on</param>
        /// <param name="printQuestion">Print a pre made question?</param>
        /// <param name="allowZero">Allow zeros as answers?</param>
        /// <param name="min">The minimum acceptable value</param>
        /// <param name="max">The maximum acceptable value</param>
        /// <returns>The accepted number as int</returns>
        static int LimitNumber(int value, bool printQuestion, int min, int max)
        {
            int tempNum = value;

            while (true)
            {
                if (printQuestion)
                {
                    tempNum = ReadInt("Please give me a number");
                }

                if (tempNum == 0)
                {
                    break;
                }
                else if (tempNum >= min && tempNum <= max)
                {
                    //Size is acceptable
                    break;
                }
                else
                {
                    Console.WriteLine("Value is invalid");
                    printQuestion = true;
                }
            }

            return tempNum;
        }

        /// <summary>
        /// Print ---- for Style (and better readability)
        /// </summary>
        static void PrintSeparatorLines()
        {
            for (int i = 0; i < 35; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }
    }
}
