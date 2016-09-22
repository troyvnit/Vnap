/**
 * ArticleCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} Article TBD.
 */
function ArticleCtrl($scope, $rootScope, Article) {
    var _self = this;

    _self.Article = new Article();
    _self.Article.GetAllArticles();
}