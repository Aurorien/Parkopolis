using Parkopolis.Interfaces;

namespace Parkopolis.UI.Helpers
{
    public static class UIHelper
    {
        public static void UIHeader(IUI ui)
        {
            ui.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------  PARKOPOLIS  ------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------------\n\n");
        }
        public static void UIMenuWrapper(Action showMenu, IUI ui, string exitText)
        {
            ui.WriteLine("\nChoose in the menu by entering a number and press enter.\n");
            showMenu();
            ui.WriteLine($"{MenuHelper.Close}. {exitText}");
            ui.Write("\nMenu choice: ");
        }

        public static void ReturnToMainMenuInfo(IUI ui)
        {
            ui.WriteLine($"\n\n\nPress Enter to return to Main Menu...");
            ui.ReadLine();
        }

        public static void CloseProgram(IUI ui)
        {
            ui.WriteLine("\n\nClosing the program...\n\n");
            Environment.Exit(0);
        }
    }
}
