using System;
using System.Collections.Generic;

namespace Shop.Domain.Entities
{
    public class PageedList<T> where T : new()
    {

        public List<T> ListItem { get; set; } = new List<T>();
        public PageData pageData { get; set; } = new PageData();
    }
    public class PageData
    {
        public decimal TotalItem { get; set; }
        public decimal ItemPerPage { get; set; }
        public int CurentItem { get; set; }
        public int TotalPages() {
            //if (TotalItem!=0&&CurentItem!=0)
            //{
            int x= (int)(Math.Ceiling((TotalItem / ItemPerPage)));
            return x;           
           // }
           // return 0;
        } 

    }
}