using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace htmlSerializerMiri
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance=>_instance;
        public string[] HtmlTags {  get; set; }
        public string[] HtmlVoidTags { get; set; }
        private HtmlHelper() {
            var tagsJson =File.ReadAllText("seed/HtmlTags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(tagsJson);
            var voidJson = File.ReadAllText("seed/HtmlVoidTags.json");
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(voidJson);


        }


    }
}
