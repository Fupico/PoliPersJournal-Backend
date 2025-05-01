using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;

        public UserService(UserManager<ApplicationUser> userManager, IUserRepository userRepository, ISettingRepository settingRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _settingRepository = settingRepository;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<object>> LoginAsync(LoginRequestDto request)
        {
            ApplicationUser? user = null;

            // 🧩 1. Sırayla dolu olan kimlik bilgisine göre kullanıcıyı bul
            if (!string.IsNullOrWhiteSpace(request.UserName))
                user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null && !string.IsNullOrWhiteSpace(request.Email))
                user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null && !string.IsNullOrWhiteSpace(request.PhoneNumber))
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            if (user == null)
                return new ApiResponse<object> { Success = false, Message = "Geçersiz kimlik bilgileri." };

            // 🔐 2. Şifre kontrolü
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return new ApiResponse<object> { Success = false, Message = "Geçersiz kimlik bilgileri." };

            // ✅ 3. Onay kontrolleri
            if (!user.EmailConfirmed)
                return new ApiResponse<object> { Success = false, Message = "E-posta adresinizi doğrulamanız gerekiyor." };

            if (!user.PhoneNumberConfirmed)
                return new ApiResponse<object> { Success = false, Message = "Telefon numaranızı doğrulamanız gerekiyor." };

            if (user.Invalidated == 0)
                return new ApiResponse<object> { Success = false, Message = "Hesabınız henüz yönetici tarafından onaylanmadı." };

            // 🔑 4. Token oluştur
            var token = await _jwtService.GenerateToken(user);

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Giriş başarılı.",
                Data = new { Token = token }
            };
        }


        public async Task<ApiResponse<object>> RegisterUserAsync(RegisterDto model)
        {
            // 1️⃣ Şirket ID kontrolü
            var settings = await _settingRepository.GetSettingsByCompanyIdAsync(model.CompanyId);
            if (settings == null)
                return new ApiResponse<object> { Success = false, Message = "Bu şirket için sistem ayarları yapılandırılmamış." };

            // 2️⃣ Kullanıcı giriş yöntemlerine göre zorunlu alanları kontrol et
            if (settings.AllowedLoginMethods.HasFlag(LoginMethod.Email) && string.IsNullOrWhiteSpace(model.Email))
                return new ApiResponse<object> { Success = false, Message = "E-posta adresi zorunludur." };

            if (settings.AllowedLoginMethods.HasFlag(LoginMethod.Username) && string.IsNullOrWhiteSpace(model.UserName))
                return new ApiResponse<object> { Success = false, Message = "Kullanıcı adı zorunludur." };

            if (settings.AllowedLoginMethods.HasFlag(LoginMethod.Phone) && string.IsNullOrWhiteSpace(model.PhoneNumber))
                return new ApiResponse<object> { Success = false, Message = "Telefon numarası zorunludur." };

            // 3️⃣ Kullanıcı adı oluşturma mantığı (Email veya Telefon girişe uygunsa fallback olarak kullanılır)
            var username = model.UserName ?? model.Email ?? model.PhoneNumber;

            // 4️⃣ Kullanıcı oluştur
            var user = new ApplicationUser
            {
                UserName = username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name ?? string.Empty,
                Surname = model.Surname ?? string.Empty,
                CompanyId = model.CompanyId,
                TC = settings.RequireTCApproval && !string.IsNullOrWhiteSpace(model.TC) ? Sha256Hash(model.TC) : null,
                Invalidated = settings.RequireAdminApproval ? (byte)0 : (byte)1,
                EmailConfirmed = !settings.RequireEmailConfirmation,
                PhoneNumberConfirmed = !settings.RequirePhoneConfirmation
            };

            // 5️⃣ Kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ApiResponse<object> { Success = false, Message = "Kullanıcı kaydı başarısız.", Data = result.Errors };

            return new ApiResponse<object> { Success = true, Message = "Kayıt başarılı. Lütfen doğrulama işlemlerini tamamlayın." };
        }

        private string Sha256Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
