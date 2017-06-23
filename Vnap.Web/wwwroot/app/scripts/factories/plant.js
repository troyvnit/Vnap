/**
 * Plant - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function PlantFactory($http) {
    var Plant = function () {
        this.plants = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    Plant.prototype.GetAllPlants = function () {
        var _self = this;
        var url = apiBaseUrl + 'plant';

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
            var plants = data;

            if (plants.length !== 0) {
                plants.forEach(function (item) {
                    console.log(item);
                    _self.plants.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    Plant.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "plant/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    Plant.prototype.Add = function (plant, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plant/add",
            data: plant
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Plant.prototype.Update = function (plant, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plant/update",
            data: plant
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Plant.prototype.Delete = function (plant, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plant/delete",
            data: plant
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return Plant;
}