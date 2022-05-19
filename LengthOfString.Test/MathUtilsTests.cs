using LengthOfString.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;

namespace LengthOfString.Test
{
    [TestClass]
    public class MathUtilsTests
    {
        [TestMethod]
        public void T1()
        {
            MathUtils.Flow(new List<int> {
                700, 200,
                300, 400, 100, 50},

                1000).ShouldBe(
                new List<List<int>>
                {
                    new List<int> { 700, 200},
                    new List<int> { 300, 400, 100, 50},
                }
                );
        }

        [TestMethod]
        public void T2()
        {
            MathUtils.Flow(new List<int> {
                700,
                800, 50, 150,
                10, 20},

                1000).ShouldBe(
                new List<List<int>>
                {
                    new List<int> { 700 },
                    new List<int> { 800, 50, 150},
                    new List<int> { 10, 20},
                }
                );
        }

        [TestMethod]
        public void T3()
        {
            MathUtils.Flow(new List<int> {
                10, 20, 30, 40, 50
            },

                1000).ShouldBe(
                new List<List<int>>
                {
                    new List<int> { 10, 20, 30, 40, 50 }
                }
                );
        }

        [TestMethod]
        public void T4()
        {
            MathUtils.Flow(new List<int> {
                1000
            },

                1000).ShouldBe(
                new List<List<int>>
                {
                    new List<int> { 1000 }
                }
                );
        }

        [TestMethod]
        public void T5()
        {
            MathUtils.Flow(new List<int> {
            },

                1000).ShouldBe(
                new List<List<int>>
                {
                    new List<int> {}
                }
                );
        }

        [TestMethod]
        public void should_throw_exception_if_one_width_is_too_high()
        {
            try
            {
                MathUtils.Flow(new List<int> { 1001 }, 1000);

            } catch (ArgumentException)
            {
                return;
            }
            Assert.Fail();
                    
        }


    }
}
