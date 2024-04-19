namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IMasterProductRepository<T> where T : class
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
        List<T> SearchName(int id);
        
      

        
    }
}
