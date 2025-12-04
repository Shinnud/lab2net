namespace Makhnitskyi.TaskPlanner.Domain.Models
{
    public class WorkItem
    {

            public Guid Id { get; set; }
            public string Title { get; set; }
            public int Priority { get; set; }
            public bool IsCompleted { get; set; }

            public WorkItem Clone()
            {
                return new WorkItem
                {
                    Id = this.Id,
                    Title = this.Title,
                    Priority = this.Priority,
                    IsCompleted = this.IsCompleted
                };
            }
        }
    }

