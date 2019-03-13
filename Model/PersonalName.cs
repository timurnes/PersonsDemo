using System;
using System.Collections.Generic;

namespace Model {
    public class PersonalName {

        protected PersonalName() { }

        public PersonalName(Name firstName, Name lastName) {
            if (firstName == null) {
                throw new ArgumentNullException(nameof(firstName));
            }
            if (lastName == null) {
                throw new ArgumentNullException(nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }

        public String FullName => $"{FirstName} {LastName}";

        public override Boolean Equals(Object obj) {
            return obj is PersonalName personalName &&
                   EqualityComparer<Name>.Default.Equals(FirstName, personalName.FirstName) &&
                   EqualityComparer<Name>.Default.Equals(LastName, personalName.LastName);
        }

        public override Int32 GetHashCode() {
            return HashCode.Combine(FirstName, LastName);
        }

        public override String ToString() {
            return FullName;
        }
    }
}
