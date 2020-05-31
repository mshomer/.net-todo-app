using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IToDosService
    {
        public Task<IEnumerable<Todo>> GetAll(int userId);

        public Task<Todo> GetOne(int userId, int todoId);

        public Task<Todo> Create(CreatedTodoDTO todo);

        public Task<Todo> ChangeStatus(ChangeTodoStatusDTO changeTodoStatus);
    }
}
