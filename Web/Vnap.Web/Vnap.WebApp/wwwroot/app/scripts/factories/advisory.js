/**
 * AdvisoryMessage - factory
 * @param {object} $http to make ajax call.
 * @return {object} Article wrapper.
 */
function AdvisoryMessageFactory($http, Hub) {
    var _scope;
    var AdvisoryMessage = function (scope) {
        _scope = scope;
        this.conversations = [];
        this.busy = false;
        this.skip = 0;
        this.take = 10;
    };

    //declaring the hub connection
    var hub = new Hub('notificationHub', {

        //client side methods
        listeners: {
            'publishAdvisoryMessage': function (message) {
                var conversation = _scope.AdvisoryMessage.conversations.find(x => x.Name == message.ConversationName);
                if (!conversation) {
                    conversation = { Name: message.ConversationName, LatestMessage: message };
                    _scope.AdvisoryMessage.conversations.unshift(conversation);
                } else {
                    conversation.LatestMessage = message;
                }
                if (message.ConversationName == _scope.currentConversationName) {
                    _scope.AdvisoryMessages.push(message);
                }

                _scope.notify(message);
                _scope.openChat = true;
                _scope.$apply();
            }
        },

        //server side methods
        methods: ['subscribeAdvisoryMessage'],

        //query params sent on initial connection
        queryParams: {
            'token': ''
        },

        //handle connection error
        errorHandler: function (error) {
            console.error(error);
        },

        //specify a non default root
        //rootPath: '/api

        stateChanged: function (state) {
            switch (state.newState) {
                case $.signalR.connectionState.connecting:
                    //your code here
                    break;
                case $.signalR.connectionState.connected:
                    //your code here
                    break;
                case $.signalR.connectionState.reconnecting:
                    //your code here
                    break;
                case $.signalR.connectionState.disconnected:
                    //your code here
                    break;
            }
        }
    });

    // apiBaseUrl is set in app.js
    AdvisoryMessage.prototype.GetConversations = function (successCallback) {
        var _self = this;
        var url = apiBaseUrl + 'advisoryMessage/getConversations';

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
            var conversations = data;

            if (conversations.length !== 0) {
                conversations.forEach(function (item) {
                    console.log(item);
                    _self.conversations.push(item);
                });

                _self.busy = false;
                _self.skip += _self.take;
                successCallback(conversations);
            }
        }).error(function (data, status, headers, config) {
            _self.busy = false;
        });
    };

    AdvisoryMessage.prototype.Get = function (id, successCallback) {
        $.ajax({
            method: "Get",
            url: apiBaseUrl + "advisoryMessage/get",
            data: {'id': id}
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    }

    AdvisoryMessage.prototype.Add = function (advisoryMessage) {
        hub.subscribeAdvisoryMessage(advisoryMessage).done((message) => {
            _scope.AdvisoryMessages.push(message);
            var conversation = _scope.AdvisoryMessage.conversations.find(x => x.Name == message.ConversationName);
            if (conversation) {
                conversation.LatestMessage = message;
            }
            _scope.$apply();
        });
        //$.ajax({
        //    method: "POST",
        //    url: apiBaseUrl + "advisoryMessage/add",
        //    data: advisoryMessage
        //})
        //.success(function (data) {
        //    if (successCallback) {
        //        successCallback(data);
        //    }
        //});
    };

    AdvisoryMessage.prototype.Update = function (advisoryMessage, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "advisoryMessage/update",
            data: advisoryMessage
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    AdvisoryMessage.prototype.Delete = function (advisoryMessage, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "advisoryMessage/delete",
            data: advisoryMessage
        })
        .success(function (data) {
            if (successCallback) {
                successCallback(data);
            }
        });
    };

    AdvisoryMessage.prototype.LoadAdvisoryMessages = function(conversationName, successCallback) {
        $.ajax({
                method: "Get",
                url: apiBaseUrl + "advisoryMessage",
                data: { 'conversationName': conversationName }
            })
            .success(function(data) {
                if (successCallback) {
                    successCallback(data);
                }
            });
    };

    AdvisoryMessage.prototype.DeleteConversation = function (conversation, successCallback) {
        $.ajax({
            method: "POST",
            url: apiBaseUrl + "advisoryMessage/deleteConversation",
            data: conversation
        })
            .success(function (data) {
                if (successCallback) {
                    successCallback(data);
                }
            });
    };

    return AdvisoryMessage;
}