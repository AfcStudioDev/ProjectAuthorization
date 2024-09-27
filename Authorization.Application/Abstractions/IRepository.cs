using System.Linq.Expressions;

namespace Authorization.Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Create( T item );

        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> CreateAsync( T item );

        /// <summary>
        /// Поиск по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById( uint id );

        /// <summary>
        /// Поиск по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync( uint id );

        /// <summary>
        /// Получение объекта из базы данных
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Получение объекта с условием из базы данных
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Get( Func<T, bool> predicate );

        /// <summary>
        /// Удаление объекта из бд
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Remove( T item );

        /// <summary>
        /// Удаление объекта из бд
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> RemoveAsync( T item );

        /// <summary>
        /// Обновление объекта из базы данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Update( T item );

        /// <summary>
        /// Обновление объекта из базы данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> UpdateAsync( T item );

        /// <summary>
        /// Получение объекта с вложенными полями с условием из базы данных
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetWithInclude( Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties );

        /// <summary>
        /// Получение объекта с вложенными полями с условием из базы данных
        /// </summary>
        /// <param name="includeProperty"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetWithInclude( object includeProperty,
            params Expression<Func<T, object>>[] includeProperties );
    }
}
