using Parkopolis.Interfaces;

namespace Parkopolis.UI
{
    public static class UIHelper
    {
        public static void UIMenuWrapper(Action showMenu, IUI ui)
        {
            ui.WriteLine("\nChoose in the menu by entering a number and press enter.\n");
            showMenu();
            ui.WriteLine($"{MenuHelper.Close}. Close program");
            ui.Write("\nMenu: ");
        }

        public static void InvalidMenuInput(IUI ui)
        {
            ui.WriteLine("Invalid input. Press Enter and try again.");
            ui.ReadLine();
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
    }
}
