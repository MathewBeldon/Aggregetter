using Aggregetter.Aggre.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Services.UriService
{
    public interface IUriService
    {
        (Uri FirstUri, Uri PreviousUri, Uri NextUri, Uri LastUri) GetPagedUris(PagedRequest pageRequest, string endpoint, int recordCount);
    }
}
