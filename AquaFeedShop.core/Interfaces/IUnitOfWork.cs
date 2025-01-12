namespace AquaFeedShop.core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }

        int Save();

        Task<int> SaveAsync();

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
