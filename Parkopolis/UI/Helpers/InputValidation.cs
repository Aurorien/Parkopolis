using Parkopolis.Interfaces;

namespace Parkopolis.UI.Helpers
{
    public class InputValidation
    {
        public static void InvalidMenuInput(IUI ui)
        {
            ui.WriteLine("Invalid input. Write a number corresponding to an option in the menu.\n");
            UIHelper.ReturnToMainMenuInfo(ui);
        }

        public static void NotAvailableInput(IUI ui)
        {
            ui.WriteLine("Not available option. Try another option in the menu.");
            UIHelper.ReturnToMainMenuInfo(ui);
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

        public static string GetStringInput(string prompt, IUI ui)
        {
            while (true)
            {
                ui.Write(prompt);
                string? input = ui.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                    InvalidMenuInput(ui);
                else
                    return input;
            }
        }

        public static string GetAddRegNumInput(IUI ui, IHandler garageHandler)
        {
            while (true)
            {
                ui.Write("\nRegistration number: ");
                string? input = ui.ReadLine().ToLower();

                if (garageHandler.IsRegNumExists(input))
                {
                    ui.WriteLine("Registration number already exists! Try another one or enter 0 to return to main menu.");
                }
                else if (string.IsNullOrEmpty(input))
                {
                    InvalidMenuInput(ui);
                }
                else
                {
                    return input;
                }
            }
        }

    }
}

