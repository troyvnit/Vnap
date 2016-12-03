﻿/**
 * Article - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function ArticleFactory($http) {
    var Article = function () {
        this.articles = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    // apiBaseUrl is set in app.js
    Article.prototype.GetAllArticles = function () {
        var _self = this;
        var url = apiBaseUrl + 'article';

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
            var articles = data;

            if (articles.length !== 0) {
                articles.forEach(function (item) {
                    console.log(item);
                    _self.articles.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    Article.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "article/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    Article.prototype.Add = function (article, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "article/add",
            data: article
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Article.prototype.Update = function (article, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "article/update",
            data: article
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    Article.prototype.Delete = function (article, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "article/delete",
            data: article
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    return Article;
}