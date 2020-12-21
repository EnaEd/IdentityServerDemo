using ClientBlazorIdentity.Models;
using System.Collections.Generic;

namespace ClientBlazorIdentity.Pages
{
    public partial class Index
    {
        public IEnumerable<Person> People { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            People = new Person[]
            {
                new Person { Salutation = "Mr", GivenName = "Bob", FamilyName = "Geldof" },
                new Person { Salutation = "Mrs", GivenName = "Angela", FamilyName = "Rippon" },
                new Person { Salutation = "Mr", GivenName = "Freddie", FamilyName = "Mercury" }
            };
        }
    }
}
