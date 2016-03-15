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
    public class TodoItemController : ApiController
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodoItemController(ITodoListService todoListService, ITodoItemService todoItemService)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
            var config=new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemDTO, TodoItemViewModel>();
                cfg.CreateMap<TodoItemViewModel, TodoItemDTO>();
            });
            _mapper = config.CreateMapper();
            
        }

        [HttpGet]
        [ActionName("gettodoitem")]
        public TodoItemViewModel GetTodoItem(int id)
        {
            var item = _mapper.Map<TodoItemDTO, TodoItemViewModel>(_todoItemService.GetTodoItemById(id));
            return item;
        }

        [ActionName("getall")]
        [HttpGet]
        [Route("api/todoitem/getall/")]
        public IEnumerable<TodoItemViewModel> Get(int id,bool? completed)
        {
            var list=_todoListService.GetTodoListById(id);
            bool iscompleted;
            iscompleted = completed.HasValue && completed.Value;
            if (list == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            if (list.ApplicationUserEntityId!=User.Identity.GetUserId())
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            return
                _todoItemService.GetAllTodoItems()
                    .Where(x => x.TodoListEntityId == id).Where(x=>x.IsCompleted==iscompleted)
                    .Select(x => _mapper.Map<TodoItemDTO, TodoItemViewModel>(x));
        }

        

        [HttpPost]
        public void CreateTodoItem([FromBody] TodoItemViewModel todoItem)
        {
            var list = _todoListService.GetTodoListById(todoItem.TodoListEntityId);
            if (list == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            if (list.ApplicationUserEntityId != User.Identity.GetUserId())
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            _todoItemService.CreateTodoItem(_mapper.Map<TodoItemViewModel, TodoItemDTO>(todoItem));
        }

        public void Put([FromBody] TodoItemViewModel todoItem)
        {
            if(!User.Identity.IsAuthenticated)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Доступ разрешен только для авторизованным пользователям"));
            if (todoItem==null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Возникла ошибка при обработке запроса"));
            var item = _todoItemService.GetTodoItemById(todoItem.Id);
            if (item == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Не удалось найти указанный элемент"));
            if(User.Identity.GetUserId()!=_todoListService.GetTodoListById(item.TodoListEntityId).ApplicationUserEntityId)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden,"Доступ к ресурсу запрещен"));
            item.Name = todoItem.Name;
            item.DueDate = todoItem.DueDate;
            item.ChangedFromCompletedToIncompleted = (item.IsCompleted ^ todoItem.IsCompleted) &&
                                                         !item.IsCompleted;
            item.IsCompleted = todoItem.IsCompleted;
            item.Note = todoItem.Note;
            if (todoItem.InsertToPosition > -1)
                item.IncertToPlace = todoItem.InsertToPosition;
            _todoItemService.UpdateTodoItem(item);
        }

        public void Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Доступ разрешен только для авторизованным пользователям"));
            var item = _todoItemService.GetTodoItemById(id);
            if (item == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Не удалось найти указанный элемент"));
            if (User.Identity.GetUserId() != _todoListService.GetTodoListById(item.TodoListEntityId).ApplicationUserEntityId)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Доступ к ресурсу запрещен"));
            _todoItemService.DeleteTodoItem(item);
        }
    }
}