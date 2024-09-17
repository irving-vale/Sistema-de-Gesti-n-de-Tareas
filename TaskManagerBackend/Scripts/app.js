var app = angular.module('taskManagerApp', []);


app.controller('TaskController', function ($scope, $http) {
    $scope.tareas = [];
    $scope.task = {};
    $scope.editMode = false;

    // Obtener todas las tareas
    $scope.loadTasks = function () {
        $http.get('https://localhost:44374/api/Tareas').then(function (response) {
          
            $scope.tareas = response.data;
        }, function (error) {
            console.error('Error al cargar las tareas', error);
        });
    };

    // Crear o actualizar una tarea
    $scope.submitTask = function () {
        
        if (!$scope.editMode) {
            
            $scope.task.FechaCreacion = new Date().toISOString(); 
        }

        var url = $scope.editMode ?
            'https://localhost:44374/api/Tareas/' + $scope.task.Id :
            'https://localhost:44374/api/Tareas';

        var method = $scope.editMode ? 'PUT' : 'POST';

        $http({
            method: method,
            url: url,
            data: $scope.task
        }).then(function (response) {
            
            $scope.loadTasks();
            $scope.task = {};
            $scope.editMode = false;
        }).catch(function (error) {
            
        });
    };

    // Editar una tarea
    $scope.editTask = function (tarea) {
        
        tarea.FechaCreacion = new Date(tarea.FechaCreacion).toISOString(); 

        $scope.task = angular.copy(tarea);
        $scope.editMode = true;
    };

    // Eliminar una tarea
    $scope.deleteTask = function (id) {
        $http.delete('https://localhost:44374/api/Tareas/' + id).then(function (response) {
            $scope.loadTasks();
        });
    };

    // Cargar las tareas al iniciar
    $scope.loadTasks();
});
