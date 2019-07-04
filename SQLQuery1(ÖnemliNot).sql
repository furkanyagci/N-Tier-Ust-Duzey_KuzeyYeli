use KuzeyYeli

GO
create proc prc_Urunler_Select
as
begin
select * from Urunler
end
GO

exec prc_Urunler_Select

GO
create proc prc_Kategoriler_Select
as
begin
select * from Kategoriler
end
GO

exec prc_Kategoriler_Select

go
create procedure prc_select
@TableName varchar(128)
as

declare @sql varchar(4000)
	select @sql = 'select * from [' + @TableName + ']'
	exec (@sql)
go

use KuzeyYeli
SELECT GETDATE();
select * from Urunler
SELECT GETDATE();

SELECT GETDATE();
exec prc_select 'Urunler'
SELECT GETDATE();


create proc prc_Urunler_Insert
@UrunID int,
@UrunAdi nvarchar(50),
@Fiyat money,
@Stok smallint,
@KategoriID int,
@TedarikciID int,
@BirimdekiMiktar nvarchar(50),
@YeniSatis smallint,
@EnAzYenidenSatisMikatari smallint,
@Sonlandi bit
as
begin
insert Urunler values(@UrunAdi,@TedarikciID,@KategoriID,@BirimdekiMiktar,@Fiyat,@Stok,@YeniSatis,@EnAzYenidenSatisMikatari,@Sonlandi)
end

go
use KuzeyYeli
exec [dbo].[prc_Kategoriler_Select]
delete from Kategoriler where KategoriID=13
go
create proc prc_Tedarikciler_Select
as
begin
select * from Tedarikciler
end
go

exec prc_Tedarikciler_Select

go
create proc prc_Kategoriler_Insert
@KategoriID int,
@KategoriAdi nvarchar(50),
@Tanimi nvarchar(100),
@Resim image
as
insert Kategoriler values(@KategoriAdi,@Tanimi,@Resim)
go

use KuzeyYeli
go
 create proc prc_Kategoriler_Delete
 @KategoriID int
 as
 begin
 delete Kategoriler where KategoriID=@KategoriID
 end
 go
 create proc prc_Urunler_Delete
 @UrunID int
 as
 delete Urunler where UrunID=@UrunID



