using System.ComponentModel.DataAnnotations;

namespace EducationCenter.Service.Services.Paginations;

public class Pagination
{
    private int pageSize;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => pageSize;
        set
        {
            if (value <= 0)
                pageSize = 10;
            else if (value >= 20)
                pageSize = 20;
            else
                pageSize = value;
        }
    }
}
