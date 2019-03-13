using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Persistence {
    public class EfPersons : IPersons {

        private readonly PersonsDemoContext _context;

        public EfPersons(UnitOfWork unitOfWork) {
            if (unitOfWork == null) {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            _context = unitOfWork.Context;
        }

        public async Task Add(Person person) {
            if (person == null) {
                throw new ArgumentNullException(nameof(person));
            }

            await _context.Persons.AddAsync(person);
        }

        public async Task<IReadOnlyList<Person>> GetList() {
            return await _context.Persons
                .OrderBy(p => p.PersonalName.LastName.Value)
                .ThenBy(p => p.PersonalName.FirstName.Value)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Person>> GetOlderThan(Age age) {
            if (age == null) {
                throw new ArgumentNullException(nameof(age));
            }

            return await _context.Persons
                // We should not use such expressions as we use Value Conversions for Age.
                // All Persons table will be loaded in memory before Where() clause
                .Where(p => p.Age.Value > age.Value)
                .OrderBy(p => p.PersonalName.LastName.Value)
                .ThenBy(p => p.PersonalName.FirstName.Value)
                .ToListAsync();
        }
    }
}
