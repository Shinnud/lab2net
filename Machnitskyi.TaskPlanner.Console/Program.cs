
using Makhnitskyi.TaskPlanner.DataAccess;
using Makhnitskyi.TaskPlanner.Domain.Logic;
using Makhnitskyi.TaskPlanner.Domain.Models;

class Program
{
    static void Main()
    {
        var repo = new FileWorkItemsRepository();
        var planner = new SimpleTaskPlanner(repo);

        while (true)
        {
            Console.WriteLine("\n[A]dd | [B]uild plan | [M]ark done | [R]emove | [Q]uit");
            Console.Write("Enter command: ");
            var cmd = Console.ReadLine().Trim().ToUpper();

            switch (cmd)
            {
                case "A":
                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Priority (1-5): ");
                    var priority = int.Parse(Console.ReadLine());

                    repo.Add(new WorkItem { Title = title, Priority = priority });
                    repo.SaveChanges();
                    break;

                case "B":
                    var plan = planner.CreatePlan();
                    Console.WriteLine("\nTASK PLAN:");
                    foreach (var item in plan)
                        Console.WriteLine($"{item.Id} | {item.Title} | pr: {item.Priority}");
                    break;

                case "M":
                    Console.Write("Enter ID: ");
                    var id = Guid.Parse(Console.ReadLine());
                    var itemM = repo.Get(id);
                    if (itemM != null)
                    {
                        itemM.IsCompleted = true;
                        repo.Update(itemM);
                        repo.SaveChanges();
                    }
                    break;

                case "R":
                    Console.Write("Enter ID: ");
                    var rid = Guid.Parse(Console.ReadLine());
                    repo.Remove(rid);
                    repo.SaveChanges();
                    break;

                case "Q":
                    return;
            }
        }
    }
}
