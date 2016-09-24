vnap
    .filter('stringRes', function () {

        return function (key) {
            var strings = {
                'article': 'Article',
                'articlesList': 'Article List',
                'articlesHeader': 'Article Management',
                'articlesSubheader': 'Articles',

                'plant': 'Cây trồng',
                'plantList': 'Danh sách cây trồng',
                'plantListHeader': 'Quản lý cây trồng',
                'plantListSubheader': 'Danh sách cây trồng',

                'plantAdd': 'Thêm cây trồng',
                'plantAddHeader': 'Thêm cây trồng',
                'plantDetail': 'Chi tiết cây trồng',
                'plantNamePlaceHolder': 'Tên cây trồng',
                'plantDescriptionPlaceHolder': 'Mô tả cây trồng',
                'priorityPlaceHolder': 'Ưu tiên số từ nhỏ đến lơn',

                'name': 'Tên',
                'description': 'Mô tả',
                'priority': 'Thứ tự ưu tiên',
                'add': 'Tạo mới',
                'list': 'Danh sách',
                'cancel': 'Hủy',
                'save': 'Lưu',
                'edit': 'Sửa',
                'delete': 'Xóa',
                'avatar': 'Hình đại diện',

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