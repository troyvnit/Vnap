/**
 * User - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function UserFactory($http) {
    var User = function () {
        this.users = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    User.prototype.GetAllUsers = function (successCallback) {
        var _self = this;
        var url = apiBaseUrl + 'user';

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
            var users = data;

            if (users.length !== 0) {
                users.forEach(function (item) {
                    console.log(item);
                    _self.users.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
                successCallback(users);
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    User.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "user/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    User.prototype.Add = function (user, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "user/add",
            data: user
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    User.prototype.Update = function (user, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "user/update",
            data: user
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    User.prototype.Delete = function (user, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "user/delete",
            data: user
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return User;
}