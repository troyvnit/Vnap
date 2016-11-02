// Declare your factories
angular.module('vnap')
    .factory('Plant', PlantFactory)
    .factory('PlantDisease', PlantDiseaseFactory)
    .factory('Solution', SolutionFactory)
    .factory('Article', ArticleFactory);