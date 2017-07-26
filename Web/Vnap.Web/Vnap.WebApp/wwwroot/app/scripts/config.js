vnap
    .config([
        '$stateProvider',
        '$urlRouterProvider',
        '$locationProvider',
        '$ocLazyLoadProvider',
        'IdleProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $ocLazyLoadProvider, IdleProvider) {

            IdleProvider.idle(5); // in seconds
            IdleProvider.timeout(120); // in seconds

            $locationProvider.html5Mode(true);
            $urlRouterProvider.otherwise("/user");
            $ocLazyLoadProvider.config({
                // Set to true if you want to see what and when is dynamically loaded
                debug: false
            });

            $stateProvider
                .state('login', {
                    url: "/login",
                    templateUrl: appBaseUrl + "views/account/login.html",
                    data: { pageTitle: 'Đăng nhập' }
                })
                .state('index', {
                    abstract: true,
                    url: "",
                    templateUrl: appBaseUrl + "views/common/content.html"
                })
                .state('index.user', {
                    url: "/user",
                    templateUrl: appBaseUrl + "views/user/list.html",
                    data: { pageTitle: 'Danh sách người dùng' },
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
                .state('index.plant', {
                    url: "/plant",
                    templateUrl: appBaseUrl + "views/plant/list.html",
                    data: { pageTitle: 'Danh sách cây trồng' },
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
                .state('index.plant-form', {
                    url: "/plant-form/:Id",
                    templateUrl: appBaseUrl + "views/plant/form.html",
                    data: { pageTitle: 'Thêm cây trồng' },
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
                .state('index.plant-disease', {
                    url: "/plant-disease",
                    templateUrl: appBaseUrl + "views/plant-disease/list.html",
                    data: { pageTitle: 'Danh sách bệnh' },
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
                .state('index.plant-disease-form', {
                    url: "/plant-disease-form/:Id/:ActiveTabIndex",
                    templateUrl: appBaseUrl + "views/plant-disease/form.html",
                    data: { pageTitle: 'Thêm bệnh' },
                    resolve: {
                        loadPlugin: function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                {
                                    files: [appBaseUrl + "bower_components/ng-file-upload/ng-file-upload-all.min.js"]
                                },
                                {
                                    name: 'summernote',
                                    files: [appBaseUrl + 'bower_components/summernote/dist/summernote.css', appBaseUrl + 'bower_components/summernote/dist/summernote.js', appBaseUrl + 'bower_components/angular-summernote/dist/angular-summernote.min.js']
                                }
                            ]);
                        }
                    }
                })
                .state('index.solution', {
                    url: "/solution",
                    templateUrl: appBaseUrl + "views/solution/list.html",
                    data: { pageTitle: 'Danh sách giải pháp' },
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
                .state('index.solution-form', {
                    url: "/solution-form/:Id/:PlantDiseaseId/:PlantDiseaseName",
                    templateUrl: appBaseUrl + "views/solution/form.html",
                    data: { pageTitle: 'Thêm/Sửa giải pháp' },
                    resolve: {
                        loadPlugin: function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                {
                                    files: [appBaseUrl + "bower_components/ng-file-upload/ng-file-upload-all.min.js"]
                                },
                                {
                                    name: 'summernote',
                                    files: [appBaseUrl + 'bower_components/summernote/dist/summernote.css', appBaseUrl + 'bower_components/summernote/dist/summernote.js', appBaseUrl + 'bower_components/angular-summernote/dist/angular-summernote.min.js']
                                }
                            ]);
                        }
                    }
                })
                .state('index.article', {
                    url: "/article",
                    templateUrl: appBaseUrl + "views/article/list.html",
                    data: { pageTitle: 'Danh sách bài viết' },
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
                .state('index.article-form', {
                    url: "/article-form/:Id",
                    templateUrl: appBaseUrl + "views/article/form.html",
                    data: { pageTitle: 'Thêm/Sửa bài viết' },
                    resolve: {
                        loadPlugin: function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                {
                                    files: [appBaseUrl + "bower_components/ng-file-upload/ng-file-upload-all.min.js"]
                                },
                                {
                                    name: 'summernote',
                                    files: [appBaseUrl + 'bower_components/summernote/dist/summernote.css', appBaseUrl + 'bower_components/summernote/dist/summernote.js', appBaseUrl + 'bower_components/angular-summernote/dist/angular-summernote.min.js']
                                }
                            ]);
                        }
                    }
                })
                .state('index.main', {
                    url: "/main",
                    templateUrl: appBaseUrl + "views/main.html",
                    data: { pageTitle: 'Gi?i thi?u' }
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