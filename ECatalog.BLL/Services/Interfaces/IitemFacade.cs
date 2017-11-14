using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;

namespace ECatalog.BLL.Services.Interfaces
{
    public interface IitemFacade
    {
        void AddItem(ItemDTO itemDto, string language, string path);
        ItemDTO GetItem(long itemId, string language);
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        void DeleteItem(long itemId);
        void UpdateItem(ItemDTO itemDto, string language, string path);
        List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId);
        void TranslateItem(ItemDTO itemDto, string language);
        void ActivateItem(long itemId);
        void DeActivateItem(long itemId);
        PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize);
    }
}
