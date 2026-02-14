# Changelog

Semua perubahan yang dilakukan akan didokumentasikan di dalam file ini.

## [1.0.0] - 2026-02-12
**Initial Release**

### Chore
- Menginisialisasi proyek dengan **ASP .NET Core 9 Web API**.
- Menambahkan `.env.example` untuk **environment configuration template**.
- Menambahkan `.gitignore`.
- Menambahkan `MySQL Entity Framework Core packages`.
- Mengonfigurasi **koneksi database** di `appsettings.json`.

### Chore
- Menginisialisasi proyek dengan **ASP .NET Core 9 Web API**.
- Menambahkan `.env.example` untuk **environment configuration template**.
- Menambahkan `.gitignore`.
- Menambahkan `MySQL Entity Framework Core packages`.
- Mengonfigurasi **koneksi database** di `appsettings.json`.

### Added
- Menambahkan `Room` & `Booking` model dengan attribute validasi.
- Menambahkan `AppDbContext` dan `MySQL` service di `Program.cs`.
- Implementasi **controller secara manual** untuk `RoomsController` dan `BookingsController` (karena masalah scaffolding)
- Menambahkan **enum** `BookingStatus` untuk properti `Status` di Models\Booking.cs
- Membuat **DTO** untuk `Room` dan `Booking`.
- Menambahkan **Seeding DB** untuk default `Rooms` (beberapa ruangan di gedung D4).
- Menambahkan **soft delete** (`IsDeleted`) untuk `Room` and `Booking`.
- Menambahkan **timestamp** kapan `BookingStatus` diubah.
- Menambahkan **migrasi** untuk tabel `Rooms` dan `Bookings`

### Changed
- Downgraded `Swashbuckle.AspNetCore` dari 10.0.0 ke **6.6.2**.
- Mengubah **database migration** menjadi *satu* `IntialCreate` migration.
- Menggunakan **Scalar** (sebelumnya Swagger) untuk melihat *dokumentasi API*.
- Menghapus properti `IsAvailable` dari Models\Room.cs.

### Fixed
- Memperbaiki **controllers** sesuai dengan *DTO*, fitur *soft delete* dan *update* `BookingStatus`.
- Memperbaiki **controllers** karena inconsisten penamaan variabel.
- Memperbaiki perbedaan **DTO** dengan controllers dan models.
- Memperbaiki endpoint `GET` agar menampilkan `UpdateAt` dan `StatusUpdateAt`.

## [1.0.1] - 2026-02-14

### Added
- Menambahkan **CORS policy** agar frontend bisa berjalan lancar dengan backend.

## [1.0.2] - 2026-02-14

### Fixed
- **Booking Update Logic (Security)**:
    - Memperbarui endpoint `PUT /api/bookings/{id}` untuk memaksa status booking kembali menjadi **Pending** setiap kali data reservasi (Jam/Ruangan/Keperluan) diedit.
    - Hal ini mencegah celah keamanan di mana pengguna bisa mengubah detail booking yang sudah *Approved* tanpa melalui persetujuan ulang Admin.
    - Otomatis memperbarui timestamp `StatusUpdatedAt` saat pengeditan terjadi.