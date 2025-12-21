using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refahi.Modules.Hotels.Application.Contract.Providers.DTOs;

public class HotelSearchResultDto
{
    public int HotelId { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
    public int Stars { get; set; }
    public long MinPrice { get; set; }

}
