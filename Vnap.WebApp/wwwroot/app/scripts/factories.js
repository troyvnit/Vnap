// Declare your factories
angular.module('vnap')
    .factory('User', UserFactory)
    .factory('Plant', PlantFactory)
    .factory('PlantDisease', PlantDiseaseFactory)
    .factory('Solution', SolutionFactory)
    .factory('Article', ArticleFactory);