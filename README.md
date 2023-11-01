# TaskManager
đây là 1 web api của một cửa hàng và sẽ đươc cập nhật và chỉnh sửa tiếp tục cho đến khi hoàn thiện,
vì đây là 1 dự án cá nhân nên chỉ có thể cập nhật và chỉnh sửa trong những lúc có thời gian
về phần database: 
- csdl bao gồm các thực thể cơ bản như kho chứa, sản phẩm, loại, thương hiệu, hình ảnh, và giỏ hàng
- được xây dựng và thiết lập thông qua entiry framework và được triển khai trên sql

về phần api:
- tập trung vào việc trải nghiệm các câu lệnh và làm quen các cú pháp của lập trình api
- sử dụng framework 7.0 và asp.net web api để xây dựng chương trình

về xác thực và ủy quyền
- sử dụng identity framework để tạo hệ dữ liệu người dùng
- tập trung vào việc trải nghiệm các câu lệnh và làm quen khi làm việc với identity framework
- sử dụng thư viện authorzation để ủy quyền truy cập
  + khách hàng: có thể truy cập, xem và tìm kiếm trong thương hiệu, loại, sản phẩm và hình ảnh, riêng phần giỏ hàng và vật phẩm trong giỏ hàng người dùng sẽ được cấp toàn quyền truy cập
  + quản lý : sẽ được cấp toàn quyền nhưng sẽ chỉ có thể truy cập vào giỏ hàng và vật phẩm trong giỏ hàng
