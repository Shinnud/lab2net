using Makhnitskyi.TaskPlanner.Domain.Logic;
using Makhnitskyi.TaskPlanner.DataAccess.Abstractions;
using Makhnitskyi.TaskPlanner.Domain.Models;
using Moq;
using Xunit;


namespace Makhnitskyi.TaskPlanner.Domain.Logic.Tests
{
    public class SimpleTaskPlannerTests
    {
        [Fact]
        public void TestSorting()
        {
            var items = new[]
            {
            new WorkItem { Priority = 1, IsCompleted = false },
            new WorkItem { Priority = 5, IsCompleted = false },
            new WorkItem { Priority = 3, IsCompleted = false }
        };

            var mock = new Mock<IWorkItemsRepository>();
            mock.Setup(r => r.GetAll()).Returns(items);

            var planner = new SimpleTaskPlanner(mock.Object);
            var result = planner.CreatePlan();

            Assert.Equal(5, result[0].Priority);
            Assert.Equal(3, result[1].Priority);
            Assert.Equal(1, result[2].Priority);
        }

        [Fact]
        public void TestIgnoresCompleted()
        {
            var items = new[]
            {
            new WorkItem { Priority = 1, IsCompleted = false },
            new WorkItem { Priority = 5, IsCompleted = true }
        };

            var mock = new Mock<IWorkItemsRepository>();
            mock.Setup(r => r.GetAll()).Returns(items);

            var planner = new SimpleTaskPlanner(mock.Object);
            var result = planner.CreatePlan();

            Assert.Single(result);
            Assert.False(result[0].IsCompleted);
        }
    }

}
