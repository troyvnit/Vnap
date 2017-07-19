/**
 * Solution - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function SolutionFactory($http) {
    var Solution = function () {
        this.solutions = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    Solution.prototype.GetAllSolutions = function () {
        var _self = this;
        var url = apiBaseUrl + 'solution';

        if (_self.busy) {
            return;
        }

        _self.busy = true;

        return $http.get(url, {
            params: {
                skip: _self.skip,
                take: _self.take
            }
        }).success(function (data) {
            var solutions = data;

            if (solutions.length !== 0) {
                solutions.forEach(function (item) {
                    console.log(item);
                    _self.solutions.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    Solution.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "solution/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    Solution.prototype.Add = function (solution, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "solution/add",
            data: solution
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Solution.prototype.Update = function (solution, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "solution/update",
            data: solution
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Solution.prototype.Delete = function (solution, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "solution/delete",
            data: solution
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return Solution;
}