using System;
using System.IO;
using System.Text.Json;
using TestAcuite.Class;
namespace TestAcuite.Helpers
{
    public class ConfigHelper
    {
        const string PARAM_FILENAME = "calibration.json";

        public static Boolean SaveCalibration(CalibrationParams p)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "TestAcuite");
            Directory.CreateDirectory(specificFolder);
            string jsonString = JsonSerializer.Serialize(p);
            File.WriteAllText(Path.Combine(specificFolder, PARAM_FILENAME), jsonString);

            return true;
        }

        public static CalibrationParams GetCalibration()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "TestAcuite");
            if (File.Exists(Path.Combine(specificFolder, PARAM_FILENAME)))
            {
                string paramsString = File.ReadAllText(Path.Combine(specificFolder, PARAM_FILENAME));
                CalibrationParams newParams = JsonSerializer.Deserialize<CalibrationParams>(paramsString);
                return newParams;

            }
            else
            {
                return null;
            }

        }

    }
}
