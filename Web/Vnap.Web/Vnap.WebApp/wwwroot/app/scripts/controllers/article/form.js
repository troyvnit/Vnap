/**
 * ArticleFormCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Article TBD.
 */
function ArticleFormCtrl($scope, $rootScope, $stateParams, $state, Article, Upload, cloudinary, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Article = new Article();
    $scope.article = { Id: 0 };
    
    if ($stateParams.Id && $stateParams.Id > 0) {
        $scope.Article.Get($stateParams.Id, function (data) {
            $scope.$apply(function() {
                $scope.article.Id = data.Id;
                $scope.article.Title = data.Title;
                $scope.article.Description = data.Description;
                $scope.article.Content = data.Content;
                $scope.article.Priority = data.Priority;
                $scope.article.Avatar = data.Avatar;
                $scope.article.ArticleType = data.ArticleType;
            });
        });
    }

    $scope.fileProgress = 0;
    $scope.uploadFiles = function(files) {
        $scope.files = files;

        if (!$scope.files || $scope.files.length > 2) {
            return;
        }

        angular.forEach(files,
            function(file) {
                if (file && !file.$error) {
                    $scope.fileProgress = 0.1;
                    file.upload = Upload.upload({
                            url: "https://api.cloudinary.com/v1_1/vnap/upload",
                            data: {
                                upload_preset: cloudinary.config().upload_preset,
                                api_key: cloudinary.config().api_key,
                                api_secret: cloudinary.config().api_secret,
                                tags: "article-avatar",
                                context: "photo=" + $scope.title,
                                file: file
                            }
                        })
                        .progress(function(e) {
                            var progressValue = e.loaded * 100.0 / e.total;

                            $scope.fileProgress = Math.round(progressValue);
                            file.status = "Uploading... " + $scope.fileProgress + "%";
                        })
                        .success(function(data, status, headers, config) {

                            $scope.article.Avatar = data.url;
                            $scope.fileProgress = 0;
                        })
                        .error(function(data, status, headers, config) {
                            file.result = data;
                            $scope.fileProgress = 0;
                        });
                }
            });
    };

    $scope.articleTypes = [{ Name: 'Giới thiệu', Id: 0 }, { Name: 'Thông báo', Id: 1 }, { Name: 'Tin tức', Id: 2 }, { Name: 'Hướng dẫn', Id: 3 }];

    $scope.save = function () {
        if ($scope.article.Id > 0) {
            $scope.Article.Update($scope.article, function() {
                $state.go("index.article");
            });
        } else {
            $scope.Article.Add($scope.article, function () {
                $state.go("index.article");
            });
        }
    };
}