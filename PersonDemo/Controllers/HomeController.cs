using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Persistence;
using PersonDemo.Models;

namespace PersonDemo.Controllers {
    public class HomeController : Controller {
        private readonly IPersons _persons;
        private readonly UnitOfWork _unitOfWork;

        public HomeController(IPersons persons, UnitOfWork unitOfWork) {
            if (persons == null) {
                throw new ArgumentNullException(nameof(persons));
            }
            if (unitOfWork == null) {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            _persons = persons;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var persons = await _persons.GetList();

            var result = new PersonsListModel {
                Persons = persons
                    .Select(CreateModel)
                    .ToArray()
            };
            return View(result);
        }

        [HttpGet]
        public IActionResult AddPerson() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonModel model) {
            if (model == null) {
                return BadRequest();
            }

            if (!Name.IsValid(model.FirstName)) {
                ModelState.AddModelError(nameof(model.FirstName), "FirstName is invalid");
            }
            if (!Name.IsValid(model.LastName)) {
                ModelState.AddModelError(nameof(model.LastName), "LastName is invalid");
            }
            if (!Age.IsValid(model.Age)) {
                ModelState.AddModelError(nameof(model.Age), "Age is invalid");
            }
            if (!ModelState.IsValid) {
                return View();
            }

            var firstName = new Name(model.FirstName);
            var lastName = new Name(model.LastName);
            var person = new Person(
                new PersonalName(firstName, lastName),
                new Age(model.Age));

            await _persons.Add(person);
            await _unitOfWork.Commit();

            var persons = await _persons.GetList();
            var result = new PersonsListModel {
                Persons = persons
                    .Select(CreateModel)
                    .ToArray()
            };
            return View("Index", result);
        }

        private static PersonModel CreateModel(Person person) {
            return new PersonModel {
                FirstName = person.PersonalName.FirstName.Value,
                LastName = person.PersonalName.LastName.Value,
                Age = person.Age.Value
            };
        }
    }
}
