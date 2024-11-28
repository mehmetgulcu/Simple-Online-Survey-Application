# Basit Çevrimiçi Anket Uygulamasý - Readme
Bu belge, kullanýcýlarýn anketler oluþturmasýna, oylama yapmasýna ve sonuçlarý görüntülemesine olanak tanýyan, .NET Core Web API projesi olan Basit Çevrimiçi Anket Uygulamasý hakkýnda bir genel bakýþ saðlar.

## Proje Fonksiyonelliði
* Kullanýcýlar uygulamaya kaydolabilir ve giriþ yapabilir.
* Kullanýcýlar baþlýk ve birden fazla seçenek ile anket oluþturabilir.
* Kullanýcýlar tüm anketleri veya sadece kendi oluþturduklarý anketleri görüntüleyebilir.
* Kullanýcýlar belirli bir anketin detaylarýný, seçeneklerini ve oy sayýmlarýný görebilir.
* Kullanýcýlar bir anketin belirli bir seçeneði için oy verebilir.
* Kullanýcýlar bir anketin sonuçlarýný, seçenek metnini ve oy sayýsýný gösteren þekilde görüntüleyebilir.
## Kullanýlan Teknolojiler
* ASP.NET Core Web API: Web API oluþturmak için kullanýlan arka uç framework'ü.
* Entity Framework Core: Veri eriþimi ve veritabaný iþlemleri için kullanýlýr.
* JWT (JSON Web Token): Kullanýcý kimlik doðrulama ve yetkilendirme için kullanýlýr.
* MySQL: Uygulama verilerinin saklandýðý veritabaný.
## Proje Yapýsý
### Proje aþaðýdaki klasörlere ayrýlmýþtýr:

* Controllers: Kullanýcý isteklerini iþleyen sýnýflarý (API uç noktalarý).
* Dtos: Ýstemci ve sunucu arasýndaki veri alýþveriþi için kullanýlan Veri Transfer Objeleri.
* Interfaces: Temel iþlevler için hizmet arayüzleri.
* Models: Veri varlýklarýný temsil eden model sýnýflarý (örneðin, Kullanýcý, Anket, Seçenek).
* Services: Kullanýcý kaydý, giriþ, anket yönetimi ve oylama gibi hizmet mantýðýnýn uygulandýðý klasör.
* Startup.cs: Uygulamanýn ana yapýlandýrma dosyasý.
## Uygulamanýn Çalýþtýrýlmasý
* Proje deposunu klonlayýn veya indirin.
* Gerekli baðýmlýlýklarý yüklemek için dotnet restore komutunu çalýþtýrýn.
appsettings.json dosyasýndaki baðlantý dizesini yapýlandýrýn (veritabaný bilgilerinizi burada belirtin).
* Uygulamayý çalýþtýrmak için dotnet run komutunu kullanýn.

## API Uç Noktalarý
### Kimlik Doðrulama:
* POST /api/auth/register - Yeni bir kullanýcý kaydeder.
* POST /api/auth/login - Bir kullanýcýyý giriþ yaptýrýr ve JWT token'ý döner.
### Anketler:
* POST /api/poll/create (Yetkilendirme gerektirir) - Yeni bir anket oluþturur.
* GET /api/poll - Tüm anketleri getirir.
* GET /api/poll/my-pools (Yetkilendirme gerektirir) - Mevcut kullanýcýnýn oluþturduðu anketleri getirir.
* GET /api/poll/{pollId} (Yetkilendirme gerektirir) - Belirli bir anketin detaylarýný getirir.
### Oylama:
* POST /api/vote/vote (Yetkilendirme gerektirir) - Bir ankette bir seçeneðe oy verir.
* GET /api/vote/results/{pollId} (Yetkilendirme gerektirir) - Belirli bir anketin sonuçlarýný getirir.
### Yetkilendirme:
* Yetkilendirme gerektiren tüm uç noktalar, istek baþlýðýnda geçerli bir JWT token'ý bekler ("Authorization: Bearer <token>").

## Ekstra Notlar
* ServiceResponse<T> sýnýfý, hizmet metotlarýndan veri ve baþarý/hata mesajlarý döndürmek için standart bir yol saðlar.
* Geliþtirme modunda tüm kökenler için CORS (Cross-Origin Resource Sharing) etkinleþtirilmiþtir (prodüksiyon için düzenlenmesi gerekir).
* Potansiyel sorunlar için bilgilendirici mesajlar saðlayan hata iþleme uygulanmýþtýr.