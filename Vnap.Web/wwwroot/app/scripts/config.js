vnap
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        '$ocLazyLoadProvider',
        'IdleProvider',
        function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, IdleProvider) {

            IdleProvider.idle(5); // in seconds
            IdleProvider.timeout(120); // in seconds

            $urlRouterProvider.otherwise("/index/main");
            $ocLazyLoadProvider.config({
                // Set to true if you want to see what and when is dynamically loaded
                debug: false
            });

            $stateProvider
                .state('index', {
                    abstract: true,
                    url: "/index",
                    templateUrl: appBaseUrl + "views/common/content.html"
                })
                .state('index.plant', {
                    url: "/plant",
                    templateUrl: appBaseUrl + "views/plant/list.html",
                    data: { pageTitle: 'Danh sách cây tr?ng' }
                })
                .state('index.plant-add', {
                    url: "/plant-add",
                    templateUrl: appBaseUrl + "views/plant/add.html",
                    data: { pageTitle: 'Thêm cây tr?ng' },
                    resolve: {
                        loadPlugin: function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                {
                                    files: [appBaseUrl + "bower_components/ng-file-upload/ng-file-upload-all.min.js"]
                                }
                            ]);
                        }
                    }
                })
                .state('index.article', {
                    url: "/article",
                    templateUrl: appBaseUrl + "views/article.html",
                    data: { pageTitle: 'Article page' }
                })
                .state('index.main', {
                    url: "/main",
                    templateUrl: appBaseUrl + "views/main.html",
                    data: { pageTitle: 'Gi?i thi?u' }
                })
                .state('index.minor', {
                    url: "/minor",
                    templateUrl: appBaseUrl + "views/minor.html",
                    data: { pageTitle: 'Example view' }
                });
        }]);

vnap
    .config([
        'cloudinaryProvider',
        function (cloudinaryProvider) {
            cloudinaryProvider
                .set("cloud_name", "vnap")
                .set("upload_preset", "rkeobjyv")
                .set("api_key", "779243627828354")
                .set("api_secret", "83F-o-2dn-ZubPVpcS57SxsOabI");
        }]);