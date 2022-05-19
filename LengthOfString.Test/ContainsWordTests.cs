using LengthOfString.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LengthOfString.Test
{
    [TestClass]
    public class ContainsWordTests
    {
        [TestMethod]
        public void should_return_true_when_string_contains_the_word()
        {
            "cat".ContainsWord("cat").ShouldBeTrue();
            "cat ".ContainsWord("cat").ShouldBeTrue();
            " cat ".ContainsWord("cat").ShouldBeTrue();
            "cat cat".ContainsWord("cat").ShouldBeTrue();
            "dog cat ".ContainsWord("cat").ShouldBeTrue();
            "dog cat dog".ContainsWord("cat").ShouldBeTrue();
        }

        [TestMethod]
        public void should_return_false_when_string_dont_contains_the_word()
        {
            "catt".ContainsWord("cat").ShouldBeFalse();
            "ca".ContainsWord("cat").ShouldBeFalse();
            "".ContainsWord("cat").ShouldBeFalse();
        }

        [TestMethod]
        public void edge_cases()
        {
            string? apa = null;
            apa.ContainsWord("cat").ShouldBeFalse();
        }
    }
}
