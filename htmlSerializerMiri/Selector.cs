using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace htmlSerializerMiri
{
    internal class Selector
    {
        public string? TagName { get; set; }
        public string? Id { get; set; }
        public List<string> Classes { get; set; } = new();
        public Selector? Parent { get; set; }
        public Selector? Child { get; set; }
        public static Selector parse(string query)
        {

            var parts = query.Split(' ');
            var root = new Selector();
            var current = root;
            for(int i=0; i<parts.Length; i++)
            {
                var pieces = new Regex("(?=[#.])").Split(parts[i]);
                foreach (var piece in pieces)
                {
                    if (piece.Length == 0) continue;
                    if (piece.StartsWith('#'))
                        current.Id = piece.Substring(1);
                    else if (piece.StartsWith('.'))
                        current.Classes.Add(piece.Substring(1));
                    else if(HtmlHelper.Instance.HtmlTags.Contains(piece))
                        current.TagName = piece;


                }
                if(i < parts.Length - 1)
                {
                    var next = new Selector();
                    current.Child = next;
                    next.Parent = current;
                    current = next;

                }

            }
            return root;
        }

    }
}
