using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class RestaurantTypeTranslationService:Service<RestaurantTypeTranslation>,IRestaurantTypeTranslationService
    {
        public RestaurantTypeTranslationService(IRepositoryAsync<RestaurantTypeTranslation> repository) : base(repository)
        {
            _repository = repository;
        }

        public bool CheckRepeatedType(string typeName, string language,long restaurantTypeId)
        {
            var restaurantTypeTranslations =
                Query(x => x.Language.ToLower() == language.ToLower() &&
                           x.TypeName.ToLower() == typeName.ToLower() &&
                           !x.RestaurantType.IsDeleted && x.RestaurantTypeId != restaurantTypeId).Select().ToList();
            return restaurantTypeTranslations.Count > 0;
        }

        public List<RestaurantTypeTranslation> GeRestaurantTypeTranslation(string language)
        {
            return Query(x => x.Language.ToLower() == language.ToLower() && !x.RestaurantType.IsDeleted).Select(x => x).ToList();
        }
    }
}
