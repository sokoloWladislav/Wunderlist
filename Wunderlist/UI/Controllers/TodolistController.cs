using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using BLL.Interface.DTO;
using BLL.Interface.Interfaces;
using Microsoft.AspNet.Identity;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class TodoListController:ApiController
    {
        private readonly ITodoListService _todoListService;
        private readonly IMapper _mapper;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoListViewModel, TodoListDTO>(); });
            _mapper = config.CreateMapper();
        }

        [ActionName("Get")]
        public IEnumerable<TodoListViewModel> GetTodoLists()
        {
            var enumerable = _todoListService.GetAllTodoLists();
            return enumerable.Where(x=>x.ApplicationUserEntityId==User.Identity.GetUserId()).Select(
                        list =>
                            new TodoListViewModel()
                            {
                                ApplicationUserEntityId = list.ApplicationUserEntityId,
                                Id = list.Id,
                                Name = list.Name
                            });
        }

        
        [HttpGet]
        public TodoListViewModel GetTodoList(int id)
        {
            return _mapper.Map<TodoListDTO, TodoListViewModel>(_todoListService.GetTodoListById(id));
        }

        [HttpPost]
        public void CreateTodoList([FromBody]TodoListViewModel todoList)
        {
            todoList.ApplicationUserEntityId = User.Identity.GetUserId();
            _todoListService.CreateTodoList(_mapper.Map<TodoListViewModel, TodoListDTO>(todoList));
        }

        [HttpPut]
        public void EditTodoList(int id, [FromBody]TodoListViewModel todoList)
        {
            var item = _todoListService.GetTodoListById(id);
            if (item == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Не удалось найти указанный элемент"));
            if (User.Identity.GetUserId() != item.ApplicationUserEntityId)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Доступ к ресурсу запрещен"));
            _todoListService.UpdateTodoList(_mapper.Map<TodoListViewModel, TodoListDTO>(todoList));
        }

        [HttpDelete]
        public void DeleteTodoList(int id)
        {
            if (!User.Identity.IsAuthenticated)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Доступ разрешен только для авторизованным пользователям"));
            var item = _todoListService.GetTodoListById(id);
            if (item == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Не удалось найти указанный элемент"));
            if (User.Identity.GetUserId() !=item.ApplicationUserEntityId)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Доступ к ресурсу запрещен"));
            _todoListService.DeleteTodoList(item);
        }
    }
}