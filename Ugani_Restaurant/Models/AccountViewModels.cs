using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ugani_Restaurant.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu trước khi xác nhận!")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ tôi?")]
        public bool RememberMe { get; set; }
    }
    public class CreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn Quyền trước khi xác nhận!")]
        [Display(Name = "Quyền")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Họ và tên trước khi xác nhận!")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại trước khi xác nhận!")]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu trước khi xác nhận!")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu trước khi xác nhận!")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp với mật khẩu ban đầu!")]
        public string ConfirmPassword { get; set; }
    }

    public class EditRoleViewModel
    {
        public string UserId { get; set; } // Id của người dùng cần chỉnh sửa quyền truy cập
        [Required(ErrorMessage = "Vui lòng chọn Quyền trước khi xác nhận!")]
        [Display(Name = "Quyền")]
        public string Role { get; set; } // Quyền truy cập mới

        // Các thuộc tính khác (nếu cần thiết)
    }


    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ và tên trước khi xác nhận!")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại trước khi xác nhận!")]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu trước khi xác nhận!")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu trước khi xác nhận!")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp với mật khẩu ban đầu!")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu trước khi xác nhận!")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu trước khi xác nhận!")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp với mật khẩu ban đầu!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email trước khi xác nhận!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}