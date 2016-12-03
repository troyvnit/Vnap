vnap
    .filter('stringRes', function () {

        return function (key) {
            var strings = {
                'article': 'Bài viết',
                'articleList': 'Danh sách bài viết',
                'articleListHeader': 'Quản lý bài viết',
                'articleListSubheader': 'Danh sách bài viết',

                'articleAdd': 'Thêm bài viết',
                'articleAddHeader': 'Thêm bài viết',
                'articleDetail': 'Chi tiết bài viết',
                'articleTitlePlaceHolder': 'Tiêu đề bài viết',
                'articleDescriptionPlaceHolder': 'Mô tả bài viết',
                'articleContentPlaceHolder': 'Nội dung bài viết',

                'user': 'Người dùng',
                'userList': 'Danh sách người dùng',
                'userListHeader': 'Quản lý người dùng',
                'userListSubheader': 'Danh sách người dùng',

                'plant': 'Cây trồng',
                'plantList': 'Danh sách cây trồng',
                'plantListHeader': 'Quản lý cây trồng',
                'plantListSubheader': 'Danh sách cây trồng',

                'plantAdd': 'Thêm cây trồng',
                'plantAddHeader': 'Thêm cây trồng',
                'plantDetail': 'Chi tiết cây trồng',
                'plantNamePlaceHolder': 'Tên cây trồng',
                'plantDescriptionPlaceHolder': 'Mô tả cây trồng',

                'plantDisease': 'Bệnh',
                'plantDiseaseList': 'Danh sách bệnh',
                'plantDiseaseListHeader': 'Quản lý bệnh',
                'plantDiseaseListSubheader': 'Danh sách bệnh',

                'plantDiseaseAdd': 'Thêm bệnh',
                'plantDiseaseAddHeader': 'Thêm bệnh',
                'plantDiseaseDetail': 'Chi tiết bệnh',
                'plantDiseaseNamePlaceHolder': 'Tên bệnh',
                'plantDiseaseDescriptionPlaceHolder': 'Mô tả bệnh',

                'solution': 'Giải pháp',
                'solutionList': 'Danh sách giải pháp',
                'solutionListHeader': 'Quản lý giải pháp',
                'solutionListSubheader': 'Danh sách giải pháp',

                'solutionAdd': 'Thêm giải pháp',
                'solutionAddHeader': 'Thêm giải pháp',
                'solutionDetail': 'Chi tiết giải pháp',
                'solutionNamePlaceHolder': 'Tên giải pháp',
                'solutionCompanyNamePlaceHolder': 'Tên công ty',
                'solutionDescriptionPlaceHolder': 'Mô tả giải pháp',
                'solutionPrimePlaceHolder': 'Giải pháp khuyến nghị',

                'priorityPlaceHolder': 'Ưu tiên số từ nhỏ đến lơn',

                'name': 'Tên',
                'companyName': 'Tên công ty',
                'description': 'Mô tả',
                'priority': 'Thứ tự ưu tiên',
                'prime': 'Khuyến nghị',
                'add': 'Tạo mới',
                'list': 'Danh sách',
                'cancel': 'Hủy',
                'save': 'Lưu',
                'edit': 'Sửa',
                'delete': 'Xóa',
                'avatar': 'Hình đại diện',
                'type': 'Loại',
                'image': 'Hình ảnh',
                'imageAdd': 'Thêm hình ảnh',
                'imageListSubheader': 'Danh sách hình ảnh',
                'title': 'Tiêu đề',

                'homeLinkText': 'Dashboard'
            };
            return strings[key] || key;
        };

    })
    .filter('log', function () {
        return function (data) {
            console.log(data);
        };
    });