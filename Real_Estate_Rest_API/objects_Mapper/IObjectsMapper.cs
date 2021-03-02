using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using Real_Estate_Rest_API.DTO_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Rest_API.objects_Mapper
{
    public interface IObjectsMapper
    {
        Listing MapListingDtoToModel(ListingDTOModel modelDto);

        ListingDTOReadModel MapListingModelToDTOModel(Listing listing);



    }
}
