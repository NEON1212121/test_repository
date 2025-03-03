using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace NodeEditor.ViewModel
{
    public class MainWindowVM
    {
        public MainWindowVM()
        {
            string cuesRecord = GetCuesRecord("U:\\src\\VisualStudio\\NodeEditor\\Data\\SE_acb.json").ToString();
            System.Diagnostics.Debug.WriteLine(cuesRecord.ToString());
        }

        public TempJsonRootModel GetCuesRecord(string path)
        {
            string json = File.ReadAllText(path);
            /* --------------------------------------------------------------- */
            // 元のJSONが、Root自体が配列だった
            TempJsonRootModel record = JsonSerializer.Deserialize<TempJsonRootModel[]>(json, GetOption())[0];
            /* --------------------------------------------------------------- */
            return record;
        }

        private static JsonSerializerOptions GetOption()
        {
            // ユニコードのレンジ指定で日本語も正しく表示、インデントされるように指定
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            return options;
        }
    }


    public class TempJsonRootModel
    {
        [JsonPropertyName("Cue")]
        public IList<CueModel> cueModels { get; set; }

        public string ToString()
        {
            string s = string.Empty;
            foreach (var item in cueModels)
            {
                s += "\n";
                s += "[name]: " + item.Name;
            }
            return s;
        }
    }

    public class CueModel
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
