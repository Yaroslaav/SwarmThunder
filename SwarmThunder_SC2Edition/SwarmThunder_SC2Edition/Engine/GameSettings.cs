using SFML.Graphics;
using SFML.System;

public class GameSettings
{
    public static uint windowWidth = 1600;
    public static uint windowHeight = 900;
    public static int cellsAmountbyX = 10;
    public static int cellsAmountbyY = 10;
    public static int fieldWidth = 600;
    public static int fieldHeight = 600;

    public static Vector2f cellSize
    {
        get
        {
            return new((fieldWidth/cellsAmountbyX) - 4, (fieldHeight/cellsAmountbyY) - 4);
        }
    }
    public static string windowTitle = "SwarmThunder";
    
    public static GameMode gameMode = GameMode.PvP;
    public static int maxShipsAmount = 1;
    
    private static string pathToCFG = "CFGs/gameSettings.cfg";

    static GameSettings()
    {
        Load();
    }
    public static void Save()
    {
        if(!File.Exists(pathToCFG))
            return;
        var variables = typeof(GameSettings).GetFields();
        
        StreamWriter sw = new StreamWriter(pathToCFG);
        
        foreach (var variable in variables)
        {
            sw.WriteLine($"{variable.Name} {variable.GetValue(null)}");
        }
        sw.Close();
    }

    public static void Load()
    {
        if (!File.Exists(pathToCFG))
        {
            Save();
            return;
        }
        StreamReader sr = new (pathToCFG);

        while (!sr.EndOfStream)
        {
            string[] info = sr.ReadLine().Split(' ');
            if (info.Length < 2)
                continue;
            
            var foundVariable = typeof(GameSettings).GetField(info[0]);
            if (foundVariable == null)
                continue;

            switch (Type.GetTypeCode(foundVariable.FieldType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    foundVariable.SetValue(null, int.Parse(info[1]));
                    break;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    foundVariable.SetValue(null, uint.Parse(info[1]));
                    break;
                case TypeCode.String:
                    foundVariable.SetValue(null, info[1]);
                    break;
                case TypeCode.Boolean:
                    foundVariable.SetValue(null, Convert.ToBoolean(info[1]));
                    break;
            }
        }
        sr.Close();
    }
}