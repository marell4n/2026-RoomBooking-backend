# Changelog

Semua perubahan yang dilakukan akan didokumentasikan di dalam file ini.

## [Unreleased]

### Added
- Menginisialisasi proyek dengan ASP .NET Core 9 Web API.
- Menambahkan .env.example untuk environment configuration template.
- Menambahkan .gitignore.
- Menambahkan MySQL Entity Framework Core packages.
- Mengonfigurasi koneksi database di appsettings.json.
- Menambahkan Room & Booking model dengan attribute validasi.
- Menambahkan AppDbContext dan MySQL service di Program.cs.
- Downgraded Swashbuckle.AspNetCore dari 10.0.0 ke 6.6.2.
- Implementasi controller secara manual untuk RoomsController dan BookingsController (karena masalah scaffolding)
- Mengubah database migration menjadi satu `IntialCreate` migration.