using Entry.Game.Casting;
using Entry.Game.Directing;

namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /*
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "";
        private static string DATA_PATH = "Data/messages.txt";
        // private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_ARTIFACTS = 40;

        private static int GAME_SPEED = 1;

        */

        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            KeySerivce keySerivce = new KeySerivce();
            Player player = new Player(); 
            // start the game
            //KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            //VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director();
            director.StartGame();
                        // test comment
        }
    }    
}