using Aggregetter.Aggre.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Services.PaginationService
{
    public interface IPaginationService
    {
        (bool PreviousPage, bool NextPage) GetPagedUris(PagedRequest pageRequest, string endpoint, int recordCount);
    }
}
