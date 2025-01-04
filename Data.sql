-- Tạo cơ sở dữ liệu
CREATE DATABASE AquaFeedManagement;
GO

USE AquaFeedManagement;
GO

-- Thiết lập collation hỗ trợ tiếng Việt
ALTER DATABASE AquaFeedManagement COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

-- Bảng ROLE (Vai trò người dùng)
CREATE TABLE ROLE (
    role_id INT IDENTITY(1,1) PRIMARY KEY,
    role_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(255)
);
GO

-- Bảng USERS (Người dùng)
CREATE TABLE USERS (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20),
    avatar NVARCHAR(MAX),
    address NVARCHAR(200),
    role_id INT NOT NULL,
    is_deleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (role_id) REFERENCES ROLE(role_id)
);
GO

-- Bảng CATEGORIE (Danh mục sản phẩm)
CREATE TABLE CATEGORIE (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    category_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(255)
);
GO

-- Bảng SUPPLIER (Nhà cung cấp)
CREATE TABLE SUPPLIER (
    supplier_id INT IDENTITY(1,1) PRIMARY KEY,
    supplier_name NVARCHAR(100) NOT NULL,
    contact NVARCHAR(50),
    address NVARCHAR(200)
);
GO

-- Bảng PRODUCT (Sản phẩm)
CREATE TABLE PRODUCT (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    product_name NVARCHAR(100) NOT NULL,
    category_id INT,
    supplier_id INT,
    price DECIMAL(18, 2) NOT NULL,
    stock INT NOT NULL,
    unit NVARCHAR(50),
    description NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (category_id) REFERENCES CATEGORIE(category_id),
    FOREIGN KEY (supplier_id) REFERENCES SUPPLIER(supplier_id)
);
GO

-- Bảng ORDERS (Đơn hàng)
CREATE TABLE ORDERS (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    total_amount DECIMAL(18, 2) NOT NULL,
    status NVARCHAR(50) DEFAULT 'Pending',
    FOREIGN KEY (user_id) REFERENCES USERS(user_id)
);
GO

-- Bảng ORDER_DETAIL (Chi tiết đơn hàng)
CREATE TABLE ORDER_DETAIL (
    order_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(18, 2) NOT NULL,
    total AS (quantity * price),
    FOREIGN KEY (order_id) REFERENCES ORDERS(order_id),
    FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id)
);
GO

-- Bảng CART (Giỏ hàng)
CREATE TABLE CART (
    cart_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(18, 2) NOT NULL,
    total AS (quantity * price),
    FOREIGN KEY (user_id) REFERENCES USERS(user_id),
    FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id)
);
GO

-- Bảng CHAT_MESSAGE (Tin nhắn)
CREATE TABLE CHAT_MESSAGE (
    message_id INT IDENTITY(1,1) PRIMARY KEY,
    sender_id INT NOT NULL,
    receiver_id INT NOT NULL,
    message_content NVARCHAR(4000) NOT NULL,
    send_datetime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (sender_id) REFERENCES USERS(user_id),
    FOREIGN KEY (receiver_id) REFERENCES USERS(user_id)
);
GO

-- Bảng FAVORITE_PRODUCT (Sản phẩm yêu thích)
CREATE TABLE FAVORITE_PRODUCT (
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    PRIMARY KEY (user_id, product_id),
    FOREIGN KEY (user_id) REFERENCES USERS(user_id),
    FOREIGN KEY (product_id) REFERENCES PRODUCT(product_id)
);
GO

-- Bảng NOTIFICATION (Thông báo)
CREATE TABLE NOTIFICATION (
    notification_id INT IDENTITY(1,1) PRIMARY KEY,
    sender_id INT NOT NULL,
    receiver_id INT NOT NULL,
    message NVARCHAR(250) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    status NVARCHAR(20) CHECK (status IN ('Sent', 'Read')) NOT NULL,
    FOREIGN KEY (sender_id) REFERENCES USERS(user_id),
    FOREIGN KEY (receiver_id) REFERENCES USERS(user_id)
);
GO

-- Thêm dữ liệu vào bảng ROLE
INSERT INTO ROLE (role_name, description) VALUES
('customer', N'Khách hàng mua sản phẩm'),
('admin', N'Quản trị viên hệ thống'),
('manager', N'Quản lý cửa hàng');
GO

-- Thêm dữ liệu vào bảng CATEGORIE
INSERT INTO CATEGORIE (category_name, description) VALUES
(N'Thức ăn cá', N'Thức ăn dành cho các loại cá'),
(N'Thức ăn tôm', N'Thức ăn dành cho tôm'),
(N'Thức ăn hỗn hợp', N'Thức ăn tổng hợp cho nhiều loại thủy sản');
GO

-- Thêm dữ liệu vào bảng SUPPLIER
INSERT INTO SUPPLIER (supplier_name, contact, address) VALUES
('Nhà cung cấp A', N'0912345678', N'123 Đường Biển, TP. Đà Nẵng'),
('Nhà cung cấp B', N'0987654321', N'456 Đường Hồ, TP. Nha Trang');
GO

-- Thêm dữ liệu vào bảng USERS
INSERT INTO USERS (full_name, email, password, phone, avatar, address, role_id) VALUES
(N'Nguyễn Văn A', 'a@example.com', 'password123', '0901234567', 'avatar1.jpg', N'123 Đường A, TP. Hồ Chí Minh', 1),
(N'Trần Thị B', 'b@example.com', 'password123', '0902345678', 'avatar2.jpg', N'456 Đường B, TP. Đà Nẵng', 2),
(N'Lê Văn C', 'c@example.com', 'password123', '0903456789', 'avatar3.jpg', N'789 Đường C, TP. Nha Trang', 3);
GO

-- Thêm dữ liệu vào bảng PRODUCT
INSERT INTO PRODUCT (product_name, category_id, supplier_id, price, stock, unit, description) VALUES
('Thức ăn cá basa', 1, 1, 50000, 100, N'Kg', N'Thức ăn giàu dinh dưỡng cho cá basa'),
('Thức ăn tôm sú', 2, 2, 60000, 200, N'Kg', N'Thức ăn chuyên dụng cho tôm sú'),
('Thức ăn hỗn hợp', 3, 1, 45000, 150, N'Kg', N'Thức ăn tổng hợp cho thủy sản');
GO

-- Thêm dữ liệu vào bảng ORDERS
INSERT INTO ORDERS (user_id, total_amount, status) VALUES
(1, 100000, 'Pending'),
(2, 150000, 'Completed'),
(3, 200000, 'Shipped');
GO

-- Thêm dữ liệu vào bảng ORDER_DETAIL
INSERT INTO ORDER_DETAIL (order_id, product_id, quantity, price) VALUES
(1, 1, 3, 50000),
(1, 2, 2, 60000),
(2, 3, 1, 45000),
(3, 1, 2, 50000);
GO
