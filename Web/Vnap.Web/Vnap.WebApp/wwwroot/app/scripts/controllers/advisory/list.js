/**
 * AdvisoryMessageCtrl - controller
 * @param {object} $scope TBD.
 * @param {object} $rootScope TBD.
 * @param {object} AdvisoryMessage TBD.
 */
function AdvisoryMessageCtrl($scope, $rootScope, $uibModal, AdvisoryMessage, Upload, cloudinary, $state, authService) {
    this.authentication = authService.authentication;

    if (this.authentication.isAuth && (this.authentication.isAdmin || this.authentication.isMod)) {
        //Welcome Message
    } else {
        $state.go('login');
    }

    $scope.AdvisoryMessage = new AdvisoryMessage($scope);
    $scope.AdvisoryMessage.GetConversations(function (data) {
        $scope.loadAdvisoryMessages(data[0].Name);
    });

    $scope.confirmDelete = function (advisoryMessage) {
        $scope.deletedAdvisoryMessage = advisoryMessage;
        $scope.modalInstance = $uibModal.open({
            templateUrl: 'wwwroot/app/views/modals/delete-confirm.html',
            scope: $scope
        });
    }

    $scope.ok = function () {
        $scope.AdvisoryMessage.Delete($scope.deletedAdvisoryMessage, function () {
            $scope.$apply(function () {
                var index = $scope.AdvisoryMessage.advisoryMessages.indexOf($scope.deletedAdvisoryMessage);
                $scope.AdvisoryMessage.advisoryMessages.splice(index, 1);
            });
        });
        $scope.modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.modalInstance.dismiss('cancel');
    };

    $scope.loadAdvisoryMessages = function (conversationName) {
        $scope.currentConversationName = conversationName;
        $scope.AdvisoryMessage.LoadAdvisoryMessages(conversationName, function (data) {
            $scope.$apply(function () {
                $scope.AdvisoryMessages = data;
            });
        });
    }

    $scope.advisoryMessage = {};
    $scope.addAdvisoryMessage = function (imageUrl) {
        $scope.advisoryMessage.IsAdviser = true;
        $scope.advisoryMessage.AuthorName = "Vnap";
        $scope.advisoryMessage.ImageUrl = imageUrl;
        $scope.advisoryMessage.ConversationName = $scope.currentConversationName;
        $scope.AdvisoryMessage.Add($scope.advisoryMessage);

        $scope.advisoryMessage.Content = '';
    }

    $scope.uploadAdvisoryImage = function(file) {
        if (file) {
            $scope.fileProgress = 0.1;
            file.upload = Upload.upload({
                url: "https://api.cloudinary.com/v1_1/vnap/upload",
                data: {
                    upload_preset: cloudinary.config().upload_preset,
                    api_key: cloudinary.config().api_key,
                    api_secret: cloudinary.config().api_secret,
                    tags: "advisory-image",
                    context: "photo=" + $scope.title,
                    file: file
                }
            })
                .progress(function (e) {
                    var progressValue = e.loaded * 100.0 / e.total;

                    $scope.fileProgress = Math.round(progressValue);
                    file.status = "Uploading... " + $scope.fileProgress + "%";
                })
                .success(function (data, status, headers, config) {

                    $scope.addAdvisoryMessage(data.url);
                    $scope.fileProgress = 0;

                })
                .error(function (data, status, headers, config) {
                    file.result = data;
                });
        }
    }
}