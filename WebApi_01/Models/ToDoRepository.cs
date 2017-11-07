using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace WebApi_01.Models
{
    public class ToDoRepository : IToDoRepository
    {
        private static ConcurrentDictionary<string, ToDoItem> _todos =
            new ConcurrentDictionary<string, ToDoItem>();

        public ToDoRepository()
        {
            Add(new Models.ToDoItem { Name = "Item1" });
        }

        public void Add(ToDoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public void Update(ToDoItem item)
        {
            _todos[item.Key] = item;
        }

        public ToDoItem Find(string key)
        {
            ToDoItem item;
            _todos.TryGetValue(key,out item);
            return item;
        }

        public ToDoItem Remove(string key)
        {
            ToDoItem item;
            _todos.TryRemove(key, out item);
            return item;
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _todos.Values;
        } 

    }
}
