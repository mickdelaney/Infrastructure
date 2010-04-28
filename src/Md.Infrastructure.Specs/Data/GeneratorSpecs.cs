using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using elevate.spec.unit;
using Machine.Specifications;
using Machine.Specifications.Model;
using Md.Infrastructure.Data;

namespace Md.Infrastructure.Specs
{
    [Subject(typeof(Random))]
    public class when_a_random_list_is_created : SpecificationFor<Generate>
    {
        static IEnumerable<int> Numbers;

        Because of = () =>
        {
            Numbers = Subject.NumberList(1, 10);
        };

        It should_generate_the_correct_number_of_elements = () => Numbers.Count().ShouldEqual(10);
    }
}
