using Makhnitskyi.TaskPlanner.DataAccess.Abstractions;
using Makhnitskyi.TaskPlanner.Domain.Models;

namespace Makhnitskyi.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
            private readonly IWorkItemsRepository _repo;

            public SimpleTaskPlanner(IWorkItemsRepository repo)
            {
                _repo = repo;
            }

            public WorkItem[] CreatePlan()
            {
                return _repo.GetAll()
                            .Where(i => !i.IsCompleted)
                            .OrderByDescending(i => i.Priority)
                            .ToArray();
            }
        }
    }

