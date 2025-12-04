using Makhnitskyi.TaskPlanner.DataAccess.Abstractions;
using Makhnitskyi.TaskPlanner.Domain.Models;
using Newtonsoft.Json;

namespace Makhnitskyi.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
            private const string FileName = "work-items.json";

            private readonly Dictionary<Guid, WorkItem> _storage;

            public FileWorkItemsRepository()
            {
                if (File.Exists(FileName) && File.ReadAllText(FileName).Trim().Length > 0)
                {
                    var json = File.ReadAllText(FileName);
                    var items = JsonConvert.DeserializeObject<WorkItem[]>(json);
                    _storage = new Dictionary<Guid, WorkItem>();

                    foreach (var item in items)
                        _storage[item.Id] = item;
                }
                else
                {
                    _storage = new Dictionary<Guid, WorkItem>();
                }
            }

            public Guid Add(WorkItem workItem)
            {
                var copy = workItem.Clone();
                copy.Id = Guid.NewGuid();

                _storage.Add(copy.Id, copy);
                return copy.Id;
            }

            public WorkItem Get(Guid id)
            {
                _storage.TryGetValue(id, out var item);
                return item?.Clone();
            }

            public WorkItem[] GetAll()
            {
                var list = new List<WorkItem>();

                foreach (var item in _storage.Values)
                    list.Add(item.Clone());

                return list.ToArray();
            }

            public bool Update(WorkItem workItem)
            {
                if (!_storage.ContainsKey(workItem.Id))
                    return false;

                _storage[workItem.Id] = workItem.Clone();
                return true;
            }

            public bool Remove(Guid id)
            {
                return _storage.Remove(id);
            }

            public void SaveChanges()
            {
                var array = new List<WorkItem>(_storage.Values);
                var json = JsonConvert.SerializeObject(array, Formatting.Indented);
                File.WriteAllText(FileName, json);
            }
        }
    }


