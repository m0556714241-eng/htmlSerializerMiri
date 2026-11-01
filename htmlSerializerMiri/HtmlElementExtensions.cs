using System;
using System.Collections.Generic;
using System.Linq;

namespace htmlSerializerMiri
{
    internal static class HtmlElementExtensions
    {

        public static HashSet<HtmlElement> FindElements(this HtmlElement element, Selector selector)
        {
            var result1 = new HashSet<HtmlElement>();
            FindRecursive(element, selector, result1);
            return result1;
        }


        private static void FindRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> result)

        {
          
            var dec=element.Descendants().Skip(1).Where(e=>IsMach(e, selector));
            if(selector.Child==null)
            {
                result.UnionWith(dec);
                return;
            }
            foreach (var e in dec) { 
                FindRecursive(e, selector.Child, result);
            }



        }
        private static bool IsMach(HtmlElement element, Selector selector)
        {
            if (selector.TagName != null && element.Name != selector.TagName)
            {
                return false;


            }
            if (selector.Id != null && selector.Id != element.Id)
                return false;
            if (!selector.Classes.All(c => element.Classes.Contains(c)))
                return false;
            return true;
        }
    }
}
