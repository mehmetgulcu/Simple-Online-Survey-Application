# Basit �evrimi�i Anket Uygulamas� - Readme
Bu belge, kullan�c�lar�n anketler olu�turmas�na, oylama yapmas�na ve sonu�lar� g�r�nt�lemesine olanak tan�yan, .NET Core Web API projesi olan Basit �evrimi�i Anket Uygulamas� hakk�nda bir genel bak�� sa�lar.

## Proje Fonksiyonelli�i
* Kullan�c�lar uygulamaya kaydolabilir ve giri� yapabilir.
* Kullan�c�lar ba�l�k ve birden fazla se�enek ile anket olu�turabilir.
* Kullan�c�lar t�m anketleri veya sadece kendi olu�turduklar� anketleri g�r�nt�leyebilir.
* Kullan�c�lar belirli bir anketin detaylar�n�, se�eneklerini ve oy say�mlar�n� g�rebilir.
* Kullan�c�lar bir anketin belirli bir se�ene�i i�in oy verebilir.
* Kullan�c�lar bir anketin sonu�lar�n�, se�enek metnini ve oy say�s�n� g�steren �ekilde g�r�nt�leyebilir.
## Kullan�lan Teknolojiler
* ASP.NET Core Web API: Web API olu�turmak i�in kullan�lan arka u� framework'�.
* Entity Framework Core: Veri eri�imi ve veritaban� i�lemleri i�in kullan�l�r.
* JWT (JSON Web Token): Kullan�c� kimlik do�rulama ve yetkilendirme i�in kullan�l�r.
* MySQL: Uygulama verilerinin sakland��� veritaban�.
## Proje Yap�s�
### Proje a�a��daki klas�rlere ayr�lm��t�r:

* Controllers: Kullan�c� isteklerini i�leyen s�n�flar� (API u� noktalar�).
* Dtos: �stemci ve sunucu aras�ndaki veri al��veri�i i�in kullan�lan Veri Transfer Objeleri.
* Interfaces: Temel i�levler i�in hizmet aray�zleri.
* Models: Veri varl�klar�n� temsil eden model s�n�flar� (�rne�in, Kullan�c�, Anket, Se�enek).
* Services: Kullan�c� kayd�, giri�, anket y�netimi ve oylama gibi hizmet mant���n�n uyguland��� klas�r.
* Startup.cs: Uygulaman�n ana yap�land�rma dosyas�.
## Uygulaman�n �al��t�r�lmas�
* Proje deposunu klonlay�n veya indirin.
* Gerekli ba��ml�l�klar� y�klemek i�in dotnet restore komutunu �al��t�r�n.
appsettings.json dosyas�ndaki ba�lant� dizesini yap�land�r�n (veritaban� bilgilerinizi burada belirtin).
* Uygulamay� �al��t�rmak i�in dotnet run komutunu kullan�n.

## API U� Noktalar�
### Kimlik Do�rulama:
* POST /api/auth/register - Yeni bir kullan�c� kaydeder.
* POST /api/auth/login - Bir kullan�c�y� giri� yapt�r�r ve JWT token'� d�ner.
### Anketler:
* POST /api/poll/create (Yetkilendirme gerektirir) - Yeni bir anket olu�turur.
* GET /api/poll - T�m anketleri getirir.
* GET /api/poll/my-pools (Yetkilendirme gerektirir) - Mevcut kullan�c�n�n olu�turdu�u anketleri getirir.
* GET /api/poll/{pollId} (Yetkilendirme gerektirir) - Belirli bir anketin detaylar�n� getirir.
### Oylama:
* POST /api/vote/vote (Yetkilendirme gerektirir) - Bir ankette bir se�ene�e oy verir.
* GET /api/vote/results/{pollId} (Yetkilendirme gerektirir) - Belirli bir anketin sonu�lar�n� getirir.
### Yetkilendirme:
* Yetkilendirme gerektiren t�m u� noktalar, istek ba�l���nda ge�erli bir JWT token'� bekler ("Authorization: Bearer <token>").

## Ekstra Notlar
* ServiceResponse<T> s�n�f�, hizmet metotlar�ndan veri ve ba�ar�/hata mesajlar� d�nd�rmek i�in standart bir yol sa�lar.
* Geli�tirme modunda t�m k�kenler i�in CORS (Cross-Origin Resource Sharing) etkinle�tirilmi�tir (prod�ksiyon i�in d�zenlenmesi gerekir).
* Potansiyel sorunlar i�in bilgilendirici mesajlar sa�layan hata i�leme uygulanm��t�r.