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

                'plantDisease': 'Bệnh',
                'plantDiseaseList': 'Danh sách bệnh',
                'plantDiseaseListHeader': 'Quản lý bệnh',
                'plantDiseaseListSubheader': 'Danh sách bệnh',

                'plantDiseaseAdd': 'Thêm bệnh',
                'plantDiseaseAddHeader': 'Thêm bệnh',
                'plantDiseaseDetail': 'Chi tiết bệnh',
                'plantDiseaseNamePlaceHolder': 'Tên bệnh',
                'plantDiseaseDescriptionPlaceHolder': 'Mô tả bệnh',

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
                'type': 'Loại',
                'image': 'Hình ảnh',
                'imageAdd': 'Thêm hình ảnh',
                'imageListSubheader': 'Danh sách hình ảnh',

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