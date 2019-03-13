using System;

namespace Model {
    public class Person {

        protected Person() { }

        public Person(PersonalName personalName, Age age) {
            if (personalName == null) {
                throw new ArgumentNullException(nameof(personalName));
            }
            if (age == null) {
                throw new ArgumentNullException(nameof(age));
            }

            Id = Guid.NewGuid();
            PersonalName = personalName;
            Age = age;
        }

        public Guid Id { get; private set; }

        public PersonalName PersonalName { get; set; }

        public Age Age { get; set; }
    }
}
