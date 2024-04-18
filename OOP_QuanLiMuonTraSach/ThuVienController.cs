using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Data;
namespace OOP_QuanLiMuonTraSach
{
    internal static class ThuVienController
    {
        //Method serialize 
        public static void Serialize<T>(string path, T objects) 
        {
            try
            {
                string json = JsonSerializer.Serialize(objects, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
                File.WriteAllText(path, json, Encoding.UTF8);
            }
            catch (Exception e) { throw new Exception(); }
        }

        //Method deserialize 
        public static T Deserialize<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "");
                    return default(T);
                }
                string jsonFromFile = File.ReadAllText(path);

                if (string.IsNullOrWhiteSpace(jsonFromFile))
                {
                    return default(T);
                }

                T deserializedObject = JsonSerializer.Deserialize<T>(jsonFromFile);
                return deserializedObject;
            }
            catch (Exception e) { throw new Exception(); } 
        }
    }
}
