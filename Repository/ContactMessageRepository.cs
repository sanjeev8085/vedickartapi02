using Entities.Models;
using Microsoft.EntityFrameworkCore;
//using Repository;
using Vadickart.Repository;
using VadicKart.Entity.Presentation.RequestFeatures;
using VadicKart.Repository.Contract;
namespace vedickartApi.Repository
{
    public class ContactMessageRepository(RepositoryContext repositoryContext) :RepositoryBase<ContactMassagecs>(repositoryContext), IContactMessageRepository
    {
        private readonly RepositoryContext _repositoryContext = repositoryContext;
        public async Task<PagedList<ContactMassagecs>> GetAllContactMassagecsAsync(bool trackChanges, RequestParameters? requestParameters)
        {
            var contactMassagecs = await FindAll(trackChanges)
                .OrderBy(cm => cm.CreatedAt)
                .ToListAsync();

            return PagedList<ContactMassagecs>.ToPagedList(contactMassagecs, requestParameters!.PageNumber, requestParameters.PageSize);
        }

    }
}
