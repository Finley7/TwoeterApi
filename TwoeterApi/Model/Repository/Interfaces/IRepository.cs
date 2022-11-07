namespace TwoeterApi.Model.Repository.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        ///  Update existing entity
        /// </summary>
        /// <param name="entity">Entity that has to be updated</param>
        /// <typeparam name="T">The entity type</typeparam>
        public void Update<T>(T entity) where T : class;
        
        /// <summary>
        ///  Create new entity
        /// </summary>
        /// <param name="entity">Entity that has to be created</param>
        /// <typeparam name="T">The entity type</typeparam>
        public void Create<T>(T entity) where T : class;
        
        /// <summary>
        /// Delete existing entity
        /// </summary>
        /// <param name="entity">Entity that has to be deleted</param>
        /// <typeparam name="T">The entity type</typeparam>
        public void Delete<T>(T entity) where T : class;
        /// <summary>
        /// Finds all entities
        /// </summary>
        /// <returns></returns>
        public List<T> FindAll<T>() where T : class;

        /// <summary>
        /// Find an entity based on a simple operator
        /// </summary>
        /// <param name="key">The property of the entity</param>
        /// <param name="value">The value of the property</param>
        /// <param name="op">The operator</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns></returns>
        public IQueryable<T> FindBy<T>(string key, string value, string op = "=") where T : class;
    }
}
