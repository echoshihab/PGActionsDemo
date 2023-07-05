using COSM.PatientPortal.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace TestProject1
{
    [ExcludeFromCodeCoverage]
    public abstract class TestBase
    {
        protected static TestDatabaseFixture fixture;

        static TestBase()
        {
            fixture = new TestDatabaseFixture();
        }
        /// <summary>
        /// Runs cleanup after each test execution.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestCleanup]
        public virtual Task CleanupAsync() => Task.CompletedTask;

        /// <summary>
        /// Runs initialization before each test execution.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [TestInitialize]
        public virtual Task InitializeAsync() => Task.CompletedTask;
    }
}
