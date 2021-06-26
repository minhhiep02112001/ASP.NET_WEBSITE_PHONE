create database ShopDienThoai
use ShopDienThoai
go

create table category
(
	id bigint not null primary key  Identity(1,1),
    name nvarchar(255) NOT NULL,
    slug varchar(255)  NOT NULL,
	icon varchar(255) ,
    parent_id bigint not null DEFAULT 0,
	position int NOT NULL DEFAULT 0,
	is_active bit default 0,
	create_at datetime  DEFAULT GETDATE(),
	is_remove   bit  DEFAULT 0,
)
go
create table product 
(
	id bigint not null primary key  Identity(1,1),
    name   varchar(255) NOT NULL,
    slug   varchar(255) NOT NULL,
    images   varchar(255)   ,
    price   int NOT NULL DEFAULT 0,
    sale   int NOT NULL DEFAULT 0,
    quantity   int NOT NULL DEFAULT 0,
    is_active   bit DEFAULT 0,
    is_hot   bit  DEFAULT 0,
    category_id   bigint NOT NULL DEFAULT 0,
    url   varchar(255)   ,
    sku   varchar(255)   ,
    color   nvarchar(255) ,
    memory   nvarchar(255) ,
    summary   ntext   ,
    descriptions   ntext   ,
	
    meta_title   nvarchar(255)   ,
    meta_description   ntext   ,
    create_at datetime  DEFAULT GETDATE() ,
	is_remove bit default 0
)

go
create table users
(
	id bigint not null primary key  Identity(1,1),
	name   nvarchar(255)  NOT NULL,
    email   varchar(255)  NOT NULL,
    passwords   varchar(255) ,
    remember_token   varchar(100)  DEFAULT NULL,
    created_at   datetime  DEFAULT GETDATE() ,
    avatar   varchar(255)  DEFAULT NULL,
    is_active   bit  DEFAULT 0,
	is_remove bit default 0
)
go
create table articles
(
	id bigint not null primary key  Identity(1,1),
	title nvarchar(255)  NOT NULL,
	slug varchar(255)  NOT NULL,
	images varchar(255)  ,
	summary ntext  ,
	descriptions ntext  ,
	is_hot bit  DEFAULT 0,
	url varchar(255)  ,
	is_active bit  default 0 ,
	meta_title nvarchar(255)  ,
	meta_description nvarchar(255)  ,
	created_at datetime  DEFAULT GETDATE() ,
	is_remove bit default 0
)
go

create table comment
(
	id bigint not null primary key  Identity(1,1),
	articles_id bigint,
	customer_id bigint,
	name nvarchar(255),
	meta_title nvarchar(255)  , -- Nội dung Bình Luận
	created_at datetime  DEFAULT GETDATE() ,
	is_remove bit default 0
)

create table banner 
(
	id bigint not null primary key  Identity(1,1),
    title nvarchar(255) NOT NULL,
	slug varchar(255) NOT NULL,
	images varchar(255)  ,
	url varchar(255) ,
	targets varchar(255)  ,
	descriptions ntext ,
    position int DEFAULT 0,
	is_active bit,
	create_at datetime  DEFAULT GETDATE() ,
	is_remove bit default 0
  )
go
create table orders
(
	id bigint not null primary key  Identity(1,1),
	customer_id bigint ,
    fullname nvarchar(255) NOT NULL,
	email varchar(255) NOT NULL,
	address_order varchar(255) ,
	phone varchar(255) NOT NULL,
	note ntext ,
	coupon varchar(255) DEFAULT '0',
	total float DEFAULT 0,
	order_Status_id int DEFAULT 0,
	create_at datetime  DEFAULT GETDATE() ,
	is_remove bit default 0
)

go
create table customer 
(
	id bigint not null primary key  Identity(1,1),
	name nvarchar(255)  ,
	email nvarchar(255)  ,
	_address varchar(40),
	pass_word varchar(255),
	is_active bit default 0,
	is_remove bit default 0
)

go
create table order_details
(
	id bigint not null primary key  Identity(1,1),
	name nvarchar(255)  ,
	images varchar(255)  ,
	sku varchar(255)  ,
	order_id bigint NOT NULL,
	product_id bigint NOT NULL,
	price float NOT NULL,
	qty int NOT NULL,
	total float not null default 0,
	is_remove bit default 0 -- trường check giá trị thay đổi
)

create table contact
(
	id bigint primary key identity(1,1),
	name nvarchar(255),
	email nvarchar(255),
	phone nvarchar(255),
	content nvarchar(255),
	create_at datetime  DEFAULT GETDATE() , 
)

---chèn 1 tài khoản quản trị
insert into users(name , email , passwords , is_active , is_remove) values ('admin','admin@gmail.com' , 'e10adc3949ba59abbe56e057f20f883e',1,0) --- pass 123456


--- tính sản phẩm bán chạy
select top(8) * from product where product.id in (select top(8) product_id  from order_details group by product_id , qty order by SUM(qty) desc)


/* cập nhật số lượng khi giao hàng*/
/*	khi người quản trị thanh đổi order_Status_id của bảng orders thành [ giao hàng (1)| thành công (2)] 
	thì order_details update is_remove = true (nghĩa là sản phẩm đã trừ số lượng mua) qua trigger bên dưới
*/
	go
	Create TRIGGER updateQuantityOrderSucess ON  order_details AFTER UPDATE AS 
	BEGIN
		DECLARE @is_check bit;
		DECLARE @qty int;
		DECLARE @product_id bigint;
		select @is_check = deleted.is_remove , @product_id = order_details.product_id , @qty = order_details.qty from order_details join deleted on order_details.id = deleted.id;
		IF @is_check != 1
		BEGIN
			UPDATE product SET quantity = quantity - @qty where product.id = @product_id;
		END
	END
	GO

