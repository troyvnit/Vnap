/**
 * PlantDisease - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function PlantDiseaseFactory($http) {
    var PlantDisease = function () {
        this.plantDiseases = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    PlantDisease.prototype.GetAllPlantDiseases = function () {
        var _self = this;
        var url = apiBaseUrl + 'plantDisease';

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
            var plantDiseases = data;

            if (plantDiseases.length !== 0) {
                plantDiseases.forEach(function (item) {
                    console.log(item);
                    _self.plantDiseases.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    PlantDisease.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "plantDisease/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    PlantDisease.prototype.Add = function (plantDisease, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/add",
            data: plantDisease
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    PlantDisease.prototype.Update = function (plantDisease, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/update",
            data: plantDisease
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    PlantDisease.prototype.Delete = function (plantDisease, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/delete",
            data: plantDisease
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    PlantDisease.prototype.AddImage = function (data, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/addImage",
            data: data
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    PlantDisease.prototype.UpdateImage = function (data, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/updateImage",
            data: data
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    PlantDisease.prototype.DeleteImage = function (data, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "plantDisease/deleteImage",
            data: data
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return PlantDisease;
}