using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelService.Models.Admin
{
    public enum FilterTable
    {
        CategoryAsc,    // по имени по возрастанию
        CategoryDesc,   // по имени по убыванию
        CostAsc,        // по возрасту по возрастанию
        CostDesc,       // по возрасту по убыванию
        TitleAsc,       // по компании по возрастанию
        TitleDesc       // по компании по убыванию
    }
}
