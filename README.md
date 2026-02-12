# Room Booking Backend API

Backend API untuk sistem peminjaman ruangan kampus.

## Teknologi
- **Framework:** .NET 9 (C#)
- **Database:** MySQL
- **API Documentation:** Scalar UI
- **Architecture:** WEB API Controller-Based, DTO Pattern

## Fitur
### Rilis v1.0.0
1. **Room Management:**
    - CRUD: menambah, melihat, mengedit, dan menghapus data ruangan.
    - Data Seeding: database otomatis terisi dengan 15 data ruangan awal.
    - Soft Delete: Data yang dihapus tidak akan hilang permanen, hanya ditandai `IsDeleted`.

2. **Booking System:**
    - Pengajuan: User dapat mengajukan request peminjaman ruangan.
    - Cek Bentrok Otomatis (Conflit Guard): Sistem otomatis menolak jika pengajuan bertabrakan dengan booking lainnya.
    - Manajemen Status: 
        - `Pending` (menunggu persetujuan)
        - `Approved` (disetujui)
        - `Rejected` (ditolak)
        - `Canceled` (dibatalkan user/sistem)

3. **Audit dan Teknis**
    - Timestamp Otomatis: Mencatat waktu update terakhir (`UpdatedAt`) dan waktu perubahan status (`StatusUpdatedAt`) secara presisi (UTC).
    - Arsitektur DTO (Data Transfer Object): untuk memisahkan data internal database dengan data yang dikirim ke API.

## Cara Install (Local)
### Syarat
- .NET 9 SDK
- MySQL Server (XAMPP/Laragon/Docker)
- Editor (VS Code/Visual Studio)

### Langkah Instalasi
1. **Clone Repository:**
    ```bash
    git clone https://github.com/marell4n/2026-RoomBooking-backend
    cd 2026-RoomBooking-backend
2. **Konfigurasi Database:** Buka file `appsettings.json`, sesuaikan connection string dengan user/pass MySQL lokal:
    ```bash
    "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=db_roombooking;user=root;password="
    }
3. **Restore Paket NuGet:** Download ulang dependency yang dibutuhkan:
    ```bash
    dotnet restore
4. **Build Ulang Project:**
    ```bash
    dotnet build
5. **Migrasi Database:** Jalankan perintah:
    ```bash
    dotnet ef database update
4. **Jalankan Aplikasi:**
    ```bash
    dotnet run
5. Tambahkan `/scalar/v1` di `localhost:[port]` untuk melihat API documentation dan tes endpoints.