/**
 * Setting - factory
 * @param {object} $http to make ajax call.
 * @return {object} Setting wrapper.
 */
function SettingFactory($http) {
    var Setting = function () {
        this.settings = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    Setting.prototype.GetAllSettings = function () {
        var _self = this;
        var url = apiBaseUrl + 'setting';

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
            var settings = data;

            if (settings.length !== 0) {
                settings.forEach(function (item) {
                    console.log(item);
                    _self.settings.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    Setting.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "setting/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    Setting.prototype.Add = function (setting, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "setting/add",
            data: setting
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Setting.prototype.Update = function (setting, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "setting/update",
            data: setting
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Setting.prototype.Delete = function (setting, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "setting/delete",
            data: setting
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return Setting;
}