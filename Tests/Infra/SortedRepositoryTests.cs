using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra
{
    [TestClass]
    public class SortedRepositoryTests:AbstractClassTest<SortedRepository<Measure, MeasureData>, BaseRepository<Measure, MeasureData>>
    {
        private class TestClass : SortedRepository<Measure, MeasureData>
        {
            public TestClass(DbContext c, DbSet<MeasureData> s) : base(c, s)
            {
            }

            protected override async Task<MeasureData> getData(string id)
            {
                await Task.CompletedTask;
                return new MeasureData();
            }
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<QuantityDbContext>().UseInMemoryDatabase("TestDb").Options;
            var c = new QuantityDbContext(options);
            obj = new TestClass(c, c.Measures);
        }

        [TestMethod]
        public void SortOrderTest()
        {
            IsNullableProperty(()=>obj.SortOrder, x=>obj.SortOrder=x);
        }
        [TestMethod]
        public void DescendingStringTest()
        {
            var propertyName = GetMember.Name<TestClass>(x => x.DescendingString);
            IsReadOnlyProperty(obj, propertyName, "_desc");
        }

        [TestMethod]
        public void SetSortingTest()
        {
            void test(IQueryable<MeasureData> d, string sortOrder)
            {
                obj.SortOrder = sortOrder + obj.DescendingString;
                var set = obj.SetSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderByDescending(Param_0 => Convert(Param_0.{sortOrder}, Object))"));
                obj.SortOrder = sortOrder;
                set = obj.SetSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"Abc.Data.Quantity.MeasureData]).OrderBy(Param_0 => Convert(Param_0.{sortOrder}, Object))"));
            }

            Assert.IsNull(obj.SetSorting(null));
            IQueryable<MeasureData> data = obj.dbSet;
            obj.SortOrder = null;
            Assert.AreEqual(data, obj.SetSorting(data));
            test(data, GetMember.Name<MeasureData>(x => x.Id));
            test(data, GetMember.Name<MeasureData>(x => x.Code));
            test(data, GetMember.Name<MeasureData>(x => x.Name));
            test(data, GetMember.Name<MeasureData>(x => x.Definition));
            test(data, GetMember.Name<MeasureData>(x => x.ValidFrom));
            test(data, GetMember.Name<MeasureData>(x => x.ValidTo));
        }
        [TestMethod]
        public void CreateExpressionTest()
        { //kas teeb kõikidele nendele lambda expressioni ära
            string s;
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.ValidFrom));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.ValidTo));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Id));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Name));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Code));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Definition));
            TestCreateExpression(s =GetMember.Name<MeasureData>(x => x.ValidFrom), s+obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.ValidTo), s+obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Id),s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Name), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Code), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Definition), s + obj.DescendingString);

            TestNullExpression(GetRandom.String());
            TestNullExpression(string.Empty);
            TestNullExpression(null);

        }

        private void TestNullExpression(string name)
        {
            obj.SortOrder = name;
            var lambda = obj.CreateExpression();
            Assert.IsNull(lambda);
        }

        private void TestCreateExpression(string expected, string name = null)
        {
            name ??= expected;
            obj.SortOrder = name;
            var lambda = obj.CreateExpression();
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(expected));
        }

        [TestMethod]
        public void LambdaExpressionTest()
        {
            var name = GetMember.Name<MeasureData>(x => x.ValidFrom);
            var property = typeof(MeasureData).GetProperty(name);
            var lambda = obj.LambdaExpression(property);
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(name));
        }
        [TestMethod]
        public void FindPropertyTest()
        {
            string s;
            void Test(PropertyInfo expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.FindProperty());
            }

            Test(null, GetRandom.String());
            Test(null, null);
            Test(null, string.Empty);
            Test(typeof(MeasureData).GetProperty(s=GetMember.Name<MeasureData>(x=> x.Name)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Code)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Id)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Name)), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo)), s+obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s+obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Code)), s+obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Id)), s+obj.DescendingString);


        }
        [TestMethod]
        public void GetNameTest()
        {
            string s;
            void Test(string expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.GetName());
            }

            Test(s = GetRandom.String(), s);
            Test(s = GetRandom.String(), s + obj.DescendingString);
            Test(string.Empty, string.Empty);
            Test(string.Empty, null);
        }
        [TestMethod]
        public void SetOrderByTest()
        {
            void test(IQueryable<MeasureData> d, Expression<Func<MeasureData, object>> e, string expected)
            {
                obj.SortOrder = GetRandom.String() + obj.DescendingString;
                var set = obj.SetOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderByDescending({expected})"));
                obj.SortOrder = GetRandom.String();
                set = obj.SetOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"Abc.Data.Quantity.MeasureData]).OrderBy({expected})"));
            }

            Assert.IsNull(obj.SetOrderBy(null, null));
            IQueryable<MeasureData> data = obj.dbSet;
            Assert.AreEqual(data, obj.SetOrderBy(data, null));
            test(data, x => x.Id, "x => x.Id");
            test(data, x => x.Code, "x => x.Code");
            test(data, x => x.Name, "x => x.Name");
            test(data, x => x.Definition, "x => x.Definition");
            test(data, x => x.ValidFrom, "x => Convert(x.ValidFrom, Object)");
            test(data, x => x.ValidTo, "x => Convert(x.ValidTo, Object)");


        }
        [TestMethod]
        public void IsDescendingTest()
        {
            void Test(string sortOrder, bool expected)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.IsDescending());
            }

            Test(GetRandom.String(), false);
            Test(GetRandom.String() + obj.DescendingString, true);
            Test(string.Empty, false);
            Test(null, false);

        }
    }
}
