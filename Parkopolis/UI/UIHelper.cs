using Parkopolis.Interfaces;

namespace Parkopolis.UI
{
    public static class UIHelper
    {
        public static void UIMenuWrapper(Action showMenu, IUI ui, string exitText)
        {
            ui.WriteLine("\nChoose in the menu by entering a number and press enter.\n");
            showMenu();
            ui.WriteLine($"{MenuHelper.Close}. {exitText}");
            ui.Write("\nMenu choice: ");
        }

        public static void InvalidMenuInput(IUI ui)
        {
            ui.WriteLine("Invalid input. Press Enter and try again.");
        }

        public static void NotAvailableInput(IUI ui)
        {
            ui.WriteLine("Not available option. Press Enter and try again.");
        }

        public static void ReturnToMenu(IUI ui)
        {
            ui.WriteLine("\n\n\nPress Enter to return to menu...");
            ui.ReadLine();
        }

        public static void CloseProgram(IUI ui)
        {
            ui.WriteLine("\n\nClosing the program...\n\n");
            Environment.Exit(0);
        }

        public static int GetConvertedIntInput(string prompt, IUI ui)
        {
            int value = 1;
            bool isNotValidInput = true;
            while (isNotValidInput)
            {
                ui.Write(prompt);
                string? input = ui.ReadLine();


                if (int.TryParse(input, out value) && value > 0)
                {
                    isNotValidInput = false;
                }
                else
                {
                    ui.WriteLine("Invalid input. Enter an integer from 1 and up.");
                }
            }
            return value;
        }

        public static bool GetBooleanInput(string prompt, IUI ui)
        {
            while (true)
            {
                ui.Write(prompt);
                string input = ui.ReadLine().Trim().ToLower();

                if (input == "y")
                    return true;
                else if (input == "n")
                    return false;
                else
                    ui.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
            }
        }
    }
}
