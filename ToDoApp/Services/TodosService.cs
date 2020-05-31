using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TodosService : IToDosService
    {
        private AppDBContext _context;
        private Mapper _mapper;

        public TodosService(AppDBContext context, Mapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Todo> ChangeStatus(ChangeTodoStatusDTO changeTodoStatus)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == changeTodoStatus.Id);
            if (todo == null)
            {
                throw new Exception("Todo not found");
            }

            todo.Status = changeTodoStatus.Status;
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> Create(CreatedTodoDTO todo)
        {
            var t = _mapper.Map<Todo>(todo);
            _context.Todos.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<IEnumerable<Todo>> GetAll(int userId)
        {
            return await (from t in _context.Todos where t.UserId == userId select t).ToListAsync();
        }

        public async Task<Todo> GetOne(int userId, int todoId)
        {
            return await (from t in _context.Todos where t.UserId == userId && t.Id == todoId select t).FirstOrDefaultAsync();
        }

    }
}
