using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model {
    public interface IPersons {
        Task Add(Person person);
        Task<IReadOnlyList<Person>> GetList();
        Task<IReadOnlyList<Person>> GetOlderThan(Age age);
    }
}
