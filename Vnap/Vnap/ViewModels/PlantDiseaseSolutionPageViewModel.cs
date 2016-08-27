using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantDiseaseSolutionPageViewModel : BaseViewModel
    {

        ImageSource _avatar = null;
        public ImageSource Avatar
        {
            get { return _avatar; }
            set
            {
                SetProperty(ref _avatar, value);
            }
        }

        string _companyName = null;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                SetProperty(ref _companyName, value);
            }
        }

        private WebViewSource _description = null;
        public WebViewSource Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        public void LoadSolutionDetail()
        {
            Avatar = "solution_avatar.jpg";
            CompanyName = "CTY TNHH VNAP";
            Description = new HtmlWebViewSource()
            {
                Html = @"<strong>Ưu điểm</strong><br/>Thuốc bảo vệ thực vật là các loại hóa chất do con người sản xuất ra để diệt trừ các loại sâu bệnh hại cho cây trồng. Nó được phân làm hại loại chủ yếu là thuốc trừ sâu và thuốc trừ cỏ. Loại thuốc này có ưu điểm là diệt sâu bệnh hại, các loại cỏ dại mà sử dụng đơn giản nên được người dân rất ưa chuộng.<br/><br/>
                         <strong>Khuyết điểm</strong><br/>Ngoài ưu điểm đó ra, thuốc bảo vệ thực vật còn có rất nhiều tác hại cho người sử dụng như: Trong tự nhiên có rất nhiều loại sâu bệnh khác nhau và chúng sống ký sinh dưới nhiều dạng khác nhau như: dưới lá, trong thân, có loại thì chui vào đất cho nên phải sử dụng nhiều loại thuốc khác nhau điều đó gây khó dễ cho người dân có trình độ văn hóa thấp không biết mua thuốc khác nhau, đôi khi có người phun nhiều chỉ để chắc chắn trừ được sâu bệnh. Thuốc bảo vệ thực vật ngoài các tính năng diệt các loại sâu bệnh ra, nó còn có thể diệt các loại côn trùng có lợi cho cây trồng. Đối với loại thuốc có khả năng bay hơi thì nó có thể gây hại cho người phun cũng như người đi đường. Sử dụng nhiều loại thuốc cùng một thời gian có thể gây nhờn thuốc, do vậy mỗi loại thuốc bảo vệ thực vật chỉ được sử dụng một thời gian nhất định. Nói một cách tổng quát, thuốc bảo vệ thực vật không chỉ có tác động tích cực bảo vệ mùa màng cho dân làng mà còn gây nhiều ảnh hưởng nghiêm trọng tới môi trường, hệ sinh thái và con người. do vậy cần phải dùng thuốc đúng cách, đúng liều lượng, đúng kỹ thuật và mua thuốc ở địa điểm đáng tin cây như: chợ nông nghiệp,…. "
            };
        }
    }
}
