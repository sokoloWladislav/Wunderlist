
app.controller('todoListController', function ($scope, todoListService) {

    loadRecords();

    function loadRecords() {
        var promiseGet = todoListService.getTodoLists();
        promiseGet.then(function (pl) { $scope.TodoLists = pl.data });
    }

    $scope.save = function (todoList, creationForm) {
        if (creationForm.$valid) {
            var promisePost = todoListService.post(todoList);
            promisePost.then(function (pl) {
                loadRecords();
            });
            cancel();
        }
    };
    $scope.createForm = function() {
        var globalDark = angular.element(document.querySelector("#overlay"));
        globalDark.css("display", "block");
        var creationForm = angular.element(document.querySelector("#createForm"));
        creationForm.css("display", "block");
    }

    $scope.cancel = function() {
        var globalDark = angular.element(document.querySelector("#overlay"));
        globalDark.css("display", "none");
        var creationForm = angular.element(document.querySelector("#createForm"));
        creationForm.css("display", "none");
    }
});