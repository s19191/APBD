using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace cw2
{
    public class jsonFormatFile
    {
        public static void save(Uczelnia uczelniaTmp, string adresDolcelowy)
        {
            var dodajNaglowek = new
            {
                uczelnia = uczelniaTmp
            };
            var jsonString = JsonSerializer.Serialize(dodajNaglowek);
            File.WriteAllText(String.Concat(adresDolcelowy + "result.json"), jsonString, System.Text.Encoding.UTF8);
        }
    }
}