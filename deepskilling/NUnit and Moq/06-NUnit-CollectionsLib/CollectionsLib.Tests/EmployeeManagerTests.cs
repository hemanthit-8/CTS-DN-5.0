using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CollectionsLib.Tests
{
    [TestFixture]
    public class EmployeeManagerTests
    {
        private EmployeeManager _employeeManager;

        [SetUp]
        public void Setup()
        {
            _employeeManager = new EmployeeManager();
        }

        // Scenario 1: no null values in the collection
        [Test]
        public void GetEmployees_Collection_HasNoNullValues()
        {
            List<Employee> employees = _employeeManager.GetEmployees();

            CollectionAssert.AllItemsAreNotNull(employees);
        }

        // Scenario 2: employee with id 100 exists
        [Test]
        public void GetEmployees_Collection_ContainsEmployeeWithId100()
        {
            List<Employee> employees = _employeeManager.GetEmployees();

            bool exists = employees.Any(e => e.EmpId == 100);

            Assert.That(exists, Is.True);
        }

        // Scenario 3 (a): GetEmployees returns only unique employees (by EmpId, via Equals/GetHashCode)
        [Test]
        public void GetEmployees_Collection_ContainsOnlyUniqueEmployees()
        {
            List<Employee> employees = _employeeManager.GetEmployees();

            CollectionAssert.AllItemsAreUnique(employees);
        }

        // Scenario 3 (b): GetEmployees() and GetEmployeesWhoJoinedInPreviousYears() return the same
        // items - classic model
        [Test]
        public void GetEmployees_And_PreviousYearEmployees_AreEquivalent_ClassicModel()
        {
            List<Employee> allEmployees = _employeeManager.GetEmployees();
            List<Employee> previousYearEmployees = _employeeManager.GetEmployeesWhoJoinedInPreviousYears();

            CollectionAssert.AreEquivalent(allEmployees, previousYearEmployees);
        }

        // Same scenario - constraint model
        [Test]
        public void GetEmployees_And_PreviousYearEmployees_AreEquivalent_ConstraintModel()
        {
            List<Employee> allEmployees = _employeeManager.GetEmployees();
            List<Employee> previousYearEmployees = _employeeManager.GetEmployeesWhoJoinedInPreviousYears();

            Assert.That(previousYearEmployees, Is.EquivalentTo(allEmployees));
        }
    }
}
