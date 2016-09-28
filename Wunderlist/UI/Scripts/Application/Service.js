app.service('todoListService', function ($http) {


    this.post = function (todoList) {
        var request = $http({
            method: "post",
            url: "/api/TodoList",
            data: todoList
        });
        return request;
    }

    this.get = function (id) {
        return $http.get("/api/TodoList/" + id);
    }

    this.getTodoLists = function () {
        return $http.get("/api/TodoList");
    }


    //Update todolist
    this.put = function (id, todoList) {
        var request = $http({
            method: "put",
            url: "/api/TodoList/" + id,
            data: todoList
        });
        return request;
    }
    //Delete todolist
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/TodoList/" + id
        });
        return request;
    }

});