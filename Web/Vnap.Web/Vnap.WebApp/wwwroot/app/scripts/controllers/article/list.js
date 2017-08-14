/**
 * ArticleCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Article TBD.
 */
function ArticleCtrl($scope, $rootScope, $uibModal, Article, $state, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && this.authentication.isAdmin) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.Article = new Article();
    $scope.Article.GetAllArticles();

    $scope.confirmDelete = function (article) {
        $scope.deletedArticle = article;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'wwwroot/app/views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.Article.Delete($scope.deletedArticle, function () {
            $scope.$apply(function () {
                var index = $scope.Article.articles.indexOf($scope.deletedArticle);
                $scope.Article.articles.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };

    $scope.articleTypes = [{ Name: 'Giới thiệu', Id: 0 }, { Name: 'Thông báo', Id: 1 }, { Name: 'Tin tức', Id: 2 }, { Name: 'Hướng dẫn', Id: 3 }, { Name: 'Điều khoản', Id: 4 }];
}