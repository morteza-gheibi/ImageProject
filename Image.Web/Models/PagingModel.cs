using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

    public class PagingModel
    {
        public string Pagination(int total, int current, int Take , string AbsoluteUri)
        {
            if (total > 0)
            {
                var offset = 3;

            var ias= HttpContext.Current.Request.RequestContext.RouteData;
            if (AbsoluteUri.Contains("Thesaurus") || AbsoluteUri.Contains("Thesaurus"))
            {
                AbsoluteUri += "#PageIndex=";
            }
            else
            {
                
                var c_url = HttpContext.Current.Request.Url.Query.Replace("?1&","#");
                
                c_url = Regex.Replace(c_url, "pageindex=*.&", "");
                c_url = Regex.Replace(c_url, "pageindex=*.", "");
                c_url = c_url + "&PageIndex=";
                AbsoluteUri += c_url;
            }
                double rowPerPage = Take;
                if (Convert.ToDouble(total) < Take)
                {
                    rowPerPage = Convert.ToDouble(total);
                }

                var totalPage = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(total) / rowPerPage));
                current = current == 0 ? 1 : current;
                var pageStart = Convert.ToInt16(Convert.ToDouble(current) - Convert.ToDouble(offset));
                var pageEnd = Convert.ToInt16(Convert.ToDouble(current) + Convert.ToDouble(offset));
                var numPage = "";
                if (totalPage < 1) return "";
                numPage += "<section class='paging inlineblock'><div class='pagination'>";
                if (current > 1) numPage += "<a class='prev' href='" + AbsoluteUri  +  (current - 1)  + "' aria-label='Previous'>قبل</a>";
                else numPage += "<span class='prev current' aria-label='Previous'>قبل</span>";
                if (current > (offset + 1)) numPage += "<a href='" + AbsoluteUri + "1"  + "' name='page1'>1</a><span class='disabled spacing-dot'>...</span>";
                for (var i = 1; i <= totalPage; i++)
                {
                    if (pageStart <= i && pageEnd >= i)
                    {
                        if (i == current) numPage += "<span class='current'>" + i + " </span>";
                        else numPage += "<a href='" + AbsoluteUri  + i  + "'>" + i + "</a>";
                    }
                }
                if (totalPage > pageEnd)
                {
                    numPage += "<span>...</span><a href='" + AbsoluteUri + (totalPage)  + "'>" + totalPage + "</a>";
                }
                if (current < totalPage) numPage += "<a class='next' href='" + AbsoluteUri + (current + 1)  + "'>بعد</a>";
                else numPage += "<span class='next current' aria-hidden='true'>بعد</span>";
                numPage += "</div></section>";
                return numPage;
            }
            else
            {
                return "";
            }
        }
    }