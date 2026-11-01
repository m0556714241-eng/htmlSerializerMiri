using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlSerializerMiri
{
    internal class HtmlElement
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();
        public List<string> Classes { get; set; } = new();
        public string InnerHtml { get; set; } = "";
        public HtmlElement? Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new();
        public IEnumerable<HtmlElement> Descendants()
        {

            Queue<HtmlElement> q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                yield return current;

                foreach (var child in current.Children)
                {
                    q.Enqueue(child);
                }
            }

        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this.Parent;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }




    }
}
